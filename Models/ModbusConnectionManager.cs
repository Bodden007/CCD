using NModbus;
using System;
using System.Collections.Concurrent;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace CCD.Models
{
    internal class ModbusConnectionManager : IDisposable
    {
        private static readonly Lazy<ModbusConnectionManager> _instance =
            new Lazy<ModbusConnectionManager>(() => new ModbusConnectionManager());

        private TcpClient _client;
        private IModbusMaster _master;
        private readonly SemaphoreSlim _connectionLock = new SemaphoreSlim(1, 1);
        private readonly ConcurrentQueue<ModbusRequest> _requestQueue = new ConcurrentQueue<ModbusRequest>();
        private readonly CancellationTokenSource _cts = new CancellationTokenSource();
        private bool _isProcessing;
        private int _reconnectAttempts;

        // Настройки подключения
        private const string ModbusIp = "10.0.6.10";
        private const int ModbusPort = 502;

        // Настройки повторного подключения
        private const int MaxReconnectAttempts = 5;
        private const int InitialReconnectDelay = 1000;
        private const int MaxReconnectDelay = 10000;
        private const int ConnectionCheckInterval = 5000; // 5 секунд между попытками подключения

        public static ModbusConnectionManager Instance => _instance.Value;

        private ModbusConnectionManager()
        {
            // Запускаем фоновую задачу для обработки очереди
            Task.Factory.StartNew(async () => await ProcessQueueAsync().ConfigureAwait(false),
                TaskCreationOptions.LongRunning);

            // Запускаем фоновую задачу для поддержания соединения
            Task.Factory.StartNew(async () => await MaintainConnectionAsync().ConfigureAwait(false),
                TaskCreationOptions.LongRunning);
        }

        private async Task MaintainConnectionAsync()
        {
            while (!_cts.IsCancellationRequested)
            {
                try
                {
                    await _connectionLock.WaitAsync(_cts.Token);

                    // Если соединение отсутствует или разорвано
                    if (_client == null || !_client.Connected)
                    {
                        try
                        {
                            await TryReconnectWithRetryAsync();
                        }
                        catch
                        {
                            // Логируем ошибку или просто ждем следующей попытки
                        }
                    }
                }
                catch (OperationCanceledException)
                {
                    // Игнорируем отмену операции
                }
                finally
                {
                    if (_connectionLock.CurrentCount == 0)
                        _connectionLock.Release();
                }

                // Ждем перед следующей проверкой соединения
                await Task.Delay(ConnectionCheckInterval, _cts.Token);
            }
        }

        public async Task<ushort[]> ReadInputRegistersAsync(byte slaveId, ushort startAddress, ushort numberOfPoints)
        {
            var tcs = new TaskCompletionSource<ushort[]>();
            _requestQueue.Enqueue(new ModbusRequest
            {
                SlaveId = slaveId,
                StartAddress = startAddress,
                NumberOfPoints = numberOfPoints,
                CompletionSource = tcs
            });
            return await tcs.Task;
        }

        public async Task WriteMultipleRegistersAsync(byte slaveId, ushort startAddress, ushort[] data)
        {
            var tcs = new TaskCompletionSource<bool>();
            _requestQueue.Enqueue(new ModbusRequest
            {
                SlaveId = slaveId,
                StartAddress = startAddress,
                DataToWrite = data,
                CompletionSource = tcs
            });
            await tcs.Task;
        }

        private async Task ProcessQueueAsync()
        {
            while (!_cts.IsCancellationRequested)
            {
                if (_requestQueue.TryDequeue(out var request))
                {
                    _isProcessing = true;
                    try
                    {
                        await _connectionLock.WaitAsync(_cts.Token);

                        if (_client == null || !_client.Connected)
                        {
                            await TryReconnectWithRetryAsync();
                        }

                        var master = GetMaster();
                        if (request.DataToWrite != null)
                        {
                            await Task.Run(() => master.WriteMultipleRegisters(
                                request.SlaveId,
                                request.StartAddress,
                                request.DataToWrite));
                            ((TaskCompletionSource<bool>)request.CompletionSource).TrySetResult(true);
                        }
                        else
                        {
                            var result = await Task.Run(() => master.ReadInputRegisters(
                                request.SlaveId,
                                request.StartAddress,
                                request.NumberOfPoints));
                            ((TaskCompletionSource<ushort[]>)request.CompletionSource).TrySetResult(result);
                        }

                        // Сброс счетчика попыток после успешной операции
                        _reconnectAttempts = 0;
                    }
                    catch (OperationCanceledException)
                    {
                        // Игнорируем отмену операции
                    }
                    catch (Exception ex)
                    {
                        SetExceptionForRequest(request, ex);
                        await Task.Delay(CalculateReconnectDelay(), _cts.Token);
                    }
                    finally
                    {
                        _connectionLock.Release();
                        _isProcessing = false;
                    }
                }
                await Task.Delay(50, _cts.Token);
            }
        }

        private void SetExceptionForRequest(ModbusRequest request, Exception ex)
        {
            if (request.CompletionSource is TaskCompletionSource<ushort[]> tcsUshort)
            {
                tcsUshort.TrySetException(ex);
            }
            else if (request.CompletionSource is TaskCompletionSource<bool> tcsBool)
            {
                tcsBool.TrySetException(ex);
            }
        }

        private async Task TryReconnectWithRetryAsync()
        {
            while (_reconnectAttempts < MaxReconnectAttempts && !_cts.IsCancellationRequested)
            {
                try
                {
                    await ReconnectAsync();
                    return;
                }
                catch
                {
                    _reconnectAttempts++;
                    if (_reconnectAttempts >= MaxReconnectAttempts)
                    {
                        throw new InvalidOperationException($"Failed to reconnect after {MaxReconnectAttempts} attempts");
                    }
                    await Task.Delay(CalculateReconnectDelay(), _cts.Token);
                }
            }
        }

        private int CalculateReconnectDelay()
        {
            // Экспоненциальная задержка с ограничением максимума
            return (int)Math.Min(
                InitialReconnectDelay * Math.Pow(2, _reconnectAttempts),
                MaxReconnectDelay);
        }

        private async Task ReconnectAsync()
        {
            try
            {
                DisposeConnection();
                _client = new TcpClient();

                var connectTask = _client.ConnectAsync(ModbusIp, ModbusPort);
                var timeoutTask = Task.Delay(3000, _cts.Token);

                if (await Task.WhenAny(connectTask, timeoutTask) == timeoutTask)
                {
                    throw new TimeoutException("Connection timeout");
                }

                _master = new ModbusFactory().CreateMaster(_client);
            }
            catch
            {
                DisposeConnection();
                throw;
            }
        }

        private void DisposeConnection()
        {
            if (_master != null)
            {
                _master.Dispose();
                _master = null;
            }
            if (_client != null)
            {
                _client.Close();
                _client = null;
            }
        }

        private IModbusMaster GetMaster()
        {
            if (_master == null || !_client.Connected)
            {
                throw new InvalidOperationException("Modbus connection is not established");
            }
            return _master;
        }

        public void Dispose()
        {
            _cts.Cancel();
            DisposeConnection();
        }

        private class ModbusRequest
        {
            public byte SlaveId { get; set; }
            public ushort StartAddress { get; set; }
            public ushort NumberOfPoints { get; set; }
            public ushort[] DataToWrite { get; set; }
            public object CompletionSource { get; set; }
        }
    }
}