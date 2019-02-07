namespace BusTicketsSystem.Data.Configurations
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class ArrivedTripConfig : IEntityTypeConfiguration<ArrivedTrip>
    {
        
        public void Configure(EntityTypeBuilder<ArrivedTrip> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasOne(e => e.OriginBusStation)
                .WithMany(b => b.ActualDeparture)
                .HasForeignKey(e => e.OriginBusStationId);

            builder.HasOne(e => e.DestinationBusStation)
                .WithMany(b => b.ActualArrivals)
                .HasForeignKey(e => e.DestinationBusStationId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}