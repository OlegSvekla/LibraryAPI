using LibraryAPI.Domain.Entities;
using LibraryAPI.Domain.Interfaces.IRepository;
using LibraryAPI.Domain.Interfaces.IService;
using LibraryAPI.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/books")]
    public class BookController : ControllerBase
    {
        private readonly IBookService<BookDto> _bookService;

        public BookController(IBookService<BookDto> bookService)
        {
            _bookService = bookService;
        }

        /// <summary>
        /// Gets the list of books
        /// </summary>        
        /// <returns>Ok response containing books collection.</returns>
        /// <response code="200">Returns the list of books.</response> 
        /// <response code="404">The base is Empty. Books weren't found</response>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IList<BookDto>))]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IList<BookDto>>> GetAllBooks()
        {
            var books = await _bookService.GetAllBooks();

            return Ok(books);
        }

        /// <summary>
        /// Gets the book by its own Id
        /// </summary>   
        /// <param name="id">ID of the Book to get.</param>
        /// <returns>Ok response containing a single book.</returns>
        /// <remarks>
        /// We have five books and five Id identification key. Enter any number from 1 to 5 inclusive.
        /// </remarks>
        /// <response code="200">Returns one book.</response>
        /// <response code="404">The book with this Id was not found.</response>
        [ProducesResponseType(200, Type = typeof(BookDto))]
        [ProducesResponseType(404)]
        [HttpGet("{id}")]
        public async Task<ActionResult<BookDto>> GetBookById(int id)
        {
            var book = await _bookService.GetBookById(id);
            
            return Ok(book);
        }

        /// <summary>
        /// Gets the book by its own Isbn
        /// </summary>    
        /// <param name="isbn">Isbn of the Book to get.</param>
        /// <returns>Ok response containing a single book.</returns>
        /// <remarks>
        /// We have five books and five its own Isbn property. Enter Isbn. 
        /// FOR EXAMPLE:
        /// 1 - 1234567890
        /// 2 - 0987654321
        /// 3 - 9876543210
        /// 4 - 5678901234
        /// 5 - 4321098765
        /// </remarks>
        /// <response code="200">Returns one book.</response>
        /// <response code="404">The book with this Isbn was not found.</response>
        [ProducesResponseType(200, Type = typeof(BookDto))]
        [ProducesResponseType(404)]
        [HttpGet("isbn/{isbn}")]
        public async Task<ActionResult<IList<BookDto>>> GetBooksByIsbn(string isbn)
        {
            var books = await _bookService.GetBooksByIsbn(isbn);

            return Ok(books);
        }

        /// <summary>
        /// Add book
        /// </summary>   
        /// <param name="bookDto">The Book to be created.</param>
        /// <returns>Ok response succesefully created book in DATA.</returns>
        /// <response code="201">Book is created.</response>
        [ProducesResponseType(201, Type = typeof(BookDto))]
        [HttpPost]
        public async Task<IActionResult> AddBook(BookDto bookDto)
        {
            var success = await _bookService.AddBook(bookDto);

            return Ok();
        }

        /// <summary>
        /// Updates book with the specified ID
        /// </summary>   
        /// <param name="id">The ID of the Book to be updated.</param>
        /// <param name="updatedBookDto">The updated Book data.</param>
        /// <response code="204">Book is successfuly updated.</response>
        [ProducesResponseType(204, Type = typeof(BookDto))]    
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateBook([FromRoute] int id, [FromBody] BookDto updatedBookDto)
        {
            var success = await _bookService.UpdateBook(id, updatedBookDto);

            return NoContent();
        }

        /// <summary>
        /// Removes book with the specified ID.
        /// </summary>        
        /// <param name="id">The ID of the Book to be removed.</param>
        /// <response code="204">The book was successfully removed.</response>
        /// <response code="404">The book with this Id was not found.</response>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(204, Type = typeof(BookDto))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteBook([FromRoute] int id)
        {
            var success = await _bookService.DeleteBook(id);
                       
            return NoContent();            
        }
    }
}