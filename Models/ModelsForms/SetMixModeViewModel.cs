using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Media3D;

namespace CCD.Models.ModelsForms
{
    internal class SetMixModeViewModel : ModbusWindowViewModel
    {
        private RelayCommand _densityControlMode;
        private RelayCommand _densityHeightMode;
        private RelayCommand _densitySensorTotals;

        private bool _modeUser;
        private bool _modeDensty;
        private bool _modeTubHeight;
        private int _selectedMixMode;

        public bool ModeUser
        {
            get => _modeUser;
            set
            {
                if (_modeUser != value)
                {
                    _modeUser = value;
                    if (value) SelectedMixMode = 11;
                    OnPropertyChanged();
                }
            }
        }
        public bool ModeDensty
        {
            get => _modeDensty;
            set
            {
                if (_modeDensty != value)
                {
                    _modeDensty = value;
                    if (value) SelectedMixMode = 12;
                    OnPropertyChanged();
                }
            }
        }
        public bool ModeTubHeight
        {
            get => _modeTubHeight;
            set
            {
                if (_modeTubHeight != value)
                {
                    _modeTubHeight = value;
                    if (value) SelectedMixMode = 13;
                    OnPropertyChanged();
                }
            }
        }
        public int SelectedMixMode
        {
            get => _selectedMixMode;
            set
            {
                if (_selectedMixMode != value)
                {
                    _selectedMixMode = value;
                    OnPropertyChanged();

                    // Обновляем состояния кнопок
                    ModeUser = (value == 11);
                    ModeDensty = (value == 12);
                    ModeTubHeight = (value == 13);
                }
            }
        }
        /// <summary>
        /// Команда установки отключения Sensor Level
        /// </summary>
        public ICommand DensityControlModeCommand
        {
            get { return _densityControlMode ?? (_densityControlMode = new RelayCommand(async () => await DensityControlMode())); }
        }
        private async Task DensityControlMode()
        {

            try
            {
                await WriteRegisterAsync(48, (ushort)11); // Команда отключения
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка в SetPSZero: {ex.Message}");
            }
        }
        /// <summary>
        /// Команда для визуального контроля Sensor Level
        /// </summary>
        public ICommand DensityHeightModeCommand
        {
            get { return _densityHeightMode ?? (_densityHeightMode = new RelayCommand(async () => await DensityHeightMode())); }
        }
        private async Task DensityHeightMode()
        {

            try
            {
                await WriteRegisterAsync(48, (ushort)12); //Команда Визуального контроля
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка в SetPSZero: {ex.Message}");
            }
        }
        public ICommand DensitySensorTotalsCommand
        {
            get { return _densitySensorTotals ?? (_densitySensorTotals = new RelayCommand(async () => await DensitySensorTotals())); }
        }
        private async Task DensitySensorTotals()
        {

            try
            {
                await WriteRegisterAsync(48, (ushort)13); //Команда полного контроля
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка в SetPSZero: {ex.Message}");
            }
        }
        public async Task StartPollingAsync()
        {
            if (_isPolling) return;

            await PollRegistersContinuously(47, 1, 1000, registers =>
            {
                SelectedMixMode = (short)registers[0];
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
