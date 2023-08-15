using LibraryAPI.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryAPI.Infrastructure.ConfigurationForEntity
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            //TODO DELETE COMMENT
            builder.HasOne(b => b.Author)
                .WithMany() // Убрано навигационное свойство из связи один-ко-многим
                .IsRequired();
            builder.Navigation(b => b.Author).AutoInclude();
        }
    }
}