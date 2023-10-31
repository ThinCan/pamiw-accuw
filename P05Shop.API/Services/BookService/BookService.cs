using P06Shop.Shared;
using P06Shop.Shared.Services.BookService;
using P06Shop.Shared.Shop;
using P07Shop.DataSeeder;

namespace P05Shop.API.Services.BookService
{
    public class BookService : IBookService
    {
        public async Task<ServiceResponse<List<Book>>> GetBooksAsync()
        {
            try
            {
                var response = new ServiceResponse<List<Book>>()
                {
                    Data = ProductSeeder.GenerateBookData(),
                    Message = "Ok",
                    Success = true
                };

                return response;
            }
            catch (Exception)
            {
                return new ServiceResponse<List<Book>>()
                {
                    Data = null,
                    Message = "Problem with dataseeder library",
                    Success = false
                };
            }
        }

        public async Task<ServiceResponse<bool>> CreateBookAsync(Book b)
        {
            try
            {
                if (!ValidateBookOnCreate(b)) { throw new Exception("Cannot create book"); }

                var response = new ServiceResponse<bool>()
                {
                    Data = true,
                    Message = "Ok",
                    Success = true
                };

                return response;
            }
            catch (Exception)
            {
                return new ServiceResponse<bool>()
                {
                    Data = false,
                    Message = "Problem with dataseeder library",
                    Success = false
                };
            }
        }
        public async Task<ServiceResponse<bool>> UpdateBookAsync(Book b)
        {
            try
            {
                if (!ValidateBookOnUpdate(b)) { throw new Exception("Cannot update book"); }

                var response = new ServiceResponse<bool>()
                {
                    Data = true,
                    Message = "Ok",
                    Success = true
                };

                return response;
            }
            catch (Exception)
            {
                return new ServiceResponse<bool>()
                {
                    Data = false,
                    Message = "Problem with dataseeder library",
                    Success = false
                };
            }
        }
        public async Task<ServiceResponse<bool>> DeleteBookAsync(Book b)
        {
            try
            {
                if (!ValidateBookOnDelete(b)) { throw new Exception("Cannot delete book"); }

                var response = new ServiceResponse<bool>()
                {
                    Data = true,
                    Message = "Ok",
                    Success = true
                };

                return response;
            }
            catch (Exception)
            {
                return new ServiceResponse<bool>()
                {
                    Data = false,
                    Message = "Problem with dataseeder library",
                    Success = false
                };
            }
        }

        private bool ValidateBookOnCreate(Book b)
        {
            //Todo: check if book is in the database.
            return (
                b.Id > 0
                && b.Title.Length > 0
                && b.Author.Length > 0
                && b.Description.Length > 0);
        }
        private bool ValidateBookOnUpdate(Book b)
        {
            //Todo: check if book is in the database.
            return (
                b.Id > 0
                && b.Title.Length > 0
                && b.Author.Length > 0
                && b.Description.Length > 0);
        }
        private bool ValidateBookOnDelete(Book b)
        {
            //Todo: check if book is in the database.
            return (
                b.Id > 0
                && b.Title.Length > 0
                && b.Author.Length > 0
                && b.Description.Length > 0);
        }
    }
}
