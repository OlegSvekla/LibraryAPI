using LibraryAPI.Domain.Entities.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryAPI.Infrastructure.EntitiesConfiguration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id); 

            builder.Property(u => u.FirstName).HasMaxLength(40);
            builder.Property(u => u.LastName).HasMaxLength(40);

            builder.Property(u => u.Email).IsRequired().HasMaxLength(255);

            builder.Property(u => u.PasswordHash).IsRequired().HasMaxLength(255);

            builder.Property(u => u.VerificationToken).HasMaxLength(255);

            builder.HasMany(u => u.RefreshTokens)
                .WithOne(rt => rt.User)
                .HasForeignKey(rt => rt.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}