using NModbus.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCD.Models.ModelsForms
{
    internal class MainWindowViewModel : ModbusWindowViewModel
    {
        private int _psPassBuf;
        private int _dsPassBuf;
        private int _pskoBuf;
        private int _dskoBuf;
        //private float _psRate;
        //private float _dsRate;
        private float _rateStageTotal;
        private float _recircDensity;
        private float _recircDensityRate;
        private float _downholeDensityBuf;
        private float _mixWaterRate;
        private float _levelSensor;
        private string _levelSensoreStr = "N/A";
        //private double _progressBarValue;
        private string _pSPass = "N/A";
        private string _dSPass = "N/A";
        private string _psko = "N/A";
        private string _dsko = "N/A";
        private string _downholeDensity = "N/A";


        //NOTE изменение цвета фона
        private string _windowBackground = "#CDC771"; // Стандартный цвет

        public string WindowBackground
        {
            get => _windowBackground;
            set
            {
                if (_windowBackground != value)
                {
                    _windowBackground = value;
                    OnPropertyChanged(nameof(WindowBackground));
                }
            }
        }

        //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

        //FIXME
        private int _psPassBufint;

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

        //private string _register2Text = "N/A";
        //!!!!!!!!!!!!!!!!!!!!!
        public string PsPass
        {
            get => _pSPass;
            set { _pSPass = value; OnPropertyChanged(); }
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
            get => _psko;
            set { _psko = value; OnPropertyChanged(); }
        }

        public int PskoBuf
        {
            get => _pskoBuf;
            set
            {
                _pskoBuf = value;
                PSKO = $"OPKO = {value} PSI";
                OnPropertyChanged();
            }
        }

        public string DSKO
        {
            get => _dsko;
            set { _dsko = value; OnPropertyChanged(); }
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

        public float RateStageTotal
        {
            get => _rateStageTotal;
            set { _rateStageTotal = value; OnPropertyChanged(); }
        }

        public float RecircDensity
        {
            get => _recircDensity;
            set { _recircDensity = value; OnPropertyChanged(); }
        }

        public float RecircDensityRate
        {
            get => _recircDensityRate;
            set { _recircDensityRate = value; OnPropertyChanged(); }
        }


        //NOTE плотность выход

        public string DownholeDensity
        {
            get => _downholeDensity;
            set { _downholeDensity = value; OnPropertyChanged(); }
        }
        public float DownholeDensityBuf
        {
            get => _downholeDensityBuf;
            set
            {
                _downholeDensityBuf = value;
                DownholeDensity = value == 22222 ? "ERROR" : $"{value} ppg";
                OnPropertyChanged();
            }
        }

        //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        public float MixWaterRate
        {
            get => _mixWaterRate;
            set { _mixWaterRate = value; OnPropertyChanged(); }
        }

        public string LevelSensoreStr
        {
            get => _levelSensoreStr;
            set
            {
                _levelSensoreStr = value;
                OnPropertyChanged();
            }
        }

        //public double ProgressBarValue
        //{
        //    get => _progressBarValue;
        //    set
        //    {
        //        if (_progressBarValue != value)
        //        {
        //            _progressBarValue = value;
        //            OnPropertyChanged();
        //        }
        //    }
        //}
        public float LevelSensore
        {
            get => _levelSensor;
            set
            {
                _levelSensor = value;
                LevelSensoreStr = value == 22222 ? "ERROR" : $"{value:N2} ft";
                //ProgressBarValue = value;
                OnPropertyChanged();
            }
        }

        public async Task StartPollingAsync()
        {
            if (_isPolling) return;

            await PollRegistersContinuously(0, 56, 400, registers =>
            {

                PsPassBuf = (short)registers[0];
                DsPassBuf = (short)registers[2];
                PskoBuf = (short)registers[4];
                DskoBuf = (short)registers[6];

                ushort[] level = new ushort[] { registers[31], registers[30] };

                LevelSensore = ModbusUtility.GetSingle(level[0], level[1]);

                // Проверяем регистр 5 (значение 121) и регистр 7 (значение 122)
                bool shouldTurnRed = ((short)registers[5] == 121) || ((short)registers[7] == 122);

                // Меняем цвет фона
                WindowBackground = shouldTurnRed ? "Red" : "White";

                ushort[] registr = new ushort[] { registers[25], registers[24] };

                DownholeDensityBuf = ModbusUtility.GetSingle(registr[0], registr[1]);


                //if (registers.Length >= 4)
                //{

                //    PsPassBuf = CombineRegistersToFloat(registers[0], registers[1]);
                //    DsPassBuf = CombineRegistersToFloat(registers[2], registers[3]);
                //    PskoBuf = CombineRegistersToFloat(registers[4], registers[5]);
                //    DskoBuf = CombineRegistersToFloat(registers[6], registers[7]);
                //}
            });
        }

        private float CombineRegistersToFloat(ushort highRegister, ushort lowRegister)
        {
            byte[] bytes = new byte[4];
            BitConverter.GetBytes(highRegister).CopyTo(bytes, 0);
            BitConverter.GetBytes(lowRegister).CopyTo(bytes, 2);
            return BitConverter.ToSingle(bytes, 0);
        }
    }
}
