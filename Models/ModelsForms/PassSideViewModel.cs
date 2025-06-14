using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace CCD.Models.ModelsForms
{
    internal class PassSideViewModel : ModbusWindowViewModel
    {

        private RelayCommand _writeCommand;

        // Команды
        private RelayCommand _setPSKickoutCommand;
        private RelayCommand _setDSKickoutCommand;
        private RelayCommand _setPSZeroCommand;
        private RelayCommand _setDSZeroCommand;
        private RelayCommand _clearSetsCommand;

        private int _psPassBuf;
        private int _dsPassBuf;
        private int _pskoBuf;
        private int _dskoBuf;
        private string _pSPass = "N/A";
        private string _dSPass = "N/A";
        private string _pskostr = "N/A";
        private string _dskostr = "N/A";

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

        public string PsPass
        {
            get => _pSPass;
            set { _pSPass = value; OnPropertyChanged(); }
        }
        public int PsPassBuf
        {
            get => _psPassBuf;
            set
            {
                _psPassBuf = value;
                PsPass = value == 22222 ? "ERROR" : $"{value} PSI";
                OnPropertyChanged();
            }
        }

        public string DsPass
        {
            get => _dSPass;
            set { _dSPass = value; OnPropertyChanged(); }
        }

        public int DsPassBuf
        {
            get => _dsPassBuf;
            set
            {
                _dsPassBuf = value;
                DsPass = value == 22222 ? "ERROR" : $"{value} PSI";
                OnPropertyChanged();
            }
        }

        public string PSKO
        {
            get => _pskostr;
            set { _pskostr = value; OnPropertyChanged(); }
        }

        public int PskoBuf
        {
            get => _pskoBuf;
            set
            {
                if (_pskoBuf != value)
                {
                    _pskoBuf = value;
                    PSKO = $"OPKO = {value} PSI";
                    OnPropertyChanged();

                    // Можно добавить автоматическую запись при изменении
                    // Task.Run(() => WriteValuesAsync());
                }
            }
        }

        public string DSKO
        {
            get => _dskostr;
            set { _dskostr = value; OnPropertyChanged(); }
        }

        public int DskoBuf
        {
            get => _dskoBuf;
            set
            {
                _dskoBuf = value;
                DSKO = $"OPKO = {value} PSI";
                OnPropertyChanged();
            }
        }

        public ICommand SetPSKickoutCommand
        {
            get { return _setPSKickoutCommand ?? (_setPSKickoutCommand = new RelayCommand(async () => await SetPSKickout())); }
        }

        public ICommand SetDSKickoutCommand
        {
            get { return _setDSKickoutCommand ?? (_setDSKickoutCommand = new RelayCommand(async () => await SetDSKickout())); }
        }

        //TODO команды
        public ICommand SetPSZeroCommand
        {
            get { return _setPSZeroCommand ?? (_setPSZeroCommand = new RelayCommand(async () => await SetPSZero())); }
        }

        public ICommand SetDSZeroCommand
        {
            get { return _setDSZeroCommand ?? (_setDSZeroCommand = new RelayCommand(async () => await SetDSZero())); }
        }

        public ICommand ClearSetsCommand
        {
            get { return _clearSetsCommand ?? (_clearSetsCommand = new RelayCommand(async () => await ClearSets())); }
        }

        private async Task SetPSKickout()
        {
            try
            {
                if (int.TryParse(PskoRegStr.Replace(" PSI", ""), out int pskoValue))
                {
                    await WriteRegisterAsync(4, (ushort)pskoValue); // Запись только в регистр 4
                    PskoBuf = pskoValue;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка в SetPSKickout: {ex.Message}");
            }
        }

        private async Task SetDSKickout()
        {
            try
            {
                if (int.TryParse(DskoRegStr.Replace(" PSI", ""), out int dskoValue))
                {
                    // Явное преобразование int -> ushort
                    await WriteRegisterAsync(6, (ushort)dskoValue);
                    DskoBuf = dskoValue;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка в SetDSKickout: {ex.Message}");
            }
        }

        //TODO команды
        private async Task SetPSZero()
        {
            try
            {
                await WriteRegisterAsync(1, 321); // Запись значения 321 в регистр 1
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка в SetPSZero: {ex.Message}");
            }
        }

        //TODO команды
        private async Task SetDSZero()
        {
            try
            {
                await WriteRegisterAsync(3, 322); // Запись значения 321 в регистр 1
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка в SetPSZero: {ex.Message}");
            }
        }

        //TODO команды
        private async Task ClearSets()
        {
            try
            {
                await WriteRegisterAsync(1, 333); // Запись значения 321 в регистр 1
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка в SetPSZero: {ex.Message}");
            }
        }

        public async Task StartPollingAsync()
        {
            if (_isPolling) return;

            await PollRegistersContinuously(0, 8, 400, registers =>
            {
                //NOTE Логика чтения регистров
                PsPassBuf = (short)registers[0];
                DsPassBuf = (short)registers[2];
                PskoBuf = (short)registers[4];
                DskoBuf = (short)registers[6];

            });
        }

        //public async Task WriteValuesAsync()
        //{
        //    try
        //    {
        //        // Преобразуем int в ushort (обрезаем до 16 бит)
        //        ushort pskoValue = (ushort)(PskoBuf & 0xFFFF);
        //        ushort dskoValue = (ushort)(DskoBuf & 0xFFFF);

        //        // Записываем в регистры 4 и 6 (по одному регистру на значение)
        //        await _connection.WriteMultipleRegistersAsync(
        //            slaveId: 15,
        //            startAddress: 4,
        //            new ushort[] { pskoValue, 0, dskoValue, 0 } // 0 для четных регистров
        //        );
        //    }
        //    catch (TaskCanceledException)
        //    {
        //        // Игнорируем отмену задачи
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine($"Ошибка записи: {ex.Message}");
        //    }
        //}

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

        //private float CombineRegistersToFloat(ushort highRegister, ushort lowRegister)
        //{
        //    byte[] bytes = new byte[4];
        //    BitConverter.GetBytes(highRegister).CopyTo(bytes, 0);
        //    BitConverter.GetBytes(lowRegister).CopyTo(bytes, 2);
        //    return BitConverter.ToSingle(bytes, 0);
        //}
    }
}
