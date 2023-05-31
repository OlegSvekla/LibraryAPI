using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryAPI.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Infrastructure.Data
{
    

    public class LibraryDbContext : DbContext
    {
        public DbSet<BookDto> Books { get; set; }
        public DbSet<AuthorDto> Authors { get; set; }

        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Настройки модели и связей между сущностями

            // Пример настройки связи один-ко-многим между книгами и авторами
            modelBuilder.Entity<BookDto>()
            .HasOne(b => b.Author)
            .WithMany(a => a.Books)
            .HasForeignKey(b => b.AuthorId);
        }
    }
}
