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
    internal class PsPressureViewModel : ModbusWindowViewModel
    {
        private readonly ModbusConfig _config;
        private readonly RegisterAddressConfig _regAddr;
        private readonly ModbusConnectionManager _modbusManager;
        private RelayCommand _saveCommand;

        public PsPressureViewModel()
        {
            _config = ModbusConfig.Load();
            _regAddr = _config.RegisterAddress;
            _modbusManager = ModbusConnectionManager.Instance;
        }

        private string _minCurrentPsStr = "0.00";
        private float _minCurrentPs;

        private string _maxCurrentPsStr = "0.00";
        private float _maxCurrentPs;

        private string _minPressurePsStr = "0.00";
        private float _minPressurePs;

        private string _maxPressurePsStr = "0.00";
        private float _maxPressurePs;

        private string _pSPassStr = "N/A";
        private int _psPass;
        
        //NOTE Минимальное значение  тока
        public string MinCurrentPsStr
        {
            get => _minCurrentPsStr;
            set
            {
                if (_minCurrentPsStr == value) return;

                if (float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out float result))
                {
                    if (result >= 0.0f && result <= 20.0f)
                    {
                        _minCurrentPs = result;
                        _minCurrentPsStr = value;
                        OnPropertyChanged();
                    }
                    else
                    {
                        //FIXME Delete
                        Debug.WriteLine("Ошибка: MinFregFm выходит за пределы [0 - 2000]");
                        //TODO Можно восстановить последнее корректное значение:
                        _minCurrentPsStr = _minCurrentPs.ToString("F2", CultureInfo.InvariantCulture);
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
        public float MinCurrentPs
        {
            get => _minCurrentPs;
            set
            {
                if (_minCurrentPs == value) return;

                _minCurrentPs = value;
                MinCurrentPsStr = value.ToString("F2", CultureInfo.InvariantCulture);
                OnPropertyChanged();
                OnPropertyChanged(nameof(MinCurrentPsStr)); // Важно!
            }
        }

        //NOTE Максимальное значение  тока
        public string MaxCurrentPsStr
        {
            get => _maxCurrentPsStr;
            set
            {
                if (_maxCurrentPsStr == value) return;

                if (float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out float result))
                {
                    if (result >= 0.0f && result <= 20.0f)
                    {
                        _maxCurrentPs = result;
                        _maxCurrentPsStr = value;
                        OnPropertyChanged();
                    }
                    else
                    {
                        //FIXME Delete
                        Debug.WriteLine("Ошибка: MinFregFm выходит за пределы [0 - 2000]");
                        //TODO Можно восстановить последнее корректное значение:
                        _maxCurrentPsStr = _maxCurrentPs.ToString("F2", CultureInfo.InvariantCulture);
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
        public float MaxCurrentPs
        {
            get => _maxCurrentPs;
            set
            {
                if (_maxCurrentPs == value) return;

                _maxCurrentPs = value;
                MaxCurrentPsStr = value.ToString("F2", CultureInfo.InvariantCulture);
                OnPropertyChanged();
                OnPropertyChanged(nameof(MaxCurrentPsStr)); // Важно!
            }
        }

        //NOTE Минимальное значение давления
        public string MinPressurePsStr
        {
            get => _minPressurePsStr;
            set
            {
                if (_minPressurePsStr == value) return;

                if (float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out float result))
                {
                    if (result >= 0.0f && result <= 15000.0f)
                    {
                        _minPressurePs = result;
                        _minPressurePsStr = value;
                        OnPropertyChanged();
                    }
                    else
                    {
                        //FIXME Delete
                        Debug.WriteLine("Ошибка: MinFregFm выходит за пределы [0 - 2000]");
                        //TODO Можно восстановить последнее корректное значение:
                        _minPressurePsStr = _minPressurePs.ToString("F2", CultureInfo.InvariantCulture);
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
        public float MinPressurePs
        {
            get => _minPressurePs;
            set
            {
                if (_minPressurePs == value) return;

                _minPressurePs = value;
                MinPressurePsStr = value.ToString("F2", CultureInfo.InvariantCulture);
                OnPropertyChanged();
                OnPropertyChanged(nameof(MinPressurePsStr)); // Важно!
            }
        }

        //NOTE Максимальное значение давления
        public string MaxPressurePsStr
        {
            get => _maxPressurePsStr;
            set
            {
                if (_maxPressurePsStr == value) return;

                if (float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out float result))
                {
                    if (result >= 0.0f && result <= 15000.0f)
                    {
                        _maxPressurePs = result;
                        _maxPressurePsStr = value;
                        OnPropertyChanged();
                    }
                    else
                    {
                        //FIXME Delete
                        Debug.WriteLine("Ошибка: MinFregFm выходит за пределы [0 - 2000]");
                        //TODO Можно восстановить последнее корректное значение:
                        _maxPressurePsStr = _maxPressurePs.ToString("F2", CultureInfo.InvariantCulture);
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
        public float MaxPressurePs
        {
            get => _maxPressurePs;
            set
            {
                if (_maxPressurePs == value) return;

                _maxPressurePs = value;
                MaxPressurePsStr = value.ToString("F2", CultureInfo.InvariantCulture);
                OnPropertyChanged();
                OnPropertyChanged(nameof(MaxPressurePsStr)); // Важно!
            }
        }

        //NOTE давление пасажирской стороны
        public string PsPassStr
        {
            get => _pSPassStr;
            set { _pSPassStr = value; OnPropertyChanged(); }
        }
        public int PsPass
        {
            get => _psPass;
            set
            {
                _psPass = value;
                PsPassStr = value == 22222 ? "ERROR" : $"{value} PSI";
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
                await WriteRegisterAsync((ushort)_regAddr.MinCurrentPs[0], _minCurrentPs);
                await WriteRegisterAsync((ushort)_regAddr.MaxCurrentPs[0], _maxCurrentPs);
                await WriteRegisterAsync((ushort)_regAddr.MinPressurePs[0], _minPressurePs);
                await WriteRegisterAsync((ushort)_regAddr.MaxPressurePs[0], _maxPressurePs);
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
                var registers = await _modbusManager.ReadInputRegistersAsync(15, (ushort)_regAddr.MinCurrentPs[0], 8);

                MinCurrentPs = ModbusUtility.GetSingle(registers[1], registers[0]);
                MaxCurrentPs = ModbusUtility.GetSingle(registers[3], registers[2]);
                MinPressurePs = ModbusUtility.GetSingle(registers[5], registers[4]);
                MaxPressurePs = ModbusUtility.GetSingle(registers[7], registers[6]);

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

            await PollRegistersContinuously((ushort)_regAddr.PsPass, 2, 500, registers =>
            {
                PsPass = (short)registers[0];
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
