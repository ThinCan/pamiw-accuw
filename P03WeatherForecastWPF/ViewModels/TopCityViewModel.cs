using P03WeatherForecastWPF.Client.Models;
using P03WeatherForecastWPF.Client.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P03WeatherForecastWPF.ViewModels
{
    public class TopCityViewModel : BaseViewModel
    {
        public TopCityViewModel(City c)
        {
            _city = c;
        }

        public string localizedName { get =>_city.LocalizedName; }
        public Country country { get => _city.Country; }

        private City _city;
    }
}
