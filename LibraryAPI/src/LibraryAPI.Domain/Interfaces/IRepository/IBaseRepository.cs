using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using TaskTracker.Domain.Entities;

namespace LibraryAPI.Domain.Interfaces.IRepository
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync(Func<IQueryable<T>,
            IIncludableQueryable<T, object>>? include = null,
            Expression<Func<T, bool>>? expression = null,
            CancellationToken cancellationToken = default);

        Task<T> GetOneByAsync(Func<IQueryable<T>,
            IIncludableQueryable<T, object>>? include = null,
            Expression<Func<T, bool>>? expression = null,
            CancellationToken cancellationToken = default);

        Task<T> CreateAsync(T entity, CancellationToken cancellationToken = default);

        Task UpdateAsync(T entity, CancellationToken cancellationToken = default);

        Task DeleteAsync(T entity, CancellationToken cancellationToken = default);
    }
}