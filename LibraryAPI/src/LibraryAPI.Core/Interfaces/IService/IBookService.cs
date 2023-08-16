using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryAPI.Core.Interfaces.IService
{
    public interface IBookService<T> where T : class
    {
        Task<IList<T>> GetAllBooks();
        Task<T> GetBookById(int id);
        Task<IList<T>> GetBooksByIsbn(string isbn);
        Task<bool> AddBook(T book);
        Task<bool> UpdateBook(int id,T book);
        Task<bool> DeleteBook(T book);      
    }
}