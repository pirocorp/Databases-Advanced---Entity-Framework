namespace MusicHub.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class PerformerConfiguration : IEntityTypeConfiguration<Performer>
    {
        public void Configure(EntityTypeBuilder<Performer> builder)
        {
            builder
                .HasKey(p => p.Id);

            builder
                .Property(p => p.FirstName)
                .HasMaxLength(20)
                .IsRequired();

            builder
                .Property(p => p.LastName)
                .HasMaxLength(20)
                .IsRequired();

            builder
                .Property(p => p.Age)
                .IsRequired();

            builder
                .Property(p => p.NetWorth)
                .IsRequired();
        }
    }
}
