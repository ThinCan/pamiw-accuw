using P06Shop.Shared;
using P06Shop.Shared.Shop;

namespace P06Shop.Shared.Services.BookService
{
    public interface IBookService
    {
        Task<ServiceResponse<List<Book>>> GetBooksAsync();
        Task<ServiceResponse<bool>> CreateBookAsync(Book b);
        Task<ServiceResponse<bool>> UpdateBookAsync(Book b);
        Task<ServiceResponse<bool>> DeleteBookAsync(Book b);
    }
}
