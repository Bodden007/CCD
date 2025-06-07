using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CCD.Models
{
    internal class ModbusWindowViewModel : INotifyPropertyChanged
    {
        protected bool _isPolling;
        protected readonly ModbusConnectionManager _connection = ModbusConnectionManager.Instance;
        protected readonly CancellationTokenSource _cts = new CancellationTokenSource();

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public virtual void StopPolling()
        {
            _isPolling = false;
            _cts.Cancel();
        }

        protected async Task PollRegistersContinuously(ushort startAddress, ushort count, int intervalMs,
            System.Action<ushort[]> updateAction)
        {
            _isPolling = true;
            while (_isPolling && !_cts.IsCancellationRequested)
            {
                try
                {
                    var registers = await _connection.ReadInputRegistersAsync(15, startAddress, count);
                    updateAction(registers);
                }
                catch
                {
                    // Можно добавить обработку ошибок для конкретных свойств
                }
                try
                {
                    await Task.Delay(intervalMs, _cts.Token);
                }
                catch
                {
                    // Игнорируем отмену задержки при закрытии окна
                    break;
                }


            }
        }
    }
}
