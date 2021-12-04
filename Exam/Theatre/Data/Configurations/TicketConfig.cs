namespace Theatre.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class TicketConfig : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder
                .HasKey(t => t.Id);

            builder
                .HasOne(t => t.Play)
                .WithMany(p => p.Tickets)
                .HasForeignKey(t => t.PlayId);

            builder
                .HasOne(t => t.Theatre)
                .WithMany(t => t.Tickets)
                .HasForeignKey(t => t.TheatreId);
        }
    }
}
