using BookStoreNetReact.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStoreNetReact.Infrastructure.Data.Configurations
{
    public class UserAddressConfiguration : BaseEntityConfiguration<UserAddress>
    {
        public override void Configure(EntityTypeBuilder<UserAddress> builder)
        {
            base.Configure(builder);
            builder.Property(ua => ua.City).IsRequired();
            builder.Property(ua => ua.District).IsRequired();
            builder.Property(ua => ua.Ward).IsRequired();
            builder.Property(ua => ua.SpecificAddress).IsRequired();
            builder.Property(ua => ua.UserId).IsRequired();
        }
    }
}
