using LibraryAPI.Domain.Entities;
using LibraryAPI.Domain.Entities.Auth;
using LibraryAPI.Infrastructure.EntitiesConfiguration;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Infrastructure.Data
{
    public class LibraryDbContext : DbContext
    {
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<User> User { get; set; }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }

        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
        {          
        }
    
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BookConfiguration());
            modelBuilder.ApplyConfiguration(new AuthorConfiguration());
            modelBuilder.ApplyConfiguration(new RefreshTokenConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}