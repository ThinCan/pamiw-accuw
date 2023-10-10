using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P03WeatherForecastWPF.Client.Models
{
    public class Alarm
    {
        public string AlarmType;
    }

    internal class WeatherAlarm
    {
        public string Date { get; set; }
        public Alarm Alarms;

    }
}
