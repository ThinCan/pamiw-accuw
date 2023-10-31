using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P04WeatherForecastAPI.Client.Configuration
{
    internal class BaseBookEndpoint
    {
        public string Base_url { get; set; }
        public string GetAllBooksEndpoint {  get; set; }
        public string CreateBookEndpoint {  get; set; }
        public string UpdateBookEndpoint {  get; set; }
        public string DeleteBookEndpoint {  get; set; }
    }
}
