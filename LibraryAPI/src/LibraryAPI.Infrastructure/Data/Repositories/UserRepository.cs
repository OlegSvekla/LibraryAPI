using LibraryAPI.Domain.Entities.Auth;
using LibraryAPI.Domain.Interfaces.IRepositories;

namespace LibraryAPI.Infrastructure.Data.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(LibraryDbContext dbContext) : base(dbContext)
        {
        }
    }
}