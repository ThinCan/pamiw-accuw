using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json;
using P04WeatherForecastAPI.Client.Configuration;
using P06Shop.Shared;
using P06Shop.Shared.Services.BookService;
using P06Shop.Shared.Services.ProductService;
using P06Shop.Shared.Shop;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace P04WeatherForecastAPI.Client.Services.ProductServices
{
    internal class BookService : IBookService
    {
        private readonly HttpClient _httpClient;
        private readonly AppSettings _appSettings;
        public BookService(HttpClient httpClient, IOptions<AppSettings> appSettings)
        {
            _httpClient = httpClient;
            _appSettings = appSettings.Value;
        }

        public async Task<ServiceResponse<List<Book>>> GetBooksAsync()
        {
            var response = await _httpClient.GetAsync(_appSettings.BaseBookEndpoint.GetAllBooksEndpoint);
            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ServiceResponse<List<Book>>>(json);
            return result;
        }
        public async Task<ServiceResponse<bool>> CreateBookAsync(Book b)
        {
            var response = await _httpClient.GetAsync(_appSettings.BaseBookEndpoint.CreateBookEndpoint);
            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ServiceResponse<bool>>(json);
            return result;
        }
        public async Task<ServiceResponse<bool>> UpdateBookAsync(Book b)
        {
            var response = await _httpClient.GetAsync(_appSettings.BaseBookEndpoint.UpdateBookEndpoint);
            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ServiceResponse<bool>>(json);
            return result;
        }
        public async Task<ServiceResponse<bool>> DeleteBookAsync(Book b)
        {
            var response = await _httpClient.GetAsync(_appSettings.BaseBookEndpoint.DeleteBookEndpoint);
            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ServiceResponse<bool>>(json);
            return result;
        }
    }
}
