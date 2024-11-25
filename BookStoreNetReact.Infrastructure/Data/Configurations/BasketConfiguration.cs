﻿using BookStoreNetReact.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStoreNetReact.Infrastructure.Data.Configurations
{
    public class BasketConfiguration : BaseEntityConfiguration<Basket>
    {
        public override void Configure(EntityTypeBuilder<Basket> builder)
        {
            base.Configure(builder);
            builder.Property(b => b.UserId).IsRequired();

            builder
                .HasOne(b => b.User)
                .WithOne()
                .HasForeignKey<Basket>(b => b.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            builder
                .HasMany(b => b.Items)
                .WithOne()
                .HasForeignKey(bi => bi.BasketId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
