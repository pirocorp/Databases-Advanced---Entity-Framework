namespace CarDealer.Data.Configs
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;

    public class PartCarConfig : IEntityTypeConfiguration<PartCar>
    {
        public void Configure(EntityTypeBuilder<PartCar> builder)
        {
            builder.HasKey(pc => new {pc.PartId, pc.CarId});

            builder.HasOne(pc => pc.Part)
                .WithMany(p => p.Cars)
                .HasForeignKey(pc => pc.PartId);

            builder.HasOne(pc => pc.Car)
                .WithMany(c => c.Parts)
                .HasForeignKey(pc => pc.CarId);
        }
    }
}