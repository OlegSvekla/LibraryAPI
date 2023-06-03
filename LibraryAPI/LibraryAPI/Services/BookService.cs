using AutoMapper;
using LibraryAPI.Core.Entities;
using LibraryAPI.Core.Interfaces.IRepository;
using LibraryAPI.Core.Interfaces.IService;

namespace LibraryAPI.Services
{
    public class BookService : IBookService<BookDto>
    {
        private readonly IBaseRepository<Book> _bookRepository;
        private readonly IMapper _mapper;

        public BookService(IBaseRepository<Book> bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<IList<BookDto>> GetAllBooks()
        {
            var books = await _bookRepository.GetAllAsync();
            return _mapper.Map<IList<BookDto>>(books);
        }

        public async Task<BookDto> GetBookById(int id)
        {
            var book = await _bookRepository.GetById(id);
            return _mapper.Map<BookDto>(book);
        }

        public async Task<IList<BookDto>> GetBooksByIsbn(string isbn)
        {
            var books = await _bookRepository.FirstOrDefaultAsync(b => b.Isbn == isbn);
            return _mapper.Map<IList<BookDto>>(books);
        }

        public async Task<bool> AddBook(BookDto bookDto)
        {
            var book = _mapper.Map<Book>(bookDto);
            await _bookRepository.AddAsync(book);
            return true; // Return true if the book was successfully added
        }

        public async Task<bool> UpdateBook(BookDto bookDto)
        {
            var book = _mapper.Map<Book>(bookDto);
            await _bookRepository.UpdateAsync(book);
            return true; // Return true if the book was successfully updated
        }

        public async Task<bool> DeleteBook(BookDto bookDto)
        {
            var book = _mapper.Map<Book>(bookDto);
            await _bookRepository.DeleteAsync(book);
            return true; // Return true if the book was successfully deleted
        }


    }






}
