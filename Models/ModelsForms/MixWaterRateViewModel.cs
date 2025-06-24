using CCD.SRC;
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
        private readonly ModbusConfig _config;
        private readonly RegisterAddressConfig _regAddr;

        public MixWaterRateViewModel()
        {
            _config = ModbusConfig.Load();
            _regAddr = _config.RegisterAddress;
        }

        private RelayCommand _zeroJobTotalsCommand;
        private RelayCommand _zeroStageTotalsCommand;
        private RelayCommand _updateJobTotalsCommand;
        private RelayCommand _updateStageTotalsCommand;

        private float _stageTotalWTR;
        private string _stageTotalWTRStr = "N/A";
        private float _jobTotalWTR;
        private string _jobTotalWTRStr = "N/A";
        private string _stageTotalWTR_Reg = "0";
        private string _jobTotalWTR_Reg = "0";

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

        //NOTE Переменная для нового значения JobTotal
        public string StageTotalWTR_Reg
        {
            get => _stageTotalWTR_Reg;
            set
            {
                if (_stageTotalWTR_Reg != value)
                {
                    _stageTotalWTR_Reg = value;
                    OnPropertyChanged();
                }
            }
        }

        //NOTE Переменная для нового значения JobTotal
        public string JobTotalWTR_Reg
        {
            get => _jobTotalWTR_Reg;
            set
            {
                if (_jobTotalWTR_Reg != value)
                {
                    _jobTotalWTR_Reg = value;
                    OnPropertyChanged();
                }
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
                await WriteRegisterAsync(_regAddr.JobTotalWTR[0], 0);
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
                await WriteRegisterAsync(_regAddr.StageTotalWTR[0], 0);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка в SetPSZero: {ex.Message}");
            }
        }

        /// <summary>
        /// Команда обновления Job Totals
        /// </summary>
        /// <returns></returns>
        public ICommand UpdateJobTotalsCommand
        {
            get { return _updateJobTotalsCommand ?? (_updateJobTotalsCommand = new RelayCommand(async () => await UpdateJobTotals())); }
        }
        private async Task UpdateJobTotals()
        {
            if (string.IsNullOrEmpty(JobTotalWTR_Reg))
            {
                JobTotalWTR_Reg = "0"; // Автоматически подставляем 0 если поле пустое
            }

            if (float.TryParse(JobTotalWTR_Reg, out float value))
            {
                await WriteRegisterAsync(_regAddr.JobTotalWTR[0], value);
            }
            else
            {
                JobTotalWTR_Reg = "0"; // Сбрасываем на 0 при ошибке
            }
        }

        /// <summary>
        /// Команда обновления Stage Totals
        /// </summary>
        /// <returns></returns>
        public ICommand UpdateStageTotalsCommand
        {
            get { return _updateStageTotalsCommand ?? (_updateStageTotalsCommand = new RelayCommand(async () => await UpdateStageTotals())); }
        }
        private async Task UpdateStageTotals()
        {
            if (string.IsNullOrEmpty(StageTotalWTR_Reg))
            {
                StageTotalWTR_Reg = "0"; // Автоматически подставляем 0 если поле пустое
            }

            if (float.TryParse(StageTotalWTR_Reg, out float value))
            {
                await WriteRegisterAsync(_regAddr.StageTotalWTR[0], value);
            }
            else
            {
                StageTotalWTR_Reg = "0"; // Сбрасываем на 0 при ошибке
            }
        }
        public async Task StartPollingAsync()
        {
            if (_isPolling) return;

            await PollRegistersContinuously((ushort)_regAddr.StageTotalWTR[0], 4, 400, registers =>
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

                registers[0] = BitConverter.ToUInt16(floatBytes, 0); // Старшие 2 байта
                registers[1] = BitConverter.ToUInt16(floatBytes, 2); // Младшие 2 байта

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
