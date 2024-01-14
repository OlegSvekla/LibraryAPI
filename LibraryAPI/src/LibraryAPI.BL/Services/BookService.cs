using AutoMapper;
using FluentValidation;
using LibraryAPI.Domain.DTOs;
using LibraryAPI.Domain.Interfaces.IService;
using Microsoft.Extensions.Logging;
using TaskTracker.Domain.Interfaces.IRepositories;

namespace LibraryAPI.BL.Services
{
    public class BookService : IBookService<BookDto>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<BookService> _logger;
        private readonly IValidator<BookDto> _validator;

        public BookService(IBookRepository bookRepository,
            IMapper mapper,
            ILogger<BookService> logger,
            IValidator<BookDto> validator)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
            _logger = logger;
            _validator = validator;
        }

        public async Task<IList<BookDto>> GetAllAsync()
        {
            var books = await _bookRepository.GetAllAsync();

            return _mapper.Map<IList<BookDto>>(books);
        }

        public async Task<BookDto> GetByIdAsync(int id)
        {
            var book = await _bookRepository.GetOneByAsync(expression: _ => _.Id.Equals(id));
            if (book is null) return ThrowBookNotFoundException();

            return _mapper.Map<BookDto>(book);
        }

        public async Task<IList<BookDto>> GetByIsbnAsync(string isbn)
        {
            var book = await _bookRepository.GetOneByAsync(expression: _ => _.Isbn.Equals(isbn));

            return new List<BookDto> { _mapper.Map<BookDto>(book) };
        }

        public async Task<bool> CreateAsync(BookDto bookDto)
        {
            var validationResult = await _validator.ValidateAsync(bookDto);
            if (!validationResult.IsValid)            
                throw new ValidationException(validationResult.ToString());            

            var book = await _bookRepository.GetOneByAsync(expression: _ => _.Id.Equals(bookDto.Id));
            if (book is not null) throw new ValidationException("Book is already existing");

            await _bookRepository.CreateAsync(book);

            return true;
        }

        public async Task<bool> UpdateAsync(int id, BookDto updatedBookDto)
        {
            var existingBook = await _bookRepository.GetOneByAsync(expression: book => book.Id == id);
            if (existingBook is null) ThrowBookNotFoundException();
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

            return true;
        }

        public async Task<bool> DeleteAsync(int bookId)
        {
            var bookToDelete = await _bookRepository.GetOneByAsync(expression: _ => _.Id.Equals(bookId));
            if (bookToDelete is null) ThrowBookNotFoundException();

            await _bookRepository.DeleteAsync(bookToDelete!);

            return true;
        }

        private BookDto ThrowBookNotFoundException() =>
            throw new NotFoundException("Book not found");
    }
}