using LibraryAPI.Domain.Entities;
using LibraryAPI.Infrastructure.EntitiesConfiguration;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Infrastructure.Data
{
    public class LibraryDbContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }

        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
        {          
        }
    
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BookConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}