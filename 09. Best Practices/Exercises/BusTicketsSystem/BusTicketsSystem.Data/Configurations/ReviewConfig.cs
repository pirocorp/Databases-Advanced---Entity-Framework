namespace BusTicketsSystem.Data.Configurations
{
    using System;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;

    public class ReviewConfig : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasOne(e => e.BusStation)
                .WithMany(b => b.Reviews)
                .HasForeignKey(e => e.BusStationId);

            builder.HasOne(e => e.Customer)
                .WithMany(c => c.Reviews)
                .HasForeignKey(e => e.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(e => e.Content)
                .HasMaxLength(255);

            builder.Property(e => e.Grade)
                .HasDefaultValue(0);

            builder.Property(e => e.DateTimeOfPublishing)
                .HasDefaultValue(DateTime.Now);
        }
    }
}