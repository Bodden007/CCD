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
    internal class MixWaterRateViewModel : ModbusWindowViewModel
    {
        private RelayCommand _zeroJobTotalsCommand;
        private RelayCommand _zeroStageTotalsCommand;

        private float _stageTotalWTR;
        private string _stageTotalWTRStr = "N/A";

        private float _jobTotalWTR;
        private string _jobTotalWTRStr = "N/A";

        //NOTE Stage расход через Flow Meter в gal
        public string StageTotalWTRStr
        {
            get => _stageTotalWTRStr;
            set { _stageTotalWTRStr = value; OnPropertyChanged(); }
        }
        public float StageTotalWTR
        {
            get => _stageTotalWTR;
            set
            {
                _stageTotalWTR = value;
                StageTotalWTRStr = $"{value:N2} gal";
                OnPropertyChanged();
            }
        }

        //NOTE Job расход через Flow Meter в gal
        public string JobTotalWTRStr
        {
            get => _jobTotalWTRStr;
            set { _jobTotalWTRStr = value; OnPropertyChanged(); }
        }
        public float JobTotalWTR
        {
            get => _jobTotalWTR;
            set
            {
                _jobTotalWTR = value;
                JobTotalWTRStr = $"{value:N2} gal";
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Команда обнуления Job Total
        /// </summary>
        public ICommand ZeroJobTotalsCommand
        {
            get { return _zeroJobTotalsCommand ?? (_zeroJobTotalsCommand = new RelayCommand(async () => await ZeroJobTotals())); }
        }
        //TODO запись в регист Job Total
        private async Task ZeroJobTotals()
        {
            try
            {
                await WriteRegisterAsync(26, 0); 
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка в SetPSZero: {ex.Message}");
            }
        }
        /// <summary>
        /// Команда обнуления Stage Total
        /// </summary>
        public ICommand ZeroStageTotalsCommand
        {
            get { return _zeroStageTotalsCommand ?? (_zeroStageTotalsCommand = new RelayCommand(async () => await ZeroStageTotals())); }
        }
        private async Task ZeroStageTotals()
        {
            try
            {
                await WriteRegisterAsync(24, 0); 
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка в SetPSZero: {ex.Message}");
            }
        }
        public async Task StartPollingAsync()
        {
            if (_isPolling) return;

            await PollRegistersContinuously(24, 4, 400, registers =>
            {
                StageTotalWTR = ModbusUtility.GetSingle(registers[1], registers[0]);
                JobTotalWTR = ModbusUtility.GetSingle(registers[3], registers[2]);
            });
        }
        private async Task WriteRegisterAsync(int register, float value) // Теперь принимает ushort
        {
            try
            {
                byte[] floatBytes = BitConverter.GetBytes(value);
                ushort[] registers = new ushort[2];

                registers[0] = BitConverter.ToUInt16(floatBytes, 2); // Старшие 2 байта
                registers[1] = BitConverter.ToUInt16(floatBytes, 0); // Младшие 2 байта

                await _connection.WriteMultipleRegistersAsync(
                    slaveId: 15,
                    startAddress: (ushort)register, // Также преобразуем номер регистра
                    registers // Теперь value точно ushort
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
