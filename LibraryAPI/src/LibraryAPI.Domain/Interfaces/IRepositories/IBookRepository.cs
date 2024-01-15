using LibraryAPI.Domain.Entities;
using LibraryAPI.Domain.Interfaces.IRepositories;

namespace TaskTracker.Domain.Interfaces.IRepositories
{
    public interface IBookRepository : IBaseRepository<Book>
    {
    }
}