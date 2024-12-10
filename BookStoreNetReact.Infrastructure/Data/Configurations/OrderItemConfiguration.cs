using BookStoreNetReact.Domain.Entities;
using BookStoreNetReact.Domain.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace BookStoreNetReact.Infrastructure.Data.Configurations
{
    public class OrderItemConfiguration : BaseEntityConfiguration<OrderItem>
    {
        public override void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            base.Configure(builder);
            builder
               .HasOne(oi => oi.Book)
               .WithMany()
               .HasForeignKey(oi => oi.BookId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
