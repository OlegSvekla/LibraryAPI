using AutoMapper;
using FluentNHibernate.Data;
using LibraryAPI.Core.Entities;
using LibraryAPI.Core.Interfaces.IRepository;
using LibraryAPI.Core.Interfaces.IService;
using Microsoft.Extensions.Logging;

namespace LibraryAPI.Services
{
    public class BookService : IBookService<BookDto>
    {
        private readonly IBaseRepository<Book> _bookRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<BookService> _logger;

        public BookService(IBaseRepository<Book> bookRepository, IMapper mapper, ILogger<BookService> logger)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IList<BookDto>> GetAllBooks()
        {
            _logger.LogInformation("Getting all books.");

            var books = await _bookRepository.GetAllAsync();
            if (books is null)
            {
                _logger.LogWarning("The base is empty and have any books.");
                throw new Exception("The base is empty and don't have any books.");
            }

            _logger.LogInformation("Retrieved all books successfully.");
            return _mapper.Map<IList<BookDto>>(books);
        }

        public async Task<BookDto> GetBookById(int id)
        {
            _logger.LogInformation($"Getting book with Id: {id}.");

            var book = await _bookRepository.GetById(id);
            if (book is null)
            {
                _logger.LogWarning($"The base doesn't contain the book with Id: {id}.");
                throw new Exception($"The base doesn't contain the book with Id: {id}. Please try again with an existing Id.");
            }

            _logger.LogInformation($"Retrieved book with Id: {id} successfully.");
            return _mapper.Map<BookDto>(book);
        }

        public async Task<IList<BookDto>> GetBooksByIsbn(string isbn)
        {
            _logger.LogInformation($"Getting book with ISBN: {isbn}.");

            var book = await _bookRepository.FirstOrDefaultAsync(b => b.Isbn == isbn);
            if (book is null)
            {
                _logger.LogWarning($"The base doesn't contain the book with ISBN: {isbn}.");
                throw new Exception($"The base doesn't contain the book with ISBN: {isbn}. Please try again with an existing ISBN.");
            }

            _logger.LogInformation($"Retrieved book with ISBN: {isbn} successfully.");
            return new List<BookDto> { _mapper.Map<BookDto>(book) };
        }

        public async Task<bool> AddBook(BookDto bookDto)
        {
            _logger.LogInformation("Adding a new book.");

            var book = _mapper.Map<Book>(bookDto);
            try
            {
                await _bookRepository.AddAsync(book);
            }
            catch (Exception)
            {
                _logger.LogError("Failed to add the book.");
                throw new Exception("Failed to add the book. " +
                    "Please don't enter a value for the Id field as it is automatically populated.");
            }

            _logger.LogInformation("Added a new book successfully.");
            return true; // Return true if the book was successfully added
        }

        public async Task<bool> UpdateBook(BookDto bookDto)
        {
            _logger.LogInformation("Updating a book.");

            var book = _mapper.Map<Book>(bookDto);
            try
            {
                await _bookRepository.UpdateAsync(book);
            }
            catch (Exception)
            {
                _logger.LogError("Failed to update the book. Some Id fields are missing or the Authors Ids are invalid.");
                throw new Exception("Failed to update the book. " +
                    "Please enter a value for every Id field to choose the book to update, " +
                    "and ensure that the Authors Ids correspond to their books or contain existing Authors Ids.");
            }

            _logger.LogInformation("Updated the book successfully.");
            return true; // Return true if the book was successfully updated
        }

        public async Task<bool> DeleteBook(BookDto bookDto)
        {
            _logger.LogInformation("Deleting a book.");

            var book = _mapper.Map<Book>(bookDto);
            try
            {
                await _bookRepository.DeleteAsync(book);
            }
            catch (Exception)
            {
                _logger.LogError("Failed to delete the book.");
                throw new Exception("Failed to delete the book. Please enter a valid existing Id.");
            }

            _logger.LogInformation("Deleted the book successfully.");
            return true; // Return true if the book was successfully deleted
        }
    }
}