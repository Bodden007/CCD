using NModbus;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CCD.Models
{
    internal class MainViewModel : INotifyPropertyChanged
    {
        private string _psPassenger = "N/A";
        private string _dsPassenger = "N/A";
        private TcpClient _client;
        private IModbusMaster _master;
        private bool _isPolling;

        public string PSPassenger
        {
            get => _psPassenger;
            set
            {
                _psPassenger = value;
                OnPropertyChanged();
            }
        }

        public string DSPassenger
        {
            get => _dsPassenger;
            set
            {
                _dsPassenger = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public async Task StartPollingAsync()
        {
            if (_isPolling) return;

            _isPolling = true;

            try
            {
                // Инициализация соединения один раз
                _client = new TcpClient();
                await _client.ConnectAsync("10.0.6.10", 502);
                var factory = new ModbusFactory();
                _master = factory.CreateMaster(_client);

                while (_isPolling)
                {
                    try
                    {
                        await PollDataAsync();
                    }
                    catch (Exception)
                    {
                        // Оставляем пустой catch
                        PSPassenger = "Error";
                        DSPassenger = "Error";
                    }

                    // Короткая пауза между опросами (400 мс)
                    await Task.Delay(400);
                }
            }
            finally
            {
                _master?.Dispose();
                _client?.Close();
                _isPolling = false;
            }
        }

        public void StopPolling()
        {
            _isPolling = false;
        }

        private async Task PollDataAsync()
        {
            var registers = await Task.Run(() =>
                _master.ReadInputRegisters(15, 0, 4));

            if (registers.Length >= 4)
            {
                var float1 = CombineRegistersToFloat(registers[0], registers[1]);
                var float2 = CombineRegistersToFloat(registers[2], registers[3]);

                PSPassenger = float1.ToString("F2") + " PSI";
                DSPassenger = float2.ToString("F2") + " PSI";
            }
        }

        private float CombineRegistersToFloat(ushort reg1, ushort reg2)
        {
            uint combined = (uint)(reg2 << 16) | reg1;
            byte[] bytes = BitConverter.GetBytes(combined);
            return BitConverter.ToSingle(bytes, 0);
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
