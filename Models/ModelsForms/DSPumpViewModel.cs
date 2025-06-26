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
    internal class DSPumpViewModel : ModbusWindowViewModel
    {
        private readonly ModbusConfig _config;
        private readonly RegisterAddressConfig _regAddr;
        private readonly ModbusConnectionManager _modbusManager;
        private RelayCommand _saveCommand;
        public DSPumpViewModel()
        {
            _config = ModbusConfig.Load();
            _regAddr = _config.RegisterAddress;
            _modbusManager = ModbusConnectionManager.Instance;
        }

        private string _minFregDsStr = "0";
        private float _minFregDs;

        private string _maxFregDsStr = "0";
        private float _maxFregDs;

        private string _minFlowDsStr = "0";
        private float _minFlowDs;

        private string _maxFlowDsStr = "0";
        private float _maxFlowDs;

        private string _dsRateStr = "N/A";
        private float _dsRate;

        //NOTE Минимальное значение  чатоты
        public string MinFregDsStr
        {
            get => _minFregDsStr;
            set
            {
                if (_minFregDsStr == value) return;

                if (float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out float result))
                {
                    if (result >= 0.0f && result <= 2000.0f)
                    {
                        _minFregDs = result;
                        _minFregDsStr = value;
                        OnPropertyChanged();
                    }
                    else
                    {
                        //FIXME Delete
                        Debug.WriteLine("Ошибка: MinFregFm выходит за пределы [0 - 2000]");
                        //TODO Можно восстановить последнее корректное значение:
                        _minFregDsStr = _minFregDs.ToString(CultureInfo.InvariantCulture);
                        OnPropertyChanged();
                    }
                }
                else
                {
                    //FIXME Delete
                    Debug.WriteLine("Ошибка: некорректный ввод в MinFregFmStr");
                }
            }
        }
        public float MinFregDs
        {
            get => _minFregDs;
            set
            {
                if (_minFregDs == value) return;

                _minFregDs = value;
                MinFregDsStr = value.ToString(CultureInfo.InvariantCulture);
                OnPropertyChanged();
                OnPropertyChanged(nameof(MinFregDsStr)); // Важно!
            }
        }

        //NOTE Максимальное значение  чатоты
        public string MaxFregDsStr
        {
            get => _maxFregDsStr;
            set
            {
                if (_maxFregDsStr == value) return;

                if (float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out float result))
                {
                    if (result >= 0.0f && result <= 2000.0f)
                    {
                        _maxFregDs = result;
                        _maxFregDsStr = value;
                        OnPropertyChanged();
                    }
                    else
                    {
                        //FIXME Delete
                        Debug.WriteLine("Ошибка: MinFregFm выходит за пределы [0 - 2000]");
                        //TODO Можно восстановить последнее корректное значение:
                        _maxFregDsStr = _maxFregDs.ToString(CultureInfo.InvariantCulture);
                        OnPropertyChanged();
                    }
                }
                else
                {
                    //FIXME Delete
                    Debug.WriteLine("Ошибка: некорректный ввод в MinFregFmStr");
                }
            }
        }
        public float MaxFregDs
        {
            get => _maxFregDs;
            set
            {
                if (_maxFregDs == value) return;

                _maxFregDs = value;
                MaxFregDsStr = value.ToString(CultureInfo.InvariantCulture);
                OnPropertyChanged();
                OnPropertyChanged(nameof(MaxFregDsStr)); // Важно!
            }
        }

        //NOTE Минимальное значение объема
        public string MinFlowDsStr
        {
            get => _minFlowDsStr;
            set
            {
                if (_minFlowDsStr == value) return;

                if (float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out float result))
                {
                    if (result >= 0.0f && result <= 2000.0f)
                    {
                        _minFlowDs = result;
                        _minFlowDsStr = value;
                        OnPropertyChanged();
                    }
                    else
                    {
                        //FIXME Delete
                        Debug.WriteLine("Ошибка: MinFregFm выходит за пределы [0 - 2000]");
                        //TODO Можно восстановить последнее корректное значение:
                        _minFlowDsStr = _minFlowDs.ToString(CultureInfo.InvariantCulture);
                        OnPropertyChanged();
                    }
                }
                else
                {
                    //FIXME Delete
                    Debug.WriteLine("Ошибка: некорректный ввод в MinFregFmStr");
                }
            }
        }
        public float MinFlowDs
        {
            get => _minFlowDs;
            set
            {
                if (_minFlowDs == value) return;

                _minFlowDs = value;
                MinFlowDsStr = value.ToString(CultureInfo.InvariantCulture);
                OnPropertyChanged();
                OnPropertyChanged(nameof(MinFlowDsStr)); // Важно!
            }
        }

        //NOTE Максимальное значение объема
        public string MaxFlowDsStr
        {
            get => _maxFlowDsStr;
            set
            {
                if (_maxFlowDsStr == value) return;

                if (float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out float result))
                {
                    if (result >= 0.0f && result <= 2000.0f)
                    {
                        _maxFlowDs = result;
                        _maxFlowDsStr = value;
                        OnPropertyChanged();
                    }
                    else
                    {
                        //FIXME Delete
                        Debug.WriteLine("Ошибка: MinFregFm выходит за пределы [0 - 2000]");
                        //TODO Можно восстановить последнее корректное значение:
                        _maxFlowDsStr = _maxFlowDs.ToString(CultureInfo.InvariantCulture);
                        OnPropertyChanged();
                    }
                }
                else
                {
                    //FIXME Delete
                    Debug.WriteLine("Ошибка: некорректный ввод в MinFregFmStr");
                }
            }
        }
        public float MaxFlowDs
        {
            get => _maxFlowDs;
            set
            {
                if (_maxFlowDs == value) return;

                _maxFlowDs = value;
                MaxFlowDsStr = value.ToString(CultureInfo.InvariantCulture);
                OnPropertyChanged();
                OnPropertyChanged(nameof(MaxFlowDsStr)); // Важно!
            }
        }

        //NOTE расход насоса пасажирская сторона
        public string DsRateStr
        {
            get => _dsRateStr;
            set { _dsRateStr = value; OnPropertyChanged(); }
        }
        public float DsRate
        {
            get => _dsRate;
            set
            {
                _dsRate = value;
                DsRateStr = $"{value} bpm";
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
                await WriteRegisterAsync((ushort)_regAddr.MinFregDs[0], _minFregDs);
                await WriteRegisterAsync((ushort)_regAddr.MaxFregDs[0], _maxFregDs);
                await WriteRegisterAsync((ushort)_regAddr.MinFlowDs[0], _minFlowDs);
                await WriteRegisterAsync((ushort)_regAddr.MaxFlowDs[0], _maxFlowDs);
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
            await PollMixWaterRateContinuously();
        }
        private async Task GetInitialConfiguration()
        {
            try
            {
                var registers = await _modbusManager.ReadInputRegistersAsync(15, (ushort)_regAddr.MinFregDs[0], 8);

                MinFregDs = ModbusUtility.GetSingle(registers[1], registers[0]);
                MaxFregDs = ModbusUtility.GetSingle(registers[3], registers[2]);
                MinFlowDs = ModbusUtility.GetSingle(registers[5], registers[4]);
                MaxFlowDs = ModbusUtility.GetSingle(registers[7], registers[6]);

            }
            catch (Exception ex)
            {
                //FIXME
                Debug.WriteLine($"Ошибка при получении начальной конфигурации: {ex.Message}");
                //TODO Можно установить значения по умолчанию или обработать ошибку
            }
        }
        private async Task PollMixWaterRateContinuously()
        {

            await PollRegistersContinuously((ushort)_regAddr.DsRate[0], 2, 500, registers =>
            {
                DsRate = ModbusUtility.GetSingle(registers[1], registers[0]);
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

