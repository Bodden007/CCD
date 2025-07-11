﻿using CCD.SRC;
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
                if (_minFregFmStr == value) return;

                if (float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out float result))
                {
                    if (result >= 0.0f && result <= 2000.0f)
                    {
                        _minFregFm = result;
                        _minFregFmStr = value;
                        OnPropertyChanged();
                    }
                    else
                    {
                        //FIXME Delete
                        Debug.WriteLine("Ошибка: MinFregFm выходит за пределы [0 - 2000]");
                        //TODO Можно восстановить последнее корректное значение:
                        _minFregFmStr = _minFregFm.ToString(CultureInfo.InvariantCulture);
                        OnPropertyChanged();
                    }
                }
                else
                {
                    Debug.WriteLine("Ошибка: некорректный ввод в MinFregFmStr");
                }
            }
        }
        public float MinFregFm
        {
            get => _minFregFm;
            set
            {
                if (_minFregFm == value) return;

                _minFregFm = value;
                MinFregFmStr = value.ToString(CultureInfo.InvariantCulture);
                OnPropertyChanged();
                OnPropertyChanged(nameof(MinFregFmStr)); // Важно!
            }
        }

        //NOTE Максимальное значение  чатоты
        public string MaxFregFmStr
        {
            get => _maxFregFmStr;
            set
            {
                if (_maxFregFmStr == value) return;

                if (float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out float result))
                {
                    if (result >= 0.0f && result <= 2000.0f)
                    {
                        _maxFregFm = result;
                        _maxFregFmStr = value;
                        OnPropertyChanged();
                    }
                    else
                    {
                        //FIXME Delete
                        Debug.WriteLine("Ошибка: MinFregFm выходит за пределы [0 - 2000]");
                        //TODO Можно восстановить последнее корректное значение:
                        _maxFregFmStr = _maxFregFm.ToString(CultureInfo.InvariantCulture);
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
        public float MaxFregFm
        {
            get => _maxFregFm;
            set
            {
                if (_maxFregFm == value) return;

                _maxFregFm = value;
                MaxFregFmStr = value.ToString(CultureInfo.InvariantCulture);
                OnPropertyChanged();
                OnPropertyChanged(nameof(MaxFregFmStr)); // Важно!
            }
        }

        //NOTE Минимальное значение объема
        public string MinFlowFmStr
        {
            get => _minFlowFmStr;
            set
            {
                if (_minFlowFmStr == value) return;

                if (float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out float result))
                {
                    if (result >= 0.0f && result <= 2000.0f)
                    {
                        _minFlowFm = result;
                        _minFlowFmStr = value;
                        OnPropertyChanged();
                    }
                    else
                    {
                        //FIXME Delete
                        Debug.WriteLine("Ошибка: MinFregFm выходит за пределы [0 - 2000]");
                        //TODO Можно восстановить последнее корректное значение:
                        _minFlowFmStr = _minFlowFm.ToString(CultureInfo.InvariantCulture);
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
        public float MinFlowFm
        {
            get => _minFlowFm;
            set
            {
                if (_minFlowFm == value) return;

                _minFlowFm = value;
                MinFlowFmStr = value.ToString(CultureInfo.InvariantCulture);
                OnPropertyChanged();
                OnPropertyChanged(nameof(MinFlowFmStr)); // Важно!
            }
        }

        //NOTE Максимальное значение объема
        public string MaxFlowFmStr
        {
            get => _maxFlowFmStr;
            set
            {
                if (_maxFlowFmStr == value) return;

                if (float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out float result))
                {
                    if (result >= 0.0f && result <= 2000.0f)
                    {
                        _maxFlowFm = result;
                        _maxFlowFmStr = value;
                        OnPropertyChanged();
                    }
                    else
                    {
                        //FIXME Delete
                        Debug.WriteLine("Ошибка: MinFregFm выходит за пределы [0 - 2000]");
                        //TODO Можно восстановить последнее корректное значение:
                        _maxFlowFmStr = _maxFlowFm.ToString(CultureInfo.InvariantCulture);
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
        public float MaxFlowFm
        {
            get => _maxFlowFm;
            set
            {
                if (_maxFlowFm == value) return;

                _maxFlowFm = value;
                MaxFlowFmStr = value.ToString(CultureInfo.InvariantCulture);
                OnPropertyChanged();
                OnPropertyChanged(nameof(MaxFlowFmStr)); // Важно!
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
                await WriteRegisterAsync((ushort)_regAddr.MinFregFm[0], _minFregFm);
                await WriteRegisterAsync((ushort)_regAddr.MaxFregFm[0], _maxFregFm);
                await WriteRegisterAsync((ushort)_regAddr.MinFlowFm[0], _minFlowFm);
                await WriteRegisterAsync((ushort)_regAddr.MaxFlowFm[0], _maxFlowFm);
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
                var registers = await _modbusManager.ReadInputRegistersAsync(15, (ushort)_regAddr.MinFregFm[0], 8);

                MinFregFm = ModbusUtility.GetSingle(registers[1], registers[0]);
                MaxFregFm = ModbusUtility.GetSingle(registers[3], registers[2]);
                MinFlowFm = ModbusUtility.GetSingle(registers[5], registers[4]);
                MaxFlowFm = ModbusUtility.GetSingle(registers[7], registers[6]);
             
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

            await PollRegistersContinuously((ushort)_regAddr.MixWaterRate[0], 2, 500, registers =>
            {
                MixWaterRate = ModbusUtility.GetSingle(registers[1], registers[0]);                
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
