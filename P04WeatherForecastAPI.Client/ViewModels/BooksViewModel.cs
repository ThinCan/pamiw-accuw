using CommunityToolkit.Mvvm.ComponentModel;
using P04WeatherForecastAPI.Client.Models;
using P04WeatherForecastAPI.Client.Services.WeatherServices;
using P06Shop.Shared.Services.BookService;
using P06Shop.Shared.Services.ProductService;
using P06Shop.Shared.Shop;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace P04WeatherForecastAPI.Client.ViewModels
{
    public partial class BooksViewModel : ObservableObject
    {
        private readonly IBookService _bookService;

        public ObservableCollection<Book> Books { get; set; }

        public BooksViewModel(IBookService bookService)
        {
            _bookService = bookService;
            Books = new ObservableCollection<Book>();
        }

        public async void GetBooks()
        {
            var productsResult = await _bookService.GetBooksAsync();
            if (productsResult.Success)
            {
                foreach (var p in productsResult.Data)
                {
                    Books.Add(p);
                }
            }
        }

    }
}
