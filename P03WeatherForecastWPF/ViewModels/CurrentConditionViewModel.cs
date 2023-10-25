using P03WeatherForecastWPF.Client.Models;
using P03WeatherForecastWPF.Client.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P03WeatherForecastWPF.ViewModels
{
    public class CurrentConditionViewModel : BaseViewModel
    {
        public CurrentConditionViewModel(Weather w)
        {
            _w = w;
        }

        public double Value { get => _w.Temperature.Metric.Value; }
        public string Unit { get => _w.Temperature.Metric.Unit; }
        public string WeatherText { get=> _w.WeatherText; }

        private Weather _w;
    }
}
