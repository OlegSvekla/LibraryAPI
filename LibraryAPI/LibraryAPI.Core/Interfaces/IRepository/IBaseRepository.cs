using System.Linq.Expressions;

namespace LibraryAPI.Core.Interfaces.IRepository
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> GetById(int id);
        void Update(T entity);
        List<T> GetAll();
        Task<List<T>> GetAllAsync();
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
        Task CreateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<bool> SaveChangesAsync();   
    }
}