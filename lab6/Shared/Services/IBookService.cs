using lab6.Shared.Models;

namespace lab6.Shared.Services
{
	public interface IBookService
	{
		Task<BookModel[]> GetBooks();
		Task<BookModel?> GetBookByName(string name);
		Task<HttpResponseMessage> AddBook(BookModel book);
		Task<BookModel?> RemoveBook(int id);
		Task<HttpResponseMessage> UpdateBook(int id, BookModel book);
	}
}
