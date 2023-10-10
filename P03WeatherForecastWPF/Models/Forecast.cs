using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P03WeatherForecastWPF.Client.Models
{
    public class ForecastTemperature
    {
        public string Unit;
        public double Value;
    }
    internal class Forecast
    {
        public long EpochTime { get; set; }
        public string DateTime { get; set; }
        public string IconPhrase { get; set; }
        public ForecastTemperature Temperature { get; set; }
    }
}
