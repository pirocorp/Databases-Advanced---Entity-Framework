namespace Cars.Data
{
    using Microsoft.EntityFrameworkCore;
    using Models;
    using Models.Configurations;

    public class CarsDbContext : DbContext
    {
        public CarsDbContext()
        {
        }

        public CarsDbContext(DbContextOptions options)
            :base(options)
        {
        }

        public DbSet<Make> Makes { get; set; }

        public DbSet<Car> Cars { get; set; }

        public DbSet<Engine> Engines { get; set; }

        public DbSet<LicensePlate> LicensePlates { get; set; }

        public DbSet<Dealership> Dealerships { get; set; }

        public DbSet<CarDealership> CarsDealerships { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                builder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CarConfig());
            builder.ApplyConfiguration(new CarDealershipConfig());
        }
    }
}
