namespace Cars.Data.Models.Configurations
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class CarConfig : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            //One-to-One
            builder
                .HasOne(c => c.LicensePlate)
                .WithOne(lp => lp.Car)
                .HasForeignKey<LicensePlate>(lp => lp.CarId);

            //One-To-Many
            builder
                .HasOne(c => c.Engine)
                .WithMany(e => e.Cars)
                .HasForeignKey(c => c.EngineId);

            //One-To-Many
            builder
                .HasOne(c => c.Make)
                .WithMany(m => m.Cars)
                .HasForeignKey(c => c.MakeId);
        }
    }
}