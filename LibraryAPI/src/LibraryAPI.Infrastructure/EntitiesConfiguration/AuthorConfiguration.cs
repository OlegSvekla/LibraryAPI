using LibraryAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryAPI.Infrastructure.EntitiesConfiguration
{
    public class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.FirstName).IsRequired();
            builder.Property(b => b.LastName).IsRequired();
        }
    }
}