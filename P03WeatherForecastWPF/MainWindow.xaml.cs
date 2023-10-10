using P03WeatherForecastWPF.Client.Models;
using P03WeatherForecastWPF.Client.Services;
using P03WeatherForecastWPF.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace P03WeatherForecastWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnGetTemperature_Click(object sender, RoutedEventArgs e)
        {
            WeatherForecastService wfs = new WeatherForecastService();
            tbTemperature.Text = "";
            string[] cities = txtCity.Text.Split(Environment.NewLine);
            foreach (var city in cities)
            {
                int temp = wfs.GetTemperature(city);
                tbTemperature.Text += $"Temperature in {city} is {temp} C" + Environment.NewLine;
            }
        }

        // Scenariusz 1: wywołanie asynchronicznie jedno miasto po drugim
        // 
        private async void btnGetTemperatureAsnyc1_Click(object sender, RoutedEventArgs e)
        {
            WeatherForecastService wfs = new WeatherForecastService();
            tbTemperature.Text = "";
            lvLogger.Items.Clear();
            string[] cities = txtCity.Text.Split(Environment.NewLine);
            foreach (var city in cities)
            {
                var t = await Task.Run<int>(() =>  // to co jest w ciele bedzie wykonane asynchrodnicznie 
                {
                    int temp = wfs.GetTemperature(city);
                    return temp;
                });

                tbTemperature.Text += $"Temperature in {city} is {t} C" + Environment.NewLine;
                lvLogger.Items.Add($"Currently processing : {city}");
            }
        }

        // Scenariusz 2:  wywołanie asynchroniczne ale czekamy na az wszystkie zadania sie wykonaja
        private async void btnGetTemperatureAsnyc2_Click(object sender, RoutedEventArgs e)
        {
            WeatherForecastService wfs = new WeatherForecastService();
            tbTemperature.Text = "";
            lvLogger.Items.Clear();
            string[] cities = txtCity.Text.Split(Environment.NewLine);

            List<Task<int>> tasks = new List<Task<int>>();
            foreach (var city in cities)
            {
                var t = Task.Run<int>(() =>  // to co jest w ciele bedzie wykonane asynchrodnicznie 
                {
                    int temp = wfs.GetTemperature(city);
                    return temp;
                });
                tasks.Add(t);
            }

            // koniec petli. wszystkie zadania zostaly dodane do listy 
            // i w tle spokojnie sie wykonuja 

            lvLogger.Items.Add("Started processing all cities");
            await Task.WhenAll(tasks); // czekamy az wszystkie zadania sie wykonaja 
            lvLogger.Items.Add("finished processing all cities");

            foreach (var task in tasks)
            {
                // tbTemperature.Text += $"Temperature in {city} is {t} C" + Environment.NewLine;

                int temp = task.Result;
                // int temp = ((Task<int>)task).Result;
                tbTemperature.Text += $"Temperature in ... is {temp} C" + Environment.NewLine;
            }

        }

        //class ExtendedResult
        //{
        //    public string City { get; set; }
        //    public int Temperature { get; set; }
        //}

        // Scenariusz 3:  wywołanie asynchroniczne ale czekamy na az wszystkie zadania sie wykonaja
        // tym razem rozszerzamy taska o to zeby przchowywal dowolne dane 
        private async void btnGetTemperatureAsnyc3_Click(object sender, RoutedEventArgs e)
        {
            WeatherForecastService wfs = new WeatherForecastService();
            tbTemperature.Text = "";
            lvLogger.Items.Clear();
            string[] cities = txtCity.Text.Split(Environment.NewLine);

            //List<Task<ExtendedResult>> tasks = new List<Task<ExtendedResult>>();
            var tasks = new List<Task>();
            foreach (var city in cities)
            {
                var t = Task.Run(() =>  // to co jest w ciele bedzie wykonane asynchrodnicznie 
                {
                    int temp = wfs.GetTemperature(city);
                    return (temp, city);
                });
                tasks.Add(t);
            }

            // koniec petli. wszystkie zadania zostaly dodane do listy 
            // i w tle spokojnie sie wykonuja 

            lvLogger.Items.Add("Started processing all cities");
            await Task.WhenAll(tasks); // czekamy az wszystkie zadania sie wykonaja 
            lvLogger.Items.Add("finished processing all cities");

            foreach (Task<(int Temperature, string City)> task in tasks)
            {
                // tbTemperature.Text += $"Temperature in {city} is {t} C" + Environment.NewLine;

                int temp = task.Result.Temperature;
                string cityName = task.Result.City;
                // int temp = ((Task<int>)task).Result;
                tbTemperature.Text += $"Temperature in {cityName} is {temp} C" + Environment.NewLine;
            }
        }

        // Scenariusz 4:  wywołanie asynchroniczne nie czkeamy tylko wynik wypisz od razu 
        private void btnGetTemperatureAsnyc4_Click(object sender, RoutedEventArgs e)
        {
            WeatherForecastService wfs = new WeatherForecastService();
            tbTemperature.Text = "";
            lvLogger.Items.Clear();
            string[] cities = txtCity.Text.Split(Environment.NewLine);


            foreach (var city in cities)
            {
                var t = Task.Run(() =>  // to co jest w ciele bedzie wykonane asynchrodnicznie 
                {
                    int temp = wfs.GetTemperature(city);
                    return (temp, city);
                });

                lvLogger.Items.Add($"Started processing city {city}");

                t.GetAwaiter().OnCompleted(() =>
                {// tutaj definuje dowolny kod, ktory wykonuje sie gdy zadanie jest skonczone 
                    tbTemperature.Text += $"Temperature in {city} is {t.Result.temp} C" + Environment.NewLine;
                    //  tbTemperature.Text += $"Temperature in {t.Result.city} is {t.Result.temp} C" + Environment.NewLine;
                });
            }


        }

        private async void btnGetTemperatureAsnyc5_Click(object sender, RoutedEventArgs e)
        {
            WeatherForecastService wfs = new WeatherForecastService();
            tbTemperature.Text = "";
            lvLogger.Items.Clear();
            string[] cities = txtCity.Text.Split(Environment.NewLine);

            pbProgress.Maximum = cities.Length;
            pbProgress.Value = 0;

            foreach (var city in cities)
            {
                lvLogger.Items.Add($"Started processing city {city}");

                var t = await Task.Run(() =>  // to co jest w ciele bedzie wykonane asynchrodnicznie 
                {
                    int temp = wfs.GetTemperature(city);
                    return (temp, city);
                });

                pbProgress.Value += 1;
                tbTemperature.Text += $"Temperature in {city} is {t.temp} C" + Environment.NewLine;
            }
        }

        private async void btnGetTemperatureAsnyc6_Click(object sender, RoutedEventArgs e)
        {
            WeatherForecastService wfs = new WeatherForecastService();
            tbTemperature.Text = "";
            lvLogger.Items.Clear();
            string[] cities = txtCity.Text.Split(Environment.NewLine);

            pbProgress.Maximum = cities.Length;
            pbProgress.Value = 0;

            foreach (var city in cities)
            {
                lvLogger.Items.Add($"Started processing city {city}");

                int temp = await wfs.GetTemperatureAsync(city);

                tbTemperature.Text += $"Temperature in {city} is {temp} C" + Environment.NewLine;
                pbProgress.Value += 1;
            }
        }

        bool isCityProcessed = false;
        string lastProcessedCityText;
        City lastProcessedCity;

        private async Task<bool> processCityOnTxtCityChange()
        {
            if (lastProcessedCityText != txtCity.Text)
            {
                isCityProcessed = false;
            }

            if (!isCityProcessed)
            {
                lastProcessedCityText = txtCity.Text;

                AccuWeatherService aws = new AccuWeatherService();
                var cities = await aws.GetCity(lastProcessedCityText);
                if (cities == null) { return false; }

                isCityProcessed = true;
                lastProcessedCity = cities;
                return true;
            }
            return false;
        }
        private async void btnGetCityNeighbors_Click(object sender, RoutedEventArgs e)
        {
            await processCityOnTxtCityChange();

            if (lastProcessedCity == null) { return; }

            AccuWeatherService aws = new AccuWeatherService();
            var cities = await aws.GetCityNeighbors(lastProcessedCity);
            if (cities == null) { return; }

            string txt = string.Format("{0} najblizszych miast od miasta \"{1}\":", cities.Length, lastProcessedCity.LocalizedName);
            for (var i = 0; i < cities.Length; ++i)
            {
                txt += "\n" + cities[i].LocalizedName;
            }

            tbTemperature.Text = txt;
        }

        private async void btnGetCityWeatherAlarms_Click(object sender, RoutedEventArgs e)
        {
            await processCityOnTxtCityChange();
            if (lastProcessedCity == null) { return; }

            AccuWeatherService aws = new AccuWeatherService();
            var cities = await aws.GetCityAlarms(lastProcessedCity);

            if (cities == null) { return; }

            string txt = string.Format("{0} ostatnich alarmów dla miasta \"{1}\" od ostatnich 24 godzin:", cities.Length, lastProcessedCity.LocalizedName);
            for (var i = 0; i < cities.Length; ++i)
            {
                txt += "\n" + cities[i].Date;
            }

            tbTemperature.Text = txt;
        }

        private async void btnGetCityWeather10DayAlarms_Click(object sender, RoutedEventArgs e)
        {
            await processCityOnTxtCityChange();
            if (lastProcessedCity == null) { return; }

            AccuWeatherService aws = new AccuWeatherService();
            var cities = await aws.GetCityAlarms(lastProcessedCity);

            if (cities == null) { return; }

            string txt = string.Format("{0} ostatnich alarmów dla miasta \"{1}\" w ciągu ostatnich 10 dni:", cities.Length, lastProcessedCity.LocalizedName);
            for (var i = 0; i < cities.Length; ++i)
            {
                txt += "\n" + cities[i].Date;
            }

            tbTemperature.Text = txt;
        }

        private async void btnGetIpData_Click(object sender, RoutedEventArgs e)
        {
            using (var hc = new HttpClient())
            {
                var r = await hc.GetAsync("http://icanhazip.com");
                var ip = await r.Content.ReadAsStringAsync();
                ip = ip.Trim();
                AccuWeatherService aws = new AccuWeatherService();
                var ipdata = await aws.GetIpLocation(ip);
                if (ipdata == null) { return; }
                var text = $"Twój adres ip to: {ip}.\nZnajduje się w województwie \"{ipdata.AdministrativeArea.LocalizedName}\"\nKraj: {ipdata.Country.LocalizedName}\nLokalizacja: {ipdata.LocalizedName}";
                tbTemperature.Text = text;
            }
        }

        private async void btnGetTop10Cities_Click(object sender, RoutedEventArgs e)
        {
            AccuWeatherService aws = new AccuWeatherService();
            var cities = await aws.GetTop10Cities();

            if (cities == null) { return; }

            string txt = "Oto top 10 miast:";
            foreach(var c in cities)
            {
                txt += $"\nKraj: {c.Country.LocalizedName}, Miasto: {c.LocalizedName}";
            }
            tbTemperature.Text = txt;
        }

        private async void btnGetCurrentConditions_Click(object sender, RoutedEventArgs e)
        {
            await processCityOnTxtCityChange();
            if (lastProcessedCity == null) { return; }

            AccuWeatherService aws = new AccuWeatherService();
            var conditions = await aws.GetCurrentConditions(lastProcessedCity.Key);

            if (conditions == null) { return; }

            string txt = $"Aktualne warunki pogodowe dla miasta {lastProcessedCity.LocalizedName}:\nTemperatura: {conditions.Temperature.Metric.Value} {conditions.Temperature.Metric.Unit}\nOpis: {conditions.WeatherText}";
            tbTemperature.Text = txt;
        }

        private async void btnGet12HoursForecast_Click(object sender, RoutedEventArgs e)
        {
            await processCityOnTxtCityChange();
            if (lastProcessedCity == null) { return; }

            AccuWeatherService aws = new AccuWeatherService();
            var forecasts = await aws.Get12HourForecasts(lastProcessedCity);

            if (forecasts == null) { return; }

            string txt = $"Pogoda na najbliższe 12 godzin dla miasta " + lastProcessedCity.LocalizedName + ":";
            foreach(var f in forecasts)
            {
                string data = DateTimeOffset.Parse(f.DateTime).ToString("yyyy-MM-dd HH:mm:ss");
                data = $"[{data}]: {f.Temperature.Value} {f.Temperature.Unit}, {f.IconPhrase}";
                txt += "\n" + data;
            }
            tbTemperature.Text = txt;
        }

    }
}
