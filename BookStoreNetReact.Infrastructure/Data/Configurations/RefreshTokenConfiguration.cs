using BookStoreNetReact.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStoreNetReact.Infrastructure.Data.Configurations
{
    public class RefreshTokenConfiguration : BaseEntityConfiguration<RefreshToken>
    {
        public override void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            base.Configure(builder);
            builder.Property(rt => rt.Token).IsRequired();
            builder.Property(rt => rt.ExpiresAt).IsRequired();
        }
    }
}
