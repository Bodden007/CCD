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
        private string _pSPassStr = "N/A";
        private int _dsPass;
        private string _dSPassStr = "N/A";
        private int _psko;
        private string _pskoStr = "N/A";
        private int _dsko;
        private string _dskoStr = "N/A";
        private float _psRate;
        private string _psRateStr = "N/A";
        private float _dsRate;
        private string _dsRateStr = "N/A";
        private float _rateCombined;
        private string _rateCombinedStr = "N/A";
        private float _psRateStage;
        private string _psRateStageStr = "N/A";
        private float _dsRateStage;
        private string _dsRateStageStr = "N/A";
        private float _rateStageTotal;
        private string _rateStageTotalStr = "N/A";
        private float _recircDensity;
        private string _recircDensityStr = "N/A";
        private float _recircDensityRate;
        private string _recircDensityRateStr = "N/A";
        private float _downholeDensity;
        private string _downholeDensityStr = "N/A";
        private float _mixWaterRate;
        private string _mixWaterRateStr = "N/A";
        private float _stageTotalWTR;
        private string _stageTotalWTRStr = "N/A";
        private float _jobTotalWTR;
        private string _jobTotalWTRStr = "N/A";
        private float _levelSensor;
        private string _totalWaterRateStr = "N/A";
        private string _levelSensoreStr = "N/A";
        private int _cmt;
        private string _cmtStr = "N/A";
        private int _wtr;
        private string _wtrStr = "N/A";

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
        public string DownholeDensityStr
        {
            get => _downholeDensityStr;
            set { _downholeDensityStr = value; OnPropertyChanged(); }
        }
        public float DownholeDensity
        {
            get => _downholeDensity;
            set
            {
                _downholeDensity = value;
                DownholeDensityStr = value == 22222 ? "ERROR" : $"{value:N2} ppg";
                OnPropertyChanged();
            }
        }

        //NOTE Поток жидкости через Flow Meter gpm
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

        //NOTE Stage расход через Flow Meter в gal
        public string StageTotalWTRStr
        {
            get => _stageTotalWTRStr;
            set { _stageTotalWTRStr = value; OnPropertyChanged(); }
        }
        public float StageTotalWTR
        {
            get => _stageTotalWTR;
            set
            {
                _stageTotalWTR = value;
                StageTotalWTRStr = $"{value:N2} gal";
                OnPropertyChanged();
            }
        }

        //NOTE Job расход через Flow Meter в gal
        public string JobTotalWTRStr
        {
            get => _jobTotalWTRStr;
            set { _jobTotalWTRStr = value; OnPropertyChanged(); }
        }
        public float JobTotalWTR
        {
            get => _jobTotalWTR;
            set
            {
                _jobTotalWTR = value;
                JobTotalWTRStr = $"{value:N2} gal";
                OnPropertyChanged();
            }
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

        //NOTE Позиция цементного дозатора
        public string CMTStr
        {
            get => _cmtStr;
            set { _cmtStr = value; OnPropertyChanged(); }
        }
        public int CMT
        {
            get => _cmt;
            set
            {
                _cmt = value;
                CMTStr = $"CMT= {value} %";
                OnPropertyChanged();
            }
        }

        //NOTE Позиция водяного дозатора
        public string WTRStr
        {
            get => _wtrStr;
            set { _wtrStr = value; OnPropertyChanged(); }
        }
        public int WTR
        {
            get => _wtr;
            set
            {
                _wtr = value;
                WTRStr = $"WTR= {value} %";
                OnPropertyChanged();
            }
        }

        public async Task StartPollingAsync()
        {
            if (_isPolling) return;

            await PollRegistersContinuously(0, 50, 500, registers =>
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

                MixWaterRate = ModbusUtility.GetSingle(registers[23], registers[22]);

                StageTotalWTR = ModbusUtility.GetSingle(registers[25], registers[24]);

                JobTotalWTR = ModbusUtility.GetSingle(registers[27], registers[26]);

                RecircDensity = ModbusUtility.GetSingle(registers[29], registers[28]);

                RecircDensityRate = ModbusUtility.GetSingle(registers[31], registers[30]);

                DownholeDensity = ModbusUtility.GetSingle(registers[33], registers[32]);                               

                LevelSensor = ModbusUtility.GetSingle(registers[39], registers[38]);

                CMT = (short)registers[40];
                WTR = (short)registers[41];

                // Проверяем регистр 5 (значение 121) и регистр 7 (значение 122)
                bool shouldTurnRed = ((short)registers[5] == 121) || ((short)registers[7] == 122);

                // Меняем цвет фона
                WindowBackground = shouldTurnRed ? "Red" : "#F5F9FF";
                MixControlBackground = shouldTurnRed ? "Red" : "#C4B454";
            });
        }        
    }
}
