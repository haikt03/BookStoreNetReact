using BookStoreNetReact.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStoreNetReact.Infrastructure.Data.Configurations
{
    public class BookConfiguration : BaseEntityConfiguration<Book>
    {
        public override void Configure(EntityTypeBuilder<Book> builder)
        {
            base.Configure(builder);
            builder.Property(b => b.Name).IsRequired();
            builder.Property(b => b.Category).IsRequired();
            builder.Property(b => b.Publisher).IsRequired();
            builder.Property(b => b.PublishedYear).IsRequired();
            builder.Property(b => b.Language).IsRequired();
            builder.Property(b => b.Weight).IsRequired();
            builder.Property(b => b.NumberOfPages).IsRequired();
            builder.Property(b => b.Form).IsRequired();
            builder.Property(b => b.Description).IsRequired();
            builder.Property(b => b.Price).IsRequired();
            builder.Property(b => b.QuantityInStock).IsRequired();

            builder
                .HasOne(b => b.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(b => b.AuthorId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
