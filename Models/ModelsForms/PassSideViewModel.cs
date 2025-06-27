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
using System.Windows.Threading;

namespace CCD.Models.ModelsForms
{
    internal class PassSideViewModel : ModbusWindowViewModel
    {

        private readonly ModbusConfig _config;
        private readonly RegisterAddressConfig _regAddr;
        private readonly ModbusConnectionManager _modbusManager;

        public PassSideViewModel()
        {
            _config = ModbusConfig.Load();
            _regAddr = _config.RegisterAddress;
            _modbusManager = ModbusConnectionManager.Instance;
        }

        // Команды
        private RelayCommand _setPSKickoutCommand;
        private RelayCommand _setDSKickoutCommand;
        private RelayCommand _setPSZeroCommand;
        private RelayCommand _setDSZeroCommand;
        private RelayCommand _clearSetsCommand;

        private string _pSPassStr = "N/A";
        private int _psPass;
        private string _dSPassStr = "N/A";
        private int _dsPass;
        private string _pskoStr = "N/A";
        private int _psko;
        private string _dskoStr = "N/A";
        private int _dsko;
        
        private int _pskoReg;
        private int _dskoReg;
        private string _pskoRegStr;
        private string _dskoRegStr;

        //TODO переделать переменные
        public string PskoRegStr
        {
            get => _pskoRegStr;
            set
            {
                // Проверяем, что вводится число от 0 до 15000
                if (int.TryParse(value, out int num) && num >= 0 && num <= 15000)
                {
                    _pskoRegStr = value;
                    OnPropertyChanged();
                }
                else if (string.IsNullOrEmpty(value))
                {                    
                    OnPropertyChanged();
                }
            }

        }
        public int PskoReg
        {
            get => _pskoReg;
            set
            {
                _pskoReg = value;
                PskoRegStr = $"{value} PSI";
                OnPropertyChanged();
            }
        }
        public string DskoRegStr
        {
            get => _dskoRegStr;
            set 
            {
                // Проверяем, что вводится число от 0 до 15000
                if (int.TryParse(value, out int num) && num >= 0 && num <= 15000)
                {
                    _dskoRegStr = value;
                    OnPropertyChanged();
                }
                else if (string.IsNullOrEmpty(value))
                {
                    OnPropertyChanged();
                }
            }
        }
        public int DskoReg
        {
            get => _dskoReg;
            set
            {
                _dskoReg = value;
                DskoRegStr = $"{value} PSI";
                OnPropertyChanged();
            }
        }
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
        public string PSKOStr
        {
            get => _pskoStr;
            set {
                if (_pskoStr == value) return;

                if (int.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out int result))
                {
                    if (result >= 0.0f && result <= 15000.0f)
                    {
                        _psko = result;
                        _pskoStr = value;
                        OnPropertyChanged();
                    }
                    else
                    {
                        //FIXME Delete
                        Debug.WriteLine("Ошибка: MinFregFm выходит за пределы [0 - 15000]");
                        //TODO Можно восстановить последнее корректное значение:
                        _pskoStr = _psko.ToString( CultureInfo.InvariantCulture);
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
        public int PSKO
        {
            get => _psko;
            set
            {
                if (_psko == value) return;

                _psko = value;
                PSKOStr = value.ToString("F2", CultureInfo.InvariantCulture);
                OnPropertyChanged();
                OnPropertyChanged(nameof(PSKOStr)); // Важно!
            }
        }

        //NOTE DSKO
        public string DSKOStr
        {
            get => _dskoStr;
            set
            {
                if (_dskoStr == value) return;

                if (int.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out int result))
                {
                    if (result >= 0.0f && result <= 15000.0f)
                    {
                        _dsko = result;
                        _dskoStr = value;
                        OnPropertyChanged();
                    }
                    else
                    {
                        //FIXME Delete
                        Debug.WriteLine("Ошибка: MinFregFm выходит за пределы [0 - 15000]");
                        //TODO Можно восстановить последнее корректное значение:
                        _dskoStr = _dsko.ToString("F2", CultureInfo.InvariantCulture);
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

        public int DSKO
        {
            get => _dsko;
            set
            {
                if (_dsko == value) return;

                _dsko = value;
                DSKOStr = value.ToString("F2", CultureInfo.InvariantCulture);
                OnPropertyChanged();
                OnPropertyChanged(nameof(DSKOStr)); // Важно!
            }
        }

        public ICommand SetPSKickoutCommand
        {
            get { return _setPSKickoutCommand ?? (_setPSKickoutCommand = new RelayCommand(async () => await SetPSKickout())); }
        }
        private async Task SetPSKickout()
        {
            try
            {
                await WriteRegisterAsync((ushort)_regAddr.PsKo, (ushort)_psko);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка в SetPSKickout: {ex.Message}");
            }
        }
        public ICommand SetDSKickoutCommand
        {
            get { return _setDSKickoutCommand ?? (_setDSKickoutCommand = new RelayCommand(async () => await SetDSKickout())); }
        }
        private async Task SetDSKickout()
        {
            try
            {
                await WriteRegisterAsync((ushort)_regAddr.DsKo, (ushort)_dsko);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка в SetDSKickout: {ex.Message}");
            }
        }

        //TODO команды установки 0
        public ICommand SetPSZeroCommand
        {
            get { return _setPSZeroCommand ?? (_setPSZeroCommand = new RelayCommand(async () => await SetPSZero())); }
        }
        private async Task SetPSZero()
        {
            try
            {
                await WriteRegisterAsync(_regAddr.PsOffset, 321); // Запись значения 321 
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка в SetPSZero: {ex.Message}");
            }
        }

        public ICommand SetDSZeroCommand
        {
            get { return _setDSZeroCommand ?? (_setDSZeroCommand = new RelayCommand(async () => await SetDSZero())); }
        }
        private async Task SetDSZero()
        {
            try
            {
                await WriteRegisterAsync(_regAddr.DsOffset, 322); // Запись значения 321 в регистр 1
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка в SetPSZero: {ex.Message}");
            }
        }

        public ICommand ClearSetsCommand
        {
            get { return _clearSetsCommand ?? (_clearSetsCommand = new RelayCommand(async () => await ClearSets())); }
        }
        private async Task ClearSets()
        {
            try
            {
                await WriteRegisterAsync(_regAddr.PsOffset, 333); 
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка в SetPSZero: {ex.Message}");
            }
        }

        public async Task StartPollingAsync()
        {
            if (_isPolling) return;

            // Сначала получаем начальные конфигурационные значения
            await GetInitialConfiguration();

            // Затем запускаем непрерывный опрос коэффициента смешивания воды
            await PollPassSideContinuously();

        }

        private async Task GetInitialConfiguration()
        {
            try
            {
                var registers = await _modbusManager.ReadInputRegistersAsync(15, 0, 10);

                PSKO = (short)registers[_regAddr.PsKo];
                DSKO = (short)registers[_regAddr.DsKo];

            }
            catch (Exception ex)
            {
                //FIXME
                Debug.WriteLine($"Ошибка при получении начальной конфигурации: {ex.Message}");
                //TODO Можно установить значения по умолчанию или обработать ошибку
            }
        }
        private async Task PollPassSideContinuously()
        {

            await PollRegistersContinuously(0, 4, 500, registers =>
            {
                PsPass = (short)registers[_regAddr.PsPass];
                DsPass = (short)registers[_regAddr.DsPass];
            });
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
                //FIXME
                Debug.WriteLine($"Ошибка записи в регистр {register}: {ex.Message}");
                throw;
            }
        }
    }
}
