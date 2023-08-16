using LibraryAPI.Core.Entities;
using LibraryAPI.Core.Interfaces.IRepository;
using LibraryAPI.Core.Interfaces.IService;
using LibraryAPI.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers
{
    //[Authorize]
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
        /// <response code="500">The base is Empty. We don't have any books</response>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IList<BookDto>))]
        [ProducesResponseType(500)]
        public async Task<ActionResult<IList<BookDto>>> GetAllBooks()
        {
            var books = await _bookService.GetAllBooks();
            return Ok(books);
        }

        /// <summary>
        /// Gets the book by its own Id
        /// </summary>        
        /// <returns>Ok response containing a single book.</returns>
        /// <remarks>
        /// We have five books and five Id identification key. Enter any number from 1 to 5 inclusive.
        /// </remarks>
        /// <response code="200">Returns one book.</response>
        /// <response code="500">You entered an invalid Id, please enter an existing Id.</response>
        [ProducesResponseType(200, Type = typeof(BookDto))]
        [ProducesResponseType(500)]
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

        /// <summary>
        /// Gets the book by its own Isbn
        /// </summary>        
        /// <returns>Ok response containing a single book.</returns>
        /// <remarks>
        /// We have five books and five its own Isbn property. Enter Isbn. 
        /// FOR EXAMPLE:
        /// </remarks>
        /// <response code="200">Returns one book.</response>
        /// <response code="500">You entered an invalid Isbn, please enter an existing Isbn.</response>
        [ProducesResponseType(200, Type = typeof(BookDto))]
        [ProducesResponseType(500)]
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

        /// <summary>
        /// Add book
        /// </summary>        
        /// <returns>Ok response succesefully created book in DATA.</returns>
        /// <remarks>
        /// Enter any fields except fields containing Id, cause Id is generated automatically. 
        /// If you have not entered other field values, they are automatically generated with default values.
        /// </remarks>
        /// <response code="200">Book is created.</response>
        /// <response code="500">You enter the Id fields.
        /// Please don't enter a value for the Id fields cause they are automatically populated.</response>
        [ProducesResponseType(200, Type = typeof(BookDto))]
        [ProducesResponseType(500)]
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
            return StatusCode(500, "You enter the Id fields." +
                "Please don't enter a value for the Id fields as they are automatically populated");
        }

        ///// <summary>
        ///// Updates book with the specified ID
        ///// </summary>        
        ///// <returns>Ok response indicating the update was successful.</returns>
        ///// <remarks>
        ///// Please enter a value for the every Id fields to choose the book to update. 
        ///// Authors Ids must have values ​​corresponding to their book or not but have to contain an existing Authors Id. 
        ///// </remarks>
        ///// <response code="200">Book is updated.</response>
        ///// <response code="500">You don't enter every Id fields.</response>
        //[ProducesResponseType(200, Type = typeof(BookDto))]
        //[ProducesResponseType(500)]        
        //[HttpPut]
        //public async Task<IActionResult> UpdateBook(BookDto bookDto)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var success = await _bookService.UpdateBook(bookDto);
        //    if (success)
        //    {
        //        return Ok();
        //    }
        //    return StatusCode(500, "You don't enter every Id fields.");            
        //}

        [HttpPut("{id:int}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> UpdateBook([FromRoute] int id, [FromBody] BookDto updatedBookDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var success = await _bookService.UpdateBook(id, updatedBookDto);
            if (success)
            {
                return NoContent();
            }
            return StatusCode(500, "You don't enter every Id fields.");
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteBook([FromRoute] int id)
        {
            var success = await _bookService.DeleteBook(id);
            
            
            return NoContent();
            
           
        }

        ///// <summary>
        ///// Removes book with the specified ID.
        ///// </summary>        
        ///// <returns>Ok response indicating the removing was successful.</returns>
        ///// /// <remarks>
        ///// Enter only book's Id field which you want to remove.
        ///// </remarks>
        ///// <response code="200">The book was successfully removed.</response>
        ///// <response code="500">You entered a non-existent Id, please enter a valid existing Id  </response>
        //[ProducesResponseType(200, Type = typeof(BookDto))]
        //[ProducesResponseType(500)]
        //[HttpDelete]
        //public async Task<IActionResult> DeleteBook(BookDto bookDto)
        //{
        //    var success = await _bookService.DeleteBook(bookDto);
        //    if (success)
        //    {
        //        return Ok();
        //    }
        //    return StatusCode(500, "You entered a non-existent Id, please enter a valid existing Id. ");
        //}
    }
}