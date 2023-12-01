﻿using FluentValidation;
using LibraryAPI.Domain.Entities;
using LibraryAPI.Domain.Interfaces.IService;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers
{ 
    //[Authorize]
    [ApiController]
    [Route("api/book")]
    public class BookController : ControllerBase
    {
        private readonly IBookService<BookDto> _bookService;
        private readonly IValidator<BookDto> _validator;

        public BookController(
            IBookService<BookDto> bookService,
            IValidator<BookDto> validator)
        {
            _bookService = bookService;
            _validator = validator;
        }
     
        /// <returns>Ok response containing books collection.</returns>
        /// <response code="200">Returns the list of books.</response> 
        /// <response code="404">The base is Empty. Books weren't found</response>
        [ProducesResponseType(200, Type = typeof(IList<BookDto>))]
        [ProducesResponseType(404)]
        //delete 404
        [HttpGet("all")]
        public async Task<ActionResult<IList<BookDto>>> GetAll()
        {
            var books = await _bookService.GetAllAsync();

            return books == null ? NotFound("Books was not found") : Ok(books);
        }

        /// <param name="id">ID of the Book to get.</param>
        /// <returns>Ok response containing a single book.</returns>
        /// <response code="200">Returns one book.</response>
        /// <response code="404">The book with this Id was not found.</response>
        [ProducesResponseType(200, Type = typeof(BookDto))]
        [ProducesResponseType(404)]
        [HttpGet("{id}")]
        public async Task<ActionResult<BookDto>> GetById(int id)
        {
            var book = await _bookService.GetByIdAsync(id);

            return book == null ? NotFound("Book not found by Id") : Ok(book);
        }
 
        /// <param name="isbn">Isbn of the Book to get.</param>
        /// <returns>Ok response containing a single book.</returns>
        /// <response code="200">Returns one book.</response>
        /// <response code="404">The book with this Isbn was not found.</response>
        [ProducesResponseType(200, Type = typeof(BookDto))]
        [ProducesResponseType(404)]
        [HttpPut("books/{isbn}")]
        public async Task<ActionResult<IList<BookDto>>> GetByIsbn(string isbn)
        {
            var books = await _bookService.GetByIsbnAsync(isbn);

            return books == null ? NotFound("Books not found by ISBN") : Ok(books);
        }

        /// <param name="bookDto">The Book to be created.</param>
        /// <returns>Ok response succesefully created book in DATA.</returns>
        /// <response code="201">Book is created.</response>
        [ProducesResponseType(201, Type = typeof(BookDto))]
        [HttpPost]
        public async Task<IActionResult> Create(BookDto bookDto)
        {
            var validationResult = await _validator.ValidateAsync(bookDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.ToString());
            }

            var success = await _bookService.CreateAsync(bookDto);

            return success == false ? BadRequest("This Book is already existing") : Ok();
        }

        /// <param name="id">The ID of the Book to be updated.</param>
        /// <param name="updatedBookDto">The updated Book data.</param>
        /// <response code="204">Book is successfuly updated.</response>
        /// <response code="404">The Task by Id was not found.</response>
        [ProducesResponseType(204, Type = typeof(BookDto))]
        [ProducesResponseType(404)]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateBook([FromRoute] int id, [FromBody] BookDto updatedBookDto)
        {
            var validationResult = await _validator.ValidateAsync(updatedBookDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.ToString());
            }

            var success = await _bookService.UpdateAsync(id, updatedBookDto);

            return success == false ? NotFound("Book not found by Id") : NoContent();
        }
      
        /// <param name="id">The ID of the Book to be removed.</param>
        /// <response code="204">The book was successfully removed.</response>
        /// <response code="404">The book with this Id was not found.</response>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(204, Type = typeof(BookDto))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteBook([FromRoute] int id)
        {
            var success = await _bookService.DeleteAsync(id);

            return success == false ? NotFound("Book not found by Id") : NoContent();
        }
    }
}