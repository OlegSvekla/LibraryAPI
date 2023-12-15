using LibraryAPI.Domain.Entities;
using LibraryAPI.Domain.Interfaces.IRepository;

namespace TaskTracker.Domain.Interfaces.IRepositories
{
    public interface IBookRepository : IBaseRepository<Book>
    {
    }
}