using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCD.SRC
{
    internal class ModbusConfig
    {
        public int SlaveId { get; set; }
        public int PollingInterval { get; set; }
        public RegisterAddressConfig RegisterAddress { get; set; }
        public static ModbusConfig Load()
        {
            var configPath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Configs", "modbus_config.json");
            var json = File.ReadAllText(configPath);
            return JsonConvert.DeserializeObject<ModbusConfig>(json);
        }
    }
    public class RegisterAddressConfig
    {
        public int PsPass { get; set; }
        public int DsPass { get; set; }
        public int PsKo { get; set; }
        public int DsKo { get; set; }
        public int PsKoStatus { get; set; }
        public int DsKoStatus { get; set; }
        public List<int> PsRate { get; set; }
        public List<int> DsRate { get; set; }
        public List<int> RateCombined { get; set; }
        public List<int> PsRateStage { get; set; }
        public List<int> DsRateStage { get; set; }
        public List<int> RateStageTotal { get; set; }
        public List<int> MixWaterRate { get; set; }
        public List<int> StageTotalWTR { get; set; }
        public List<int> JobTotalWTR { get; set; }
        public List<int> RecircDensity { get; set; }
        public List<int> RecircDensityRate { get; set; }
        public List<int> DownholeDensity { get; set; }
        public List<int> LevelSensor { get; set; }
        public int Cmt { get; set; }
        public int Wtr { get; set; }
        public int ValveControl { get; set; }
        public int CmtSend { get; set; }
        public int WtrSend { get; set; }
        public int ModeLevel { get; set; }
        public int SetLevel { get; set; }
        public List<int> MinCurrentPs {  get; set; }
        public List<int> MaxCurrentPs { get; set; }
        public List<int> MinPressurePs { get; set; }
        public List<int> MaxPressurePs { get; set; }
        public List<int> MinCurrentDs { get; set; }
        public List<int> MaxCurrentDs { get; set; }
        public List<int> MinPressureDs { get; set; }
        public List<int> MaxPressureDs { get; set; }
        public List<int> MinFregFm { get; set; }
        public List<int> MaxFregFm { get; set; }
        public List<int> MinFlowFm { get; set; }
        public List<int> MaxFlowFm { get; set; }
        public List<int> MinFregPs { get; set; }
        public List<int> MaxFregPs { get; set; }
        public List<int> MinFlowPs { get; set; }
        public List<int> MaxFlowPs { get; set; }
        public List<int> MinFregDs { get; set; }
        public List<int> MaxFregDs { get; set; }
        public List<int> MinFlowDs { get; set; }
        public List<int> MaxFlowDs { get; set; }
    }
}
