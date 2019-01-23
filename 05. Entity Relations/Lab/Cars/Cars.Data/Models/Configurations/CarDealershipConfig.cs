namespace Cars.Data.Models.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class CarDealershipConfig : IEntityTypeConfiguration<CarDealership>
    {
        public void Configure(EntityTypeBuilder<CarDealership> builder)
        {
            builder
                .HasKey(cd => new { cd.CarId, cd.DealershipId });

            //One-To-Many
            //These both one-to-many build one relation of type 
            //Many-To-Many
            builder
                .HasOne(cd => cd.Car)
                .WithMany(c => c.CarDealerships)
                .HasForeignKey(cd => cd.CarId);

            //One-To-Many
            builder
                .HasOne(cd => cd.Dealership)
                .WithMany(d => d.CarDealerships)
                .HasForeignKey(cd => cd.DealershipId);
        }
    }
}