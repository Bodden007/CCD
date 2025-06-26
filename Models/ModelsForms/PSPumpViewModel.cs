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
    internal class PSPumpViewModel : ModbusWindowViewModel
    {
        private readonly ModbusConfig _config;
        private readonly RegisterAddressConfig _regAddr;
        private readonly ModbusConnectionManager _modbusManager;
        private RelayCommand _saveCommand;
        public PSPumpViewModel()
        {
            _config = ModbusConfig.Load();
            _regAddr = _config.RegisterAddress;
            _modbusManager = ModbusConnectionManager.Instance;
        }

        private string _minFregPsStr = "0";
        private float _minFregPs;

        private string _maxFregPsStr = "0";
        private float _maxFregPs;

        private string _minFlowPsStr = "0";
        private float _minFlowPs;

        private string _maxFlowPsStr = "0";
        private float _maxFlowPs;

        private string _psRateStr = "N/A";
        private float _psRate;        

        //NOTE Минимальное значение  чатоты
        public string MinFregPsStr
        {
            get => _minFregPsStr;
            set
            {
                if (_minFregPsStr == value) return;

                if (float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out float result))
                {
                    if (result >= 0.0f && result <= 2000.0f)
                    {
                        _minFregPs = result;
                        _minFregPsStr = value;
                        OnPropertyChanged();
                    }
                    else
                    {
                        //FIXME Delete
                        Debug.WriteLine("Ошибка: MinFregFm выходит за пределы [0 - 2000]");
                        //TODO Можно восстановить последнее корректное значение:
                        _minFregPsStr = _minFregPs.ToString(CultureInfo.InvariantCulture);
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
        public float MinFregPs
        {
            get => _minFregPs;
            set
            {
                if (_minFregPs == value) return;

                _minFregPs = value;
                MinFregPsStr = value.ToString(CultureInfo.InvariantCulture);
                OnPropertyChanged();
                OnPropertyChanged(nameof(MinFregPsStr)); // Важно!
            }
        }

        //NOTE Максимальное значение  чатоты
        public string MaxFregPsStr
        {
            get => _maxFregPsStr;
            set
            {
                if (_maxFregPsStr == value) return;

                if (float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out float result))
                {
                    if (result >= 0.0f && result <= 2000.0f)
                    {
                        _maxFregPs = result;
                        _maxFregPsStr = value;
                        OnPropertyChanged();
                    }
                    else
                    {
                        //FIXME Delete
                        Debug.WriteLine("Ошибка: MinFregFm выходит за пределы [0 - 2000]");
                        //TODO Можно восстановить последнее корректное значение:
                        _maxFregPsStr = _maxFregPs.ToString(CultureInfo.InvariantCulture);
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
        public float MaxFregPs
        {
            get => _maxFregPs;
            set
            {
                if (_maxFregPs == value) return;

                _maxFregPs = value;
                MaxFregPsStr = value.ToString(CultureInfo.InvariantCulture);
                OnPropertyChanged();
                OnPropertyChanged(nameof(MaxFregPsStr)); // Важно!
            }
        }

        //NOTE Минимальное значение объема
        public string MinFlowPsStr
        {
            get => _minFlowPsStr;
            set
            {
                if (_minFlowPsStr == value) return;

                if (float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out float result))
                {
                    if (result >= 0.0f && result <= 2000.0f)
                    {
                        _minFlowPs = result;
                        _minFlowPsStr = value;
                        OnPropertyChanged();
                    }
                    else
                    {
                        //FIXME Delete
                        Debug.WriteLine("Ошибка: MinFregFm выходит за пределы [0 - 2000]");
                        //TODO Можно восстановить последнее корректное значение:
                        _minFlowPsStr = _minFlowPs.ToString(CultureInfo.InvariantCulture);
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
        public float MinFlowPs
        {
            get => _minFlowPs;
            set
            {
                if (_minFlowPs == value) return;

                _minFlowPs = value;
                MinFlowPsStr = value.ToString(CultureInfo.InvariantCulture);
                OnPropertyChanged();
                OnPropertyChanged(nameof(MinFlowPsStr)); // Важно!
            }
        }

        //NOTE Максимальное значение объема
        public string MaxFlowPsStr
        {
            get => _maxFlowPsStr;
            set
            {
                if (_maxFlowPsStr == value) return;

                if (float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out float result))
                {
                    if (result >= 0.0f && result <= 2000.0f)
                    {
                        _maxFlowPs = result;
                        _maxFlowPsStr = value;
                        OnPropertyChanged();
                    }
                    else
                    {
                        //FIXME Delete
                        Debug.WriteLine("Ошибка: MinFregFm выходит за пределы [0 - 2000]");
                        //TODO Можно восстановить последнее корректное значение:
                        _maxFlowPsStr = _maxFlowPs.ToString(CultureInfo.InvariantCulture);
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
        public float MaxFlowPs
        {
            get => _maxFlowPs;
            set
            {
                if (_maxFlowPs == value) return;

                _maxFlowPs = value;
                MaxFlowPsStr = value.ToString(CultureInfo.InvariantCulture);
                OnPropertyChanged();
                OnPropertyChanged(nameof(MaxFlowPsStr)); // Важно!
            }
        }

        //NOTE расход насоса пасажирская сторона
        public string PsRateStr
        {
            get => _psRateStr;
            set { _psRateStr = value; OnPropertyChanged(); }
        }
        public float PsRate
        {
            get => _psRate;
            set
            {
                _psRate = value;
                PsRateStr = $"{value} bpm";
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
                await WriteRegisterAsync((ushort)_regAddr.MinFregPs[0], _minFregPs);
                await WriteRegisterAsync((ushort)_regAddr.MaxFregPs[0], _maxFregPs);
                await WriteRegisterAsync((ushort)_regAddr.MinFlowPs[0], _minFlowPs);
                await WriteRegisterAsync((ushort)_regAddr.MaxFlowPs[0], _maxFlowPs);
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
                var registers = await _modbusManager.ReadInputRegistersAsync(15, (ushort)_regAddr.MinFregPs[0], 8);

                MinFregPs = ModbusUtility.GetSingle(registers[1], registers[0]);
                MaxFregPs = ModbusUtility.GetSingle(registers[3], registers[2]);
                MinFlowPs = ModbusUtility.GetSingle(registers[5], registers[4]);
                MaxFlowPs = ModbusUtility.GetSingle(registers[7], registers[6]);

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

            await PollRegistersContinuously((ushort)_regAddr.PsRate[0], 2, 500, registers =>
            {
                PsRate = ModbusUtility.GetSingle(registers[1], registers[0]);
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
