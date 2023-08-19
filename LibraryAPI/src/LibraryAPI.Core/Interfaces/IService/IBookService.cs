using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryAPI.Domain.Interfaces.IService
{
    public interface IBookService<T> where T : class
    {
        Task<IList<T>> GetAllBooks();
        Task<T> GetBookById(int id);
        Task<IList<T>> GetBooksByIsbn(string isbn);
        Task<bool> AddBook(T book);
        Task<T> UpdateBook(int id,T book);
        Task<T> DeleteBook(int id);      
    }
}