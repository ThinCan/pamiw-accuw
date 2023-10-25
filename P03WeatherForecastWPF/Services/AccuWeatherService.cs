using Newtonsoft.Json;
using P03WeatherForecastWPF.Client.Models;
using P03WeatherForecastWPF.ViewModels;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace P03WeatherForecastWPF.Client.Services
{
    internal class AccuWeatherService
    {
        private const string base_url = "http://dataservice.accuweather.com";
        private const string autocomplete_endpoint = "locations/v1/cities/autocomplete?apikey={0}&q={1}&language{2}";
        private const string current_conditions_endpoint = "currentconditions/v1/{0}?apikey={1}&language={2}";
        private const string city_endpoint = "locations/v1/cities/search?q={0}&apikey={1}&language={2}";
        private const string city_alarms_endpoint = "alarms/v1/1day/{0}?apikey={1}&language={2}";
        private const string city_alarms_10d_endpoint = "alarms/v1/10day/{0}?apikey={1}&language={2}";
        private const string ip_adress_endpoint = "locations/v1/cities/ipaddress/?q={0}&apikey={1}&language={2}";
        private const string top_10_cities_endpoint = "locations/v1/topcities/50?apikey={0}&language={1}";
        private const string twelve_hour_forecast_endpoint = "forecasts/v1/hourly/12hour/{0}?apikey={1}&language={2}&metric=true";
        private const string city_neighbors_endpoint = "locations/v1/cities/neighbors/{0}?apikey={1}&language={2}";


        //private const string api_key = "MvuyGXombrGtGxOuNDUGvgMqyxjn63sn";
        private const string api_key = "i6yZECFpoTpuAjgBM8XivFnyQxYwZE2q";
        //string api_key;
        private const string language = "pl";
        //string language;

        public AccuWeatherService()
        {

        }

        public async Task<City[]> GetLocations(string locationName)
        {
            string uri = base_url + "/" + string.Format(autocomplete_endpoint, api_key, locationName, language);
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(uri);
                string json = await response.Content.ReadAsStringAsync();
                City[] cities = JsonConvert.DeserializeObject<City[]>(json);
                return cities;
            }
        }

        public async Task<CurrentConditionViewModel> GetCurrentConditions(string cityKey)
        {
            string uri = base_url + "/" + string.Format(current_conditions_endpoint, cityKey, api_key, language);
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(uri);
                string json = await response.Content.ReadAsStringAsync();
                Weather[] weathers = JsonConvert.DeserializeObject<Weather[]>(json);
                CurrentConditionViewModel ccvm = new CurrentConditionViewModel(weathers.FirstOrDefault());
                return ccvm;
            }
        }

        public async Task<City> GetCity(string name)
        {
            string uri = base_url + "/" + string.Format(city_endpoint, name, api_key, language);
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(uri);
                string json = await response.Content.ReadAsStringAsync();
                City[] cities = JsonConvert.DeserializeObject<City[]>(json);
                if (cities == null || cities.Length == 0) { return null; }
                return cities[0];
            }
        }

        public async Task<NeighborCityViewModel[]> GetCityNeighbors(City city)
        {
            string uri = base_url + "/" + string.Format(city_neighbors_endpoint, city.Key, api_key, language);
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(uri);
                string json = await response.Content.ReadAsStringAsync();
                City[] cities = JsonConvert.DeserializeObject<City[]>(json);
                NeighborCityViewModel[] ncities = new NeighborCityViewModel[cities.Length];
                for(int i=0;i<cities.Length; i++) { ncities[i] = new NeighborCityViewModel(cities[i]); }
                return ncities;
            }
        }

        public async Task<CityWeatherAlarmViewModel[]> GetCityAlarms(City city)
        {
            string uri = base_url + "/" + string.Format(city_alarms_endpoint, city.Key, api_key, language);
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(uri);
                string json = await response.Content.ReadAsStringAsync();
                WeatherAlarm[] alarms = JsonConvert.DeserializeObject<WeatherAlarm[]>(json);
                CityWeatherAlarmViewModel[] ncities = new CityWeatherAlarmViewModel[alarms.Length];
                for(int i=0;i<alarms.Length; i++) { ncities[i] = new CityWeatherAlarmViewModel(alarms[i]); }
                return ncities;
            }
        }

        public async Task<CityWeatherAlarmViewModel[]> GetCityAlarms_10Days(City city)
        {
            string uri = base_url + "/" + string.Format(city_alarms_10d_endpoint, city.Key, api_key, language);
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(uri);
                string json = await response.Content.ReadAsStringAsync();
                WeatherAlarm[] alarms = JsonConvert.DeserializeObject<WeatherAlarm[]>(json);
                CityWeatherAlarmViewModel[] ncities = new CityWeatherAlarmViewModel[alarms.Length];
                for(int i=0;i<alarms.Length; i++) { ncities[i] = new CityWeatherAlarmViewModel(alarms[i]); }
                return ncities;
            }
        }

        public async Task<City> GetIpLocation(string ip)
        {
            string uri = base_url + "/" + string.Format(ip_adress_endpoint, ip, api_key, language);
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(uri);
                string json = await response.Content.ReadAsStringAsync();
                City location = JsonConvert.DeserializeObject<City>(json);
                return location;
            }
        }

        public async Task<TopCityViewModel[]> GetTop10Cities()
        {
            string uri = base_url + "/" + string.Format(top_10_cities_endpoint, api_key, language);
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(uri);
                string json = await response.Content.ReadAsStringAsync();
                City[] cities = JsonConvert.DeserializeObject<City[]>(json);
                TopCityViewModel[] ncities = new TopCityViewModel[cities.Length];
                for(int i=0;i<cities.Length; i++) { ncities[i] = new TopCityViewModel(cities[i]); }
                return ncities;
            }
        }

        public async Task<Forecast[]> Get12HourForecasts(City city)
        {
            string uri = base_url + "/" + string.Format(twelve_hour_forecast_endpoint, city.Key, api_key, language);
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(uri);
                string json = await response.Content.ReadAsStringAsync();
                Forecast[] weathers = JsonConvert.DeserializeObject<Forecast[]>(json);
                return weathers;
            }
        }

    }

}
