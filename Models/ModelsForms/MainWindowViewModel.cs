using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCD.Models.ModelsForms
{
    internal class MainWindowViewModel : ModbusWindowViewModel
    {
        private float _psPassBuf;
        private float _dsPassBuf;
        private float _pskoBuf;
        private float _dskoBuf;
        private float _psRate;
        private float _dsRate;
        private float _rateStageTotal;
        private float _recircDensity;
        private float _recircDensityRate;
        private float _downholeDensity;
        private float _mixWaterRate;
        private string _pSPass = "N/A";
        private string _dSPass = "N/A";
        private string _psko = "N/A";
        private string _dsko = "N/A";

        //FIXME
        private int _psPassBufint;

        public int PsPassBufint
        {
            get => _psPassBufint;
            set
            {
                _psPassBufint = value;
                PsPass = value ==22222 ? "ERROR" : $"{value} PSI";
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


        //FIXME раскоментируй
        //public float PsPassBuf
        //{
        //    get => _psPassBuf;
        //    set
        //    {
        //        _psPassBuf = value;
        //        PsPass = $"{value:F2} PSI";
        //        OnPropertyChanged();
        //    }
        //}

        public float DsPassBuf
        {
            get => _dsPassBuf;
            set
            {
                _dsPassBuf = value;
                DsPass = $"{value:F2} PSI";
                OnPropertyChanged();
            }
        }

        public string PSKO
        {
            get => _psko;
            set { _psko = value; OnPropertyChanged(); }
        }

        public float PskoBuf
        {
            get => _pskoBuf;
            set { _pskoBuf = value; 
                PSKO = $"OPKO = {value:F2} PSI";
                OnPropertyChanged(); 
            }
        }

        public string DSKO
        {
            get => _dsko;
            set { _dsko = value; OnPropertyChanged(); }
        }
        public float DskoBuf
        {
            get => _dskoBuf;
            set { _dskoBuf = value;
                DSKO = $"OPKO = {value:F2} PSI";
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

        public float DownholeDensity
        {
            get => _downholeDensity;
            set { _downholeDensity = value; OnPropertyChanged(); }
        }

        public float MixWaterRate
        {
            get => _mixWaterRate;
            set { _mixWaterRate = value; OnPropertyChanged(); }
        }

        public async Task StartPollingAsync()
        {
            if (_isPolling) return;

            await PollRegistersContinuously(0, 1, 100, registers =>
            {

                PsPassBufint = (short)registers[0];
                if (registers.Length >= 4)
                {
                    
                    //PsPassBuf = CombineRegistersToFloat(registers[0], registers[1]);
                    //DsPassBuf = CombineRegistersToFloat(registers[2], registers[3]);
                    //PskoBuf = CombineRegistersToFloat(registers[4], registers[5]);
                    //DskoBuf = CombineRegistersToFloat(registers[6], registers[7]);
                }
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
