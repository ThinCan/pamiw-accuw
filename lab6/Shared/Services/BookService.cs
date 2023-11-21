using lab6.Shared.Models;
using System.Net.Http.Json;

namespace lab6.Shared.Services
{
    public class BookService : IBookService
    {
        HttpClient httpClient;
        Config config;
        public BookService(HttpClient http, Config config)
        {
            this.config = config;
            this.httpClient = http;
        }
        public Task<BookModel[]> GetBooks()
        {
            string url = config.booksUrlBase + config.booksUrlGetAll;
            Console.WriteLine(url);
            return httpClient.GetFromJsonAsync<BookModel[]>(url);
        }

        public Task<BookModel?> GetBookByName(string name)
        {
            string url = config.booksUrlBase + config.booksUrlGet + name;
            Console.WriteLine(url);
            return httpClient.GetFromJsonAsync<BookModel>(url);
        }

        public Task<HttpResponseMessage> AddBook(BookModel book)
        {
            string url = config.booksUrlBase + config.booksUrlCreate;
            return httpClient.PostAsJsonAsync(url, book);
        }

        public Task<BookModel?> RemoveBook(int id)
        {
            string url = config.booksUrlBase + config.booksUrlDelete + id;
            var task = Task.Run(async () =>
            {
                var res = await httpClient.DeleteFromJsonAsync<BookModel?>(url);
                return res;
            });
            return task;
        }

        public Task<HttpResponseMessage> UpdateBook(int id, BookModel book)
        {
            string url = config.booksUrlBase + config.booksUrlUpdate + id;
            var task = Task.Run(async () =>
            {
                var res = await httpClient.PutAsJsonAsync<BookModel?>(url, book);
                return res;
            });
            return task;
        }
    }
}
