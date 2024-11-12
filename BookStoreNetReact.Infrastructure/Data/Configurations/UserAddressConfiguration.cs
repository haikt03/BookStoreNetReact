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
            builder.Property(ua => ua.Street).IsRequired();
            builder.Property(ua => ua.Alley).IsRequired();
            builder.Property(ua => ua.HouseNumber).IsRequired();
        }
    }
}
