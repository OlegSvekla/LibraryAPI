using LibraryAPI.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryAPI.Infrastructure.Configuration
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("Books");

            builder.HasKey(b => b.Id);

            builder.Property(b => b.Title)
                .IsRequired();

            builder.Property(b => b.Isbn)
                .IsRequired();

            builder.HasOne(b => b.Author)
                .WithMany() // Убрано навигационное свойство из связи один-ко-многим
                .HasForeignKey(b => b.AuthorId)
                .IsRequired();
        }
    }

}
