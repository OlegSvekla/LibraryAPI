using LibraryAPI.Core.Entities;
using LibraryAPI.Core.Interfaces.IRepository;
using LibraryAPI.Core.Interfaces.IService;
using LibraryAPI.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers
{
    [ApiController]
    [Route("api/books")]
    public class BookController : ControllerBase
    {
        private readonly IBookService<BookDto> _bookService;

        public BookController(IBookService<BookDto> bookService)
        {
            _bookService = bookService;
        }

        
        [HttpGet]
        public async Task<ActionResult<IList<BookDto>>> GetAllBooks()
        {
            var books = await _bookService.GetAllBooks();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookDto>> GetBookById(int id)
        {
            var book = await _bookService.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [HttpGet("isbn/{isbn}")]
        public async Task<ActionResult<IList<BookDto>>> GetBooksByIsbn(string isbn)
        {
            var books = await _bookService.GetBooksByIsbn(isbn);
            if (books.Count == 0)
            {
                return NotFound();
            }
            return Ok(books);
        }



        [HttpPost]
        public async Task<IActionResult> AddBook(BookDto bookDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var success = await _bookService.AddBook(bookDto);
            if (success)
            {
                return Ok();
            }
            return StatusCode(500, "Failed to add book.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBook(BookDto bookDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var success = await _bookService.UpdateBook(bookDto);
            if (success)
            {
                return Ok();
            }
            return StatusCode(500, "Failed to update book.");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBook(BookDto bookDto)
        {
            var success = await _bookService.DeleteBook(bookDto);
            if (success)
            {
                return Ok();
            }
            return StatusCode(500, "Failed to delete book.");
        }
    }

}
