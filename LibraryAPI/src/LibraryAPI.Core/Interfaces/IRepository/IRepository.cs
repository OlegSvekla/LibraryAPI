using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace LibraryAPI.Core.Interfaces.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<bool> SaveChangesAsync();
        Task<T> GetOneByAsync(Func<IQueryable<T>,
            IIncludableQueryable<T, object>>? include = null,
            Expression<Func<T, bool>>? expression = null);
        Task<IEnumerable<T>> GetAllAsync(Func<IQueryable<T>,
          IIncludableQueryable<T, object>>? include = null,
          Expression<Func<T, bool>>? expression = null);
    }
}