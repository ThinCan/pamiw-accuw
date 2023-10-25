using P03WeatherForecastWPF.Client.Models;
using P03WeatherForecastWPF.Client.ViewModels;

namespace P03WeatherForecastWPF.ViewModels
{
    public class CityWeatherAlarmViewModel : BaseViewModel
    {
        public CityWeatherAlarmViewModel(WeatherAlarm alarm)
        {
            _alarm = alarm;
        }

        public string Date { get =>_alarm.Date; }
        private WeatherAlarm _alarm;
    }
}
