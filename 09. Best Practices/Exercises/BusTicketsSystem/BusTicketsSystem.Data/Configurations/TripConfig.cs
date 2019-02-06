namespace BusTicketsSystem.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;
    using Models.Enums;

    public class TripConfig : IEntityTypeConfiguration<Trip>
    {
        public void Configure(EntityTypeBuilder<Trip> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasOne(e => e.OriginBusStation)
                .WithMany(b => b.Departure)
                .HasForeignKey(e => e.OriginBusStationId);

            builder.HasOne(e => e.DestinationBusStation)
                .WithMany(b => b.Arrivals)
                .HasForeignKey(e => e.DestinationBusStationId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.BusCompany)
                .WithMany(b => b.Trips)
                .HasForeignKey(e => e.BusCompanyId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(e => e.Status)
                .HasDefaultValue(Status.Scheduled);
        }
    }
}