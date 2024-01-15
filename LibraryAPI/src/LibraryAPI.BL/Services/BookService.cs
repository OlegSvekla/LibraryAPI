using AutoMapper;
using LibraryApi.BL.Validators.IValidators;
using LibraryAPI.BL.DTOs;
using LibraryAPI.Domain.Interfaces.IServices;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using TaskTracker.Domain.Interfaces.IRepositories;

namespace LibraryAPI.BL.Services
{
    public class BookService : IBookService<BookDto>
    {
        private readonly IBookRepository bookRepository;
        private readonly IBookDtoValidator bookDtoValidator;
        private readonly IAuthorDtoValidator authorDtoValidator;
        private readonly IMapper mapper;
        private readonly ILogger<BookService> logger;

        public BookService(IBookRepository bookRepository,
            IBookDtoValidator bookDtoValidator,
            IAuthorDtoValidator authorDtoValidator,
            IMapper mapper,
            ILogger<BookService> logger)
        {
            this.bookRepository = bookRepository;
            this.bookDtoValidator = bookDtoValidator;
            this.authorDtoValidator = authorDtoValidator;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<IList<BookDto>> GetAllAsync()
        {
            var books = await bookRepository.GetAllAsync(cancellationToken: CancellationToken.None);

            return mapper.Map<IList<BookDto>>(books);
        }

        public async Task<BookDto> GetByIdAsync(int id)
        {
            var book = await bookRepository.GetOneByAsync(expression: _ => _.Id.Equals(id),
                cancellationToken: CancellationToken.None);
            if (book is null) 
            {
                logger.LogInformation("Book not found Exception");
                ThrowBookNotFoundException();
            }
            return mapper.Map<BookDto>(book);
        }

        public async Task<IList<BookDto>> GetByIsbnAsync(string isbn)
        {
            var book = await bookRepository.GetOneByAsync(expression: _ => _.Isbn.Equals(isbn),
                cancellationToken: CancellationToken.None);

            return new List<BookDto> { mapper.Map<BookDto>(book) };
        }

        public async Task<bool> CreateAsync(BookDto bookDto)
        {
            bookDtoValidator.Validate(bookDto);
            authorDtoValidator.Validate(bookDto.Author);        

            var book = await bookRepository.GetOneByAsync(expression: _ => _.Id.Equals(bookDto.Id),
                cancellationToken: CancellationToken.None);
            if (book is not null)
            {
                logger.LogInformation("Book is already existing. There is ValidationException.");
                throw new ValidationException("Book is already existing.");
            }

            var entityBook = mapper.Map(bookDto, book);

            await bookRepository.CreateAsync(entityBook, cancellationToken: CancellationToken.None);

            return true;
        }

        public async Task<bool> UpdateAsync(int id, BookDto updatedBookDto)
        {
            bookDtoValidator.Validate(updatedBookDto);
            authorDtoValidator.Validate(updatedBookDto.Author);

            var existingBook = await bookRepository.GetOneByAsync(expression: book => book.Id.Equals(id),
                cancellationToken: CancellationToken.None);
            if (existingBook is null)
            {
                logger.LogInformation("Book not found Exception.");
                ThrowBookNotFoundException();
            } 
            else
            {
                updatedBookDto.Id = id;
                mapper.Map(updatedBookDto, existingBook);
            }

            await bookRepository.UpdateAsync(existingBook, cancellationToken: CancellationToken.None);

            return true;
        }

        public async Task<bool> DeleteAsync(int bookId)
        {
            var bookToDelete = await bookRepository.GetOneByAsync(expression: _ => _.Id.Equals(bookId),
                cancellationToken: CancellationToken.None);
            if (bookToDelete is null)
            {
                logger.LogInformation("Book not found Exception.");
                ThrowBookNotFoundException();
            } 

            await bookRepository.DeleteAsync(bookToDelete!, cancellationToken: CancellationToken.None);

            return true;
        }

        private BookDto ThrowBookNotFoundException() =>
            throw new NotFoundException("Book not found");
    }
}