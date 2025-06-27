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
    internal class DsPressureViewModel : ModbusWindowViewModel
    {
        private readonly ModbusConfig _config;
        private readonly RegisterAddressConfig _regAddr;
        private readonly ModbusConnectionManager _modbusManager;
        private RelayCommand _saveCommand;

        public DsPressureViewModel()
        {
            _config = ModbusConfig.Load();
            _regAddr = _config.RegisterAddress;
            _modbusManager = ModbusConnectionManager.Instance;
        }
        private string _minCurrentDsStr = "0.00";
        private float _minCurrentDs;

        private string _maxCurrentDsStr = "0.00";
        private float _maxCurrentDs;

        private string _minPressureDsStr = "0.00";
        private float _minPressureDs;

        private string _maxPressureDsStr = "0.00";
        private float _maxPressureDs;

        private string _dSPassStr = "N/A";
        private int _dsPass;

        //NOTE Минимальное значение  тока
        public string MinCurrentDsStr
        {
            get => _minCurrentDsStr;
            set
            {
                if (_minCurrentDsStr == value) return;

                if (float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out float result))
                {
                    if (result >= 0.0f && result <= 20.0f)
                    {
                        _minCurrentDs = result;
                        _minCurrentDsStr = value;
                        OnPropertyChanged();
                    }
                    else
                    {
                        //FIXME Delete
                        Debug.WriteLine("Ошибка: MinFregFm выходит за пределы [0 - 2000]");
                        //TODO Можно восстановить последнее корректное значение:
                        _minCurrentDsStr = _minCurrentDs.ToString("F2", CultureInfo.InvariantCulture);
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
        public float MinCurrentDs
        {
            get => _minCurrentDs;
            set
            {
                if (_minCurrentDs == value) return;

                _minCurrentDs = value;
                MinCurrentDsStr = value.ToString("F2", CultureInfo.InvariantCulture);
                OnPropertyChanged();
                OnPropertyChanged(nameof(MinCurrentDsStr)); // Важно!
            }
        }

        //NOTE Максимальное значение  тока
        public string MaxCurrentDsStr
        {
            get => _maxCurrentDsStr;
            set
            {
                if (_maxCurrentDsStr == value) return;

                if (float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out float result))
                {
                    if (result >= 0.0f && result <= 20.0f)
                    {
                        _maxCurrentDs = result;
                        _maxCurrentDsStr = value;
                        OnPropertyChanged();
                    }
                    else
                    {
                        //FIXME Delete
                        Debug.WriteLine("Ошибка: MinFregFm выходит за пределы [0 - 2000]");
                        //TODO Можно восстановить последнее корректное значение:
                        _maxCurrentDsStr = _maxCurrentDs.ToString("F2", CultureInfo.InvariantCulture);
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
        public float MaxCurrentDs
        {
            get => _maxCurrentDs;
            set
            {
                if (_maxCurrentDs == value) return;

                _maxCurrentDs = value;
                MaxCurrentDsStr = value.ToString("F2", CultureInfo.InvariantCulture);
                OnPropertyChanged();
                OnPropertyChanged(nameof(MaxCurrentDsStr)); // Важно!
            }
        }

        //NOTE Минимальное значение давления
        public string MinPressureDsStr
        {
            get => _minPressureDsStr;
            set
            {
                if (_minPressureDsStr == value) return;

                if (float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out float result))
                {
                    if (result >= 0.0f && result <= 15000.0f)
                    {
                        _minPressureDs = result;
                        _minPressureDsStr = value;
                        OnPropertyChanged();
                    }
                    else
                    {
                        //FIXME Delete
                        Debug.WriteLine("Ошибка: MinFregFm выходит за пределы [0 - 2000]");
                        //TODO Можно восстановить последнее корректное значение:
                        _minPressureDsStr = _minPressureDs.ToString("F2", CultureInfo.InvariantCulture);
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
        public float MinPressureDs
        {
            get => _minPressureDs;
            set
            {
                if (_minPressureDs == value) return;

                _minPressureDs = value;
                MinPressureDsStr = value.ToString("F2", CultureInfo.InvariantCulture);
                OnPropertyChanged();
                OnPropertyChanged(nameof(MinPressureDsStr)); // Важно!
            }
        }

        //NOTE Максимальное значение давления
        public string MaxPressureDsStr
        {
            get => _maxPressureDsStr;
            set
            {
                if (_maxPressureDsStr == value) return;

                if (float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out float result))
                {
                    if (result >= 0.0f && result <= 15000.0f)
                    {
                        _maxPressureDs = result;
                        _maxPressureDsStr = value;
                        OnPropertyChanged();
                    }
                    else
                    {
                        //FIXME Delete
                        Debug.WriteLine("Ошибка: MinFregFm выходит за пределы [0 - 2000]");
                        //TODO Можно восстановить последнее корректное значение:
                        _maxPressureDsStr = _maxPressureDs.ToString("F2", CultureInfo.InvariantCulture);
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
        public float MaxPressureDs
        {
            get => _maxPressureDs;
            set
            {
                if (_maxPressureDs == value) return;

                _maxPressureDs = value;
                MaxPressureDsStr = value.ToString("F2", CultureInfo.InvariantCulture);
                OnPropertyChanged();
                OnPropertyChanged(nameof(MaxPressureDsStr)); // Важно!
            }
        }

        //NOTE давление пасажирской стороны
        public string DsPassStr
        {
            get => _dSPassStr;
            set { _dSPassStr = value; OnPropertyChanged(); }
        }
        public int DsPass
        {
            get => _dsPass;
            set
            {
                _dsPass = value;
                DsPassStr = value == 22222 ? "ERROR" : $"{value} PSI";
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
                await WriteRegisterAsync((ushort)_regAddr.MinCurrentDs[0], _minCurrentDs);
                await WriteRegisterAsync((ushort)_regAddr.MaxCurrentDs[0], _maxCurrentDs);
                await WriteRegisterAsync((ushort)_regAddr.MinPressureDs[0], _minPressureDs);
                await WriteRegisterAsync((ushort)_regAddr.MaxPressureDs[0], _maxPressureDs);
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
                var registers = await _modbusManager.ReadInputRegistersAsync(15, (ushort)_regAddr.MinCurrentDs[0], 8);

                MinCurrentDs = ModbusUtility.GetSingle(registers[1], registers[0]);
                MaxCurrentDs = ModbusUtility.GetSingle(registers[3], registers[2]);
                MinPressureDs = ModbusUtility.GetSingle(registers[5], registers[4]);
                MaxPressureDs = ModbusUtility.GetSingle(registers[7], registers[6]);

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

            await PollRegistersContinuously((ushort)_regAddr.DsPass, 2, 500, registers =>
            {
                DsPass = (short)registers[0];
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
