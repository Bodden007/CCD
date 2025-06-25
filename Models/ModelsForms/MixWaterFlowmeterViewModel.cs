using CCD.SRC;
using NModbus.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CCD.Models.ModelsForms
{
    internal class MixWaterFlowmeterViewModel: ModbusWindowViewModel
    {
        private readonly ModbusConfig _config;
        private readonly RegisterAddressConfig _regAddr;
        private readonly ModbusConnectionManager _modbusManager;

        private RelayCommand _saveCommand;

        public MixWaterFlowmeterViewModel()
        {
            _config = ModbusConfig.Load();
            _regAddr = _config.RegisterAddress;
            _modbusManager = ModbusConnectionManager.Instance;
        }

        private string _minFregFmStr = "0";
        private float _minFregFm;

        private string _maxFregFmStr = "0";
        private float _maxFregFm;

        private string _minFlowFmStr = "0";
        private float _minFlowFm;

        private string _maxFlowFmStr = "0";
        private float _maxFlowFm;

        private string _mixWaterRateStr = "N/A";
        private float _mixWaterRate;

        //NOTE Минимальное значение  чатоты
        public string MinFregFmStr
        {
            get => _minFregFmStr;
            set
            {
                _minFregFmStr = value;
                OnPropertyChanged();
                //if (_minFregFmStr != value)
                //{
                //    // Проверяем, можно ли преобразовать строку в float
                //    if (float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out float result))
                //    {
                //        _minFregFmStr = value;
                //        _minFregFm = result; // Обновляем float значение
                //        OnPropertyChanged();
                //    }
                //    else
                //    {
                //        // Можно добавить обработку ошибки или уведомление пользователя
                //        Debug.WriteLine("Некорректный формат числа");
                //    }
                //}
            }
        }
        public float MinFregFm
        {
            get => _minFregFm;
            set
            {
                _minFregFm = value;
                MinFregFmStr = $"{value}";
                OnPropertyChanged();
            }
        }

        //NOTE Максимальное значение  чатоты
        public string MaxFregFmStr
        {
            get => _maxFregFmStr;
            set
            {
                if (_maxFregFmStr != value)
                {
                    // Проверяем, можно ли преобразовать строку в float
                    if (float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out float result))
                    {
                        _maxFregFmStr = value;
                        _minFregFm = result; // Обновляем float значение
                        OnPropertyChanged();
                    }
                    else
                    {
                        // Можно добавить обработку ошибки или уведомление пользователя
                        Debug.WriteLine("Некорректный формат числа");
                    }
                }
            }
        }
        public float MaxFregFm
        {
            get => _maxFregFm;
            set
            {
                _maxFregFm = value;
                MaxFregFmStr = $"{value}";
                OnPropertyChanged();
            }
        }

        //NOTE Минимальное значение объема
        public string MinFlowFmStr
        {
            get => _minFlowFmStr;
            set
            {
                if (_minFlowFmStr != value)
                {
                    _minFlowFmStr = value;
                    OnPropertyChanged();
                }
            }
        }
        public float MinFlowFm
        {
            get => _minFlowFm;
            set
            {
                _minFlowFm = value;
                MinFlowFmStr = $"{value}";
                OnPropertyChanged();
            }
        }

        //NOTE Максимальное значение объема
        public string MaxFlowFmStr
        {
            get => _maxFlowFmStr;
            set
            {
                if (_maxFlowFmStr != value)
                {
                    _maxFlowFmStr = value;
                    OnPropertyChanged();
                }
            }
        }
        public float MaxFlowFm
        {
            get => _maxFlowFm;
            set
            {
                _maxFlowFm = value;
                MaxFlowFmStr = $"{value}";
                OnPropertyChanged();
            }
        }

        public string MixWaterRateStr
        {
            get => _mixWaterRateStr;
            set { _mixWaterRateStr = value; OnPropertyChanged(); }
        }
        public float MixWaterRate
        {
            get => _mixWaterRate;
            set
            {
                _mixWaterRate = value;
                MixWaterRateStr = $"{value:N2} gpm";
                OnPropertyChanged();
            }
        }

        public ICommand SaveCommand
        {
            get { return _saveCommand ?? (_saveCommand = new RelayCommand(async () => await Save())); }
        }
        private async Task Save()
        {
            try
            {
                // Отправляем значение MinFregFm в нужный регистр (укажите правильный регистр)
                await WriteRegisterAsync(76, _minFregFm);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка в Save: {ex.Message}");
            }
        }
        public async Task StartPollingAsync()
        {
            if (_isPolling) return;

            // Сначала получаем начальные конфигурационные значения
            await GetInitialConfiguration();

            // Затем запускаем непрерывный опрос коэффициента смешивания воды
            //await PollMixWaterRateContinuously();
        }

        private async Task GetInitialConfiguration()
        {
            try
            {               
                var registers = await _modbusManager.ReadInputRegistersAsync(15, 76, 8);

                MinFregFm = ModbusUtility.GetSingle(registers[1], registers[0]);
             
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка при получении начальной конфигурации: {ex.Message}");
                // Можно установить значения по умолчанию или обработать ошибку
            }
        }
        private async Task PollMixWaterRateContinuously()
        {
            // Предполагаем, что коэффициент смешивания воды находится в определенном регистре
            // Настройте адрес регистра согласно вашей конфигурации устройства
            ushort mixWaterRateRegister = (ushort)(_regAddr.StageTotalWTR[0] + 8); // Пример смещения

            await PollRegistersContinuously(mixWaterRateRegister, 2, 1000, registers =>
            {
                //_mixWaterRate = ModbusUtility.GetSingle(registers[1], registers[0]);
                //MixWaterRateStr = _mixWaterRate.ToString("F2"); // Формат с 2 знаками после запятой
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
