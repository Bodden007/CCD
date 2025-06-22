using NModbus.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CCD.Models.ModelsForms
{
    internal class ManualControlViewModel : ModbusWindowViewModel
    {
        private Window _window;  // Добавляем поле для хранения окна

        // Конструктор теперь принимает окно
        public ManualControlViewModel(Window window)
        {
            _window = window;
        }

        private RelayCommand _oscillateCements;
        private RelayCommand _oscillateBoth;
        private RelayCommand _oscillateWater;
        private RelayCommand _openAllValves;
        private RelayCommand _closeAllValvesExit;
        private RelayCommand _updateCementCommand;
        private RelayCommand _updateWaterCommand;

        private int _cmt;
        private string _cmtStr = "N/A";
        private int _wtr;        
        private string _wtrStr = "N/A";

        private string _cmtReg = "0";
        private string _wtrReg = "0";

        public string CMTReg
        {
            get => _cmtReg;
            set
            {
                // Проверяем, что вводится число от 0 до 100
                if (int.TryParse(value, out int num) && num >= 0 && num <= 100)
                {
                    _cmtReg = value;
                    OnPropertyChanged();
                }
                else if (string.IsNullOrEmpty(value))
                {
                    _cmtReg = "0"; // Устанавливаем 0 если поле пустое
                    OnPropertyChanged();
                }
            }
        }

        public string WTRReg
        {
            get => _wtrReg;
            set
            {
                // Проверяем, что вводится число от 0 до 100
                if (int.TryParse(value, out int num) && num >= 0 && num <= 100)
                {
                    _wtrReg = value;
                    OnPropertyChanged();
                }
                else if (string.IsNullOrEmpty(value))
                {
                    _wtrReg = "0"; // Устанавливаем 0 если поле пустое
                    OnPropertyChanged();
                }
            }
        }

        //NOTE Позиция цементного дозатора
        public string CMTStr
        {
            get => _cmtStr;
            set{ _cmtStr = value; OnPropertyChanged(); }
        }
        public int CMT
        {
            get => _cmt;
            set
            {
                _cmt = value;
                CMTStr = $"CMT= {value}.0%";
                OnPropertyChanged();
            }
        }

        //NOTE Позиция водяного дозатора
        public string WTRStr
        {
            get => _wtrStr;
            set{_wtrStr = value; OnPropertyChanged(); }
        }
        public int WTR
        {
            get => _wtr;
            set
            {
                _wtr = value;
                WTRStr = $"WTR= {value}.0%";
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Команда запуска генератора цем. дозатора
        /// </summary>
        public ICommand OscillateCementsCommand
        {
            get { return _oscillateCements ?? (_oscillateCements = new RelayCommand(async () => await OscillateCements())); }
        }
        private async Task OscillateCements()
        {

            try
            {
                await WriteRegisterAsync(62, 221); // Запись значения 221 в регистр 54 valveControl
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка в SetPSZero: {ex.Message}");
            }
        }

        /// <summary>
        /// Команда запуска генератора водяного и цем дозаторов
        /// </summary>
        public ICommand OscillateBothCommand
        {
            get { return _oscillateBoth ?? (_oscillateBoth = new RelayCommand(async () => await OscillateBoth())); }
        }
        private async Task OscillateBoth()
        {
            try
            {
                await WriteRegisterAsync(62, 222); // Запись значения 222 в регистр 54 valveControl
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка в SetPSZero: {ex.Message}");
            }
        }

        /// <summary>
        /// Команда запуска водяного дозатора
        /// </summary>
        public ICommand OscillateWaterCommand
        {
            get { return _oscillateWater ?? (_oscillateWater = new RelayCommand(async () => await OscillateWater())); }
        }
        private async Task OscillateWater()
        {
            try
            {
                await WriteRegisterAsync(62, 223); // Запись значения 223 в регистр 54 valveControl
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка в SetPSZero: {ex.Message}");
            }
        }
        /// <summary>
        /// Команда открытия всех дозаторов
        /// </summary>
        public ICommand OpenAllValvesCommand
        {
            get { return _openAllValves ?? (_openAllValves = new RelayCommand(async () => await OpenAllValves())); }
        }
        private async Task OpenAllValves()
        {
            try
            {
                await WriteRegisterAsync(62, 224); // Запись значения 224 в регистр 54 valveControl
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка в SetPSZero: {ex.Message}");
            }
        }

        /// <summary>
        /// Команда закрытия всех дозаторов и выход с экрана Manual Control
        /// </summary>
        public ICommand CloseAllValvesExitCommand
        {
            get { return _closeAllValvesExit ?? (_closeAllValvesExit = new RelayCommand(async () => await CloseAllValvesExit())); }
        }
        private async Task CloseAllValvesExit()
        {
            try
            {
                await WriteRegisterAsync(62, 225); // Запись значения 225 в регистр 54 valveControl
                _window?.Close();                   // Закрываем окно
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка в SetPSZero: {ex.Message}");
            }
            finally
            {
                _window?.Close();
            }
        }

        /// <summary>
        /// Команда обновления значений цем дозатора
        /// </summary>
        public ICommand UpdateCementCommand
        {
            get { return _updateCementCommand ?? (_updateCementCommand = new RelayCommand(async () => await UpdateCement())); }
        }
        private async Task UpdateCement()
        {
            try
            {
                if (int.TryParse(CMTReg, out int cmtValue) && cmtValue >= 0 && cmtValue <= 100)
                {
                    await WriteRegisterAsync(62, 0); // Закрыть все клапаны
                    await WriteRegisterAsync(63, (ushort)cmtValue); // Установить новое значение
                    /*CMTBuf = cmtValue;*/ // Обновить отображаемое значение
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"UpdateCement error: {ex.Message}");
                // Можно добавить автоматический сброс при ошибке
                CMTReg = "0";
            }
        }

        /// <summary>
        /// Команда обновления значений водяного дозатора
        /// </summary>
        public ICommand UpdateWaterCommand
        {
            get { return _updateWaterCommand ?? (_updateWaterCommand = new RelayCommand(async () => await UpdateWater())); }

        }
        private async Task UpdateWater()
        {
            try
            {
                if (int.TryParse(WTRReg, out int wtrValue) && wtrValue >= 0 && wtrValue <= 100)
                {
                    await WriteRegisterAsync(62, 0); // Закрыть все клапаны
                    await WriteRegisterAsync(64, (ushort)wtrValue); // Установить новое значение
                    /*CMTBuf = cmtValue;*/ // Обновить отображаемое значение
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка в SetPSZero: {ex.Message}");
            }
        }
        public async Task StartPollingAsync()
        {
            if (_isPolling) return;

            await PollRegistersContinuously(60, 4, 400, registers =>
            {
                CMT = (short)registers[0];
                WTR = (short)registers[1];

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
                Debug.WriteLine($"Ошибка записи в регистр {register}: {ex.Message}");
                throw;
            }
        }
    }
}
