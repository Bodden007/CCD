using NModbus.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CCD.Models.ModelsForms
{
    internal class ManualControlViewModel : ModbusWindowViewModel
    {
        private RelayCommand _oscillateCements;
        private RelayCommand _oscillateBoth;
        private RelayCommand _oscillateWater; 
        private RelayCommand _openAllValves;
        private RelayCommand _closeAllValvesExit;
        private int _cmtBuf;
        private int _wtrBuf;
        private string _cmt = "N/A";
        private string _wtr = "N/A";

        public string CMT
        {
            get => _cmt; 
            set
            {
                _cmt = value;
                OnPropertyChanged();
            }
        }
        public int CMTBuf
        {
            get => _cmtBuf;
            set
            {
                _cmtBuf = value;
                CMT = $"CMT= {value}.0%";
                OnPropertyChanged();
            }
        }
        public string WTR
        { get => _wtr;
            set
            {
                _wtr = value;
                OnPropertyChanged();
            }
        }
        public int WTRBuf
        {
            get => _wtrBuf;
            set
            {
                _wtrBuf = value;
                WTR = $"WTR= {value}.0%";
                OnPropertyChanged();
            }
        }

        public ICommand OscillateCementsCommand
        {
            get { return _oscillateCements ?? (_oscillateCements = new RelayCommand(async () => await OscillateCements())); }
        }

        public ICommand CloseAllValvesExitCommand
        {
            get { return _closeAllValvesExit ?? (_closeAllValvesExit = new RelayCommand(async () => await CloseAllValvesExit())); }
        }

        public async Task StartPollingAsync()
        {
            if (_isPolling) return;

            await PollRegistersContinuously(52, 4, 400, registers =>
            {
                CMTBuf = (short)registers[0];
                WTRBuf = (short)registers[1];
                
            });
        }

       private async Task OscillateCements()
        {

            try
            {
                await WriteRegisterAsync(54, 221); // Запись значения 221 в регистр 52
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка в SetPSZero: {ex.Message}");
            }
        }


        private async Task CloseAllValvesExit()
        {

            try
            {
                await WriteRegisterAsync(54, 225); // Запись значения 225 в регистр 52
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка в SetPSZero: {ex.Message}");
            }
        }

        private async Task WriteRegisterAsync(int register, ushort value) // Теперь принимает ushort
        {
            try
            {
                await _connection.WriteMultipleRegistersAsync(
                    slaveId: 15,
                    startAddress: (ushort)register, // Также преобразуем номер регистра
                    new ushort[] { value } // Теперь value точно ushort
                );
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка записи в регистр {register}: {ex.Message}");
                throw;
            }
        }
    }
}
