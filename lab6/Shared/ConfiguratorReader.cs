using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace lab6.Shared
{
	public class Config
	{
		public bool production { get; set; }
		public string booksUrlBase { get; set; }
		public string booksUrlGetAll { get; set; }
		public string booksUrlGet { get; set; }
		public string booksUrlUpdate { get; set; }
		public string booksUrlDelete { get; set; }
		public string booksUrlCreate { get; set; }
	}
	public class ConfiguratorReader
	{
		public ConfiguratorReader(HttpClient http)
		{
			this.http = http;
		}

		public Task<Config> getConfig()
		{
			return http.GetFromJsonAsync<Config>("config.json");
		}

		public HttpClient http;
	}
}
