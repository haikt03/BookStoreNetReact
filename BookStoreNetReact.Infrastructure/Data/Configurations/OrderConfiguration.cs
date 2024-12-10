using BookStoreNetReact.Domain.Entities;
using BookStoreNetReact.Domain.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace BookStoreNetReact.Infrastructure.Data.Configurations
{
    public class OrderConfiguration : BaseEntityConfiguration<Order>
    {
        public override void Configure(EntityTypeBuilder<Order> builder)
        {
            base.Configure(builder);
            builder.HasIndex(o => o.Code).IsUnique();
            builder.HasIndex(o => o.PaymentIntentId).IsUnique();
            builder.OwnsOne(o => o.ShippingAddress, osa =>
            {
                osa.Property(sa => sa.City).IsRequired();
                osa.Property(sa => sa.District).IsRequired();
                osa.Property(sa => sa.Ward).IsRequired();
                osa.Property(sa => sa.SpecificAddress).IsRequired();
            });
            builder
                .HasOne(o => o.User)
                .WithMany()
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            builder
                .HasMany(o => o.Items)
                .WithOne()
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
