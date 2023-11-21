using lab6.Shared.Models;
using lab6.Shared.Services;

namespace lab6.Shared.Controller
{
	public class BookController
	{
		private readonly IBookService _bookService;

		public BookController(IBookService bookService)
		{
			_bookService = bookService ?? throw new ArgumentNullException(nameof(bookService));
		}

		public async Task<BookModel[]> GetBooks()
		{
			try
			{
				return await _bookService.GetBooks();
			}
			catch (Exception error)
			{
			}

			return new BookModel[0];
		}

		public async Task<BookModel?> GetBookByName(string name)
		{
			try
			{
				if (string.IsNullOrEmpty(name))
				{
					return null;
				}

				return await _bookService.GetBookByName(name);
			}
			catch (Exception error) { }

			return null;
		}

		public async Task AddBook(BookModel book)
		{
			if (book == null) { return; }

			try
			{
				await _bookService.AddBook(book);
			}
			catch (Exception error) { }
		}

		public async Task RemoveBook(int id)
		{
			if (id < 0)
			{
				return;
			}

			try
			{
				await _bookService.RemoveBook(id);
			}
			catch (Exception)
			{

			}
		}

		public async Task UpdateBook(int id, BookModel book)
		{
			if (id < 0)
			{
				return;
			}

			if (book == null)
			{
				return;
			}

			try
			{
				await _bookService.UpdateBook(id, book);
			}
			catch (Exception error)
			{
			}
		}
	}

}
