namespace BusTicketsSystem.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;

    public class TicketConfig : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasOne(e => e.Customer)
                .WithMany(c => c.Tickets)
                .HasForeignKey(e => e.CustomerId);

            builder.HasOne(e => e.Trip)
                .WithMany(t => t.Tickets)
                .HasForeignKey(e => e.TripId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(e => e.Seat)
                .IsRequired();

            builder.Property(e => e.Price)
                .HasDefaultValue(0);
        }
    }
}