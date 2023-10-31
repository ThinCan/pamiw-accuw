using Microsoft.AspNetCore.Mvc;
using P06Shop.Shared;
using P06Shop.Shared.Services.BookService;
using P06Shop.Shared.Shop;

namespace P05Shop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : Controller
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<Product>>>> GetBooks()
        {
            var result = await _bookService.GetBooksAsync();

            if (result.Success)
                return Ok(result);
            else
                return StatusCode(500, $"Internal server error {result.Message}");
        }
        [HttpPost]
        public async Task<ActionResult<bool>> CreateBook([FromBody] Book b)
        {
            var result = await _bookService.CreateBookAsync(b);

            if (result.Success)
                return Ok(result);
            else
                return StatusCode(500, $"Internal server error {result.Message}");
        }
        [HttpPut]
        public async Task<ActionResult<bool>> UpdateBook([FromBody] Book b)
        {
            var result = await _bookService.UpdateBookAsync(b);

            if (result.Success)
                return Ok(result);
            else
                return StatusCode(500, $"Internal server error {result.Message}");
        }
        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteBook([FromBody] Book b)
        {
            var result = await _bookService.DeleteBookAsync(b);

            if (result.Success)
                return Ok(result);
            else
                return StatusCode(500, $"Internal server error {result.Message}");
        }
    }
}
