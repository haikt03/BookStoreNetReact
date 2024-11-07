using BookStoreNetReact.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStoreNetReact.Infrastructure.Data.Configurations
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.Property(au => au.UserName).IsRequired();
            builder.HasIndex(au => au.UserName).IsUnique();
            builder.Property(au => au.Email).IsRequired();
            builder.HasIndex(au => au.Email).IsUnique();
            builder.Property(au => au.PhoneNumber).IsRequired();
            builder.HasIndex(au => au.PhoneNumber).IsUnique();
            builder.Property(au => au.FullName).IsRequired();
            builder.Property(au => au.DateOfBirth).IsRequired();
            builder
                .HasOne(au => au.Address)
                .WithOne(ua => ua.User)
                .HasForeignKey<UserAddress>(ua => ua.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            builder
                .HasMany(au => au.RefreshTokens)
                .WithOne(rt => rt.User)
                .HasForeignKey(rt => rt.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
