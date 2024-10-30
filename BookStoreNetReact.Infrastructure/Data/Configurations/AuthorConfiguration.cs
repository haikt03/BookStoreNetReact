using BookStoreNetReact.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStoreNetReact.Infrastructure.Data.Configurations
{
    public class AuthorConfiguration : BaseEntityConfiguration<Author>
    {
        public override void Configure(EntityTypeBuilder<Author> builder)
        {
            base.Configure(builder);
            builder.Property(a => a.FullName).IsRequired();
            builder.Property(a => a.Biography).IsRequired();
            builder.Property(a => a.Country).IsRequired();
        }
    }
}
