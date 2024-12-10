using BookStoreNetReact.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStoreNetReact.Infrastructure.Data.Configurations
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasIndex(au => au.UserName).IsUnique();
            builder.HasIndex(au => au.Email).IsUnique();
            builder.HasIndex(au => au.PhoneNumber).IsUnique();
            builder
                .HasOne(au => au.Address)
                .WithOne()
                .HasForeignKey<UserAddress>(ua => ua.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
