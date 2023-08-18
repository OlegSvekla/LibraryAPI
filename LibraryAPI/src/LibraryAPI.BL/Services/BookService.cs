using AutoMapper;
using FluentValidation;
using LibraryAPI.Core.Entities;
using LibraryAPI.Core.Interfaces.IRepository;
using LibraryAPI.Core.Interfaces.IService;
using LibraryAPI.Domain.Exeptions;
using Microsoft.Extensions.Logging;

namespace LibraryAPI.Services
{
    public class BookService : IBookService<BookDto>
    {
        private readonly IRepository<Book> _bookRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<BookService> _logger;
        private readonly IValidator<BookDto> _validator;

        public BookService(IRepository<Book> bookRepository,
            IMapper mapper,
            ILogger<BookService> logger,
            IValidator<BookDto> validator)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
            _logger = logger;
            _validator = validator;
        }

        public async Task<IList<BookDto>> GetAllBooks()
        {
            _logger.LogInformation("Getting all books.");

            var books = await _bookRepository.GetAllAsync();
            if (books is null)
            {
                _logger.LogWarning("The base is empty and have any books.");

                throw new BookNotFoundException("The base is empty and don't have any books.");
            }

            _logger.LogInformation("Retrieved all books successfully.");

            return _mapper.Map<IList<BookDto>>(books);
        }

        public async Task<BookDto> GetBookById(int id)
        {
            _logger.LogInformation($"Getting book with Id: {id}.");

            var book = await _bookRepository.GetOneByAsync(expression: _ => _.Id.Equals(id));
            if (book is null)
            {
                _logger.LogWarning($"The base doesn't contain the book with Id: {id}.");

                throw new BookNotFoundException($"The base doesn't contain the book with Id: {id}. Please try again with an existing Id.");
            }

            _logger.LogInformation($"Retrieved book with Id: {id} successfully.");

            return _mapper.Map<BookDto>(book);
        }

        public async Task<IList<BookDto>> GetBooksByIsbn(string isbn)
        {
            _logger.LogInformation($"Getting book with ISBN: {isbn}.");

            var book = await _bookRepository.GetOneByAsync(expression: _ => _.Isbn.Equals(isbn));
            if (book is null)
            {
                _logger.LogWarning($"The base doesn't contain the book with ISBN: {isbn}.");

                throw new BookNotFoundException($"The base doesn't contain the book with ISBN: {isbn}. Please try again with an existing ISBN.");
            }

            _logger.LogInformation($"Retrieved book with ISBN: {isbn} successfully.");

            return new List<BookDto> { _mapper.Map<BookDto>(book) };
        }

        public async Task<bool> AddBook(BookDto bookDto)
        {
            var validationResult = await _validator.ValidateAsync(bookDto);
            if (!validationResult.IsValid)
            {
                throw new InvalidValueException(validationResult.ToString());
            }

            _logger.LogInformation("Adding a new book.");

            var book = _mapper.Map<Book>(bookDto);

            var result = await _bookRepository.AddAsync(book);
            if (result is null)
            {
                _logger.LogError("Failed to add the book.");

                throw new FailedToMakeOperation("Failed to add the book. " +
                    "Please don't enter a value for the Id field as it is automatically populated.");
            }

            _logger.LogInformation("Added a new book successfully.");

            return true;
        }

        public async Task<BookDto> UpdateBook(int id, BookDto updatedBookDto)
        {
            var validationResult = await _validator.ValidateAsync(updatedBookDto);
            if (!validationResult.IsValid)
            {
                throw new InvalidValueException(validationResult.ToString());
            }

            _logger.LogInformation($"Updating a book with Id: {id}");

            var existingBook = await _bookRepository.GetOneByAsync(expression: book => book.Id == id);
            if (existingBook is null)
            {
                _logger.LogError($"Book with Id: {id} was not found");

                throw new BookNotFoundException($"Book with Id: {id} was not found");
            }
            else
            {
                existingBook.Title = updatedBookDto.Title;
                existingBook.Isbn = updatedBookDto.Isbn;
                existingBook.Genre = updatedBookDto.Genre;
                existingBook.Description = updatedBookDto.Description;
                existingBook.BorrowedDate = updatedBookDto.BorrowedDate;
                existingBook.ReturnDate = updatedBookDto.ReturnDate;
            }

            await _bookRepository.UpdateAsync(existingBook);

            _logger.LogInformation($"Book with Id: {id} has been successfully updated.");

            return updatedBookDto;
        }

        public async Task<BookDto> DeleteBook(int bookId)
        {
            var bookToDelete = await _bookRepository.GetOneByAsync(expression: _ => _.Id.Equals(bookId));
            if (bookToDelete is null)
            {
                throw new BookNotFoundException($"Such event with Id: {bookId} was not found");
            }

            await _bookRepository.DeleteAsync(bookToDelete!);

            _logger.LogInformation($"Event with Id: {bookId} is removed");

            var bookDeleted = _mapper.Map<BookDto>(bookToDelete);

            return bookDeleted;
        }
    }
}