using LibraryAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Infrastructure.EntitiesConfiguration
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Title).IsRequired();
            builder.Property(b => b.Isbn).IsRequired();
            builder.Property(b => b.Genre).IsRequired();
            builder.Property(b => b.Description).IsRequired();
            builder.Property(b => b.BorrowedDate).IsRequired();
            builder.Property(b => b.ReturnDate).IsRequired();

            builder.HasOne(b => b.Author)
                .WithMany()
                .HasForeignKey(b => b.AuthorId)
                .IsRequired();

            builder.Navigation(b => b.Author).AutoInclude();
        }
    }
}