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
        private int _psPass;
        private int _dsPass;
        private int _psko;
        private int _dsko;
        private float _psRate;
        private float _dsRate;
        private float _rateCombined;
        private float _psRateStage;
        private float _dsRateStage;
        private float _rateStageTotal;
        private float _recircDensity;
        private float _recircDensityRate;
        private float _downholeDensityBuf;
        private float _mixWaterRate;
        private float _levelSensor;
        private string _levelSensoreStr = "N/A";

        private string _pSPassStr = "N/A";
        private string _dSPassStr = "N/A";
        private string _pskoStr = "N/A";
        private string _dskoStr = "N/A";
        private string _psRateStr = "N/A";
        private string _dsRateStr = "N/A";
        private string _rateCombinedStr = "N/A";
        private string _psRateStageStr = "N/A";
        private string _dsRateStageStr = "N/A";
        private string _rateStageTotalStr = "N/A";
        private string _recircDensityStr = "N/A";
        private string _recircDensityRateStr = "N/A";
        private string _mixWaterRateStr = "N/A";
        private string _downholeDensity = "N/A";

        //NOTE изменение цвета фона
        private string _windowBackground = "#F5F9FF"; // Стандартный цвет
        private string _mixControlBackground = "#C4B454"; //цвет панели  Mix Control

        //NOTE Смена цвета при высоком давлении
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
        public string MixControlBackground
        {
            get => _mixControlBackground;
            set
            {
                if (_mixControlBackground != value)
                {
                    _mixControlBackground = value;
                    OnPropertyChanged(nameof(MixControlBackground));
                }
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

        //NOTE давление водительской стороны
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

        //NOTE уставка отсечки пассажирской стороны
        public string PSKOStr
        {
            get => _pskoStr;
            set { _pskoStr = value; OnPropertyChanged(); }
        }
        public int PSKO
        {
            get => _psko;
            set
            {
                _psko = value;
                PSKOStr = $"OPKO = {value} psi";
                OnPropertyChanged();
            }
        }

        //NOTE уставка отсечки водительской стороны
        public string DSKOStr
        {
            get => _dskoStr;
            set { _dskoStr = value; OnPropertyChanged(); }
        }
        public int DSKO
        {
            get => _dsko;
            set
            {
                _dsko = value;
                DSKOStr = $"OPKO = {value} psi";
                OnPropertyChanged();
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

        //NOTE расход насоса водительская сторона
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

        //NOTE комбинированный расход
        public string RateCombinedStr
        {
            get => _rateCombinedStr;
            set
            {
                _rateCombinedStr = value;
                OnPropertyChanged();
            }
        }
        public float RateCombined
        {
            get => _rateCombined;
            set
            {
                _rateCombined = value;
                RateCombinedStr = $"{value} bpm";
                OnPropertyChanged();
            }
        }

        //NOTE Общий расход пассажирская сторона
        public string PsRateStageStr
        {
            get => _psRateStageStr;
            set { _psRateStageStr = value; OnPropertyChanged(); }
        }
        public float PsRateStage
        {
            get => _psRateStage;
            set
            {
                _psRateStage = value;
                PsRateStageStr = $"{value:N2} bbl";
                OnPropertyChanged();
            }
        }

        //NOTE Общий расход водительская сторона
        public string DsRateStageStr
        {
            get => _dsRateStageStr;
            set { _dsRateStageStr = value; OnPropertyChanged(); }
        }
        public float DsRateStage
        {
            get => _dsRateStage;
            set
            {
                _dsRateStage = value;
                DsRateStageStr = $"{value:N2} bbl";
                OnPropertyChanged();
            }
        }

        //NOTE Общий сумарный расход 
        public string RateStageTotalStr
        {
            get => _rateStageTotalStr;
            set { _rateStageTotalStr = value; OnPropertyChanged(); }
        }
        public float RateStageTotal
        {
            get => _rateStageTotal;
            set
            {
                _rateStageTotal = value;
                RateStageTotalStr = $"{value:N2} bbl";
                OnPropertyChanged();
            }
        }

        //NOTE Плотность линии рециркуляции
        public string RecircDensityStr
        {
            get => _recircDensityStr;
            set
            {
                _recircDensityStr = value; OnPropertyChanged();
            }
        }
        public float RecircDensity
        {
            get => _recircDensity;
            set
            {
                _recircDensity = value;
                RecircDensityStr = $"{value:N2} ppg";
                OnPropertyChanged();
            }
        }

        //NOTE Поток линии рециркуляции
        public string RecircDensityRateStr
        {
            get => _recircDensityRateStr;
            set { _recircDensityRateStr = value; OnPropertyChanged(); }
        }

        public float RecircDensityRate
        {
            get => _recircDensityRate;
            set
            {
                _recircDensityRate = value;
                RecircDensityRateStr = $"{value:N2} gpm";
                OnPropertyChanged();
            }
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
                DownholeDensity = value == 22222 ? "ERROR" : $"{value:N2} ppg";
                OnPropertyChanged();
            }
        }

        //NOTE Поток жидкости через Flow Meter
        public string MixWaterRateStr
        {
            get => _mixWaterRateStr;
            set { _mixWaterRateStr = value; OnPropertyChanged(); }
        }
        public float MixWaterRate
        {
            get => _mixWaterRate;
            set { 
                _mixWaterRate = value;
                MixWaterRateStr = $"{value:N2} gpm";
                OnPropertyChanged(); }
        }

        //NOTE Уровень RCM
        public string LevelSensoreStr
        {
            get => _levelSensoreStr;
            set
            {
                _levelSensoreStr = value;
                OnPropertyChanged();
            }
        }

        //TODO доделать уровня
        public float LevelSensor
        {
            get => _levelSensor;
            set
            {
                _levelSensor = value;
                LevelSensoreStr = value == 22222 ? "ERROR" :
                 value == 22221 ? "Off" :
                 $"{value:N2} ft";
                OnPropertyChanged();
            }
        }

        public async Task StartPollingAsync()
        {
            if (_isPolling) return;

            await PollRegistersContinuously(0, 56, 400, registers =>
            {

                PsPass = (short)registers[0];
                DsPass = (short)registers[2];
                PSKO = (short)registers[4];
                DSKO = (short)registers[6];

                PsRate = ModbusUtility.GetSingle(registers[9], registers[8]);

                DsRate = ModbusUtility.GetSingle(registers[11], registers[10]);

                RateCombined = ModbusUtility.GetSingle(registers[13], registers[12]);

                PsRateStage = ModbusUtility.GetSingle(registers[15], registers[14]);

                DsRateStage = ModbusUtility.GetSingle(registers[17], registers[16]);

                RateStageTotal = ModbusUtility.GetSingle(registers[19], registers[18]);

                RecircDensity = ModbusUtility.GetSingle(registers[21], registers[20]);

                RecircDensityRate = ModbusUtility.GetSingle(registers[23], registers[22]);

                DownholeDensityBuf = ModbusUtility.GetSingle(registers[25], registers[24]);

                MixWaterRate = ModbusUtility.GetSingle(registers[27], registers[26]);

                LevelSensor = ModbusUtility.GetSingle(registers[31], registers[30]);

                // Проверяем регистр 5 (значение 121) и регистр 7 (значение 122)
                bool shouldTurnRed = ((short)registers[5] == 121) || ((short)registers[7] == 122);

                // Меняем цвет фона
                WindowBackground = shouldTurnRed ? "Red" : "#F5F9FF";
                MixControlBackground = shouldTurnRed ? "Red" : "#C4B454";
            });
        }

        //FIXME Удалить
        //private float CombineRegistersToFloat(ushort highRegister, ushort lowRegister)
        //{
        //    byte[] bytes = new byte[4];
        //    BitConverter.GetBytes(highRegister).CopyTo(bytes, 0);
        //    BitConverter.GetBytes(lowRegister).CopyTo(bytes, 2);
        //    return BitConverter.ToSingle(bytes, 0);
        //}
    }
}
