using LibraryAPI.Domain.Entities.Auth;
using LibraryAPI.Domain.Interfaces.IRepository;

namespace LibraryAPI.Infrastructure.Data.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(LibraryDbContext dbContext) : base(dbContext)
        {
        }
    }
}