﻿namespace FastFood.Data.Configs
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;

    public class OrderItemConfig : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(e => new {e.OrderId, e.ItemId});

            builder.HasOne(e => e.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(e => e.OrderId);

            builder.HasOne(e => e.Item)
                .WithMany(i => i.OrderItems)
                .HasForeignKey(e => e.ItemId);
        }
    }
}