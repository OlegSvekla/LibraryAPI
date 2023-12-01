using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryAPI.Domain.Interfaces.IService
{
    public interface IBookService<T> where T : class
    {
        Task<IList<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<IList<T>> GetByIsbnAsync(string isbn);

        Task<bool> CreateAsync(T book);
        Task<bool> UpdateAsync(int id,T book);
        Task<bool> DeleteAsync(int id);      
    }
}