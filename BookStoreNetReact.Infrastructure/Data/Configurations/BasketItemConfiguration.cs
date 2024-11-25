﻿using BookStoreNetReact.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStoreNetReact.Infrastructure.Data.Configurations
{
    public class BasketItemConfiguration : BaseEntityConfiguration<BasketItem>
    {
        public override void Configure(EntityTypeBuilder<BasketItem> builder)
        {
            base.Configure(builder);
            builder.Property(bi => bi.Quantity).IsRequired();
            builder.Property(bi => bi.BookId).IsRequired();

            builder
               .HasOne(bi => bi.Book)
               .WithMany()
               .HasForeignKey(bi => bi.BookId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
