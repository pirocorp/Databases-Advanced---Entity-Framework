using Microsoft.EntityFrameworkCore;

namespace Stations.Data
{
    using Models;

    public class StationsDbContext : DbContext
	{
		public StationsDbContext()
		{
		}

		public StationsDbContext(DbContextOptions options)
			: base(options)
		{
		}

        public DbSet<CustomerCard> Cards { get; set; }

        public DbSet<SeatingClass> SeatingClasses { get; set; }

        public DbSet<Station> Stations { get; set; }

        public DbSet<Ticket> Tickets { get; set; }

        public DbSet<Train> Trains { get; set; }

        public DbSet<TrainSeat> TrainSeats { get; set; }

        public DbSet<Trip> Trips { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseSqlServer(Configuration.connectionString);
			}
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Station>()
                .HasIndex(s => s.Name)
                .IsUnique();

            modelBuilder.Entity<Train>()
                .HasIndex(t => t.TrainNumber)
                .IsUnique();

            modelBuilder.Entity<SeatingClass>()
                .HasIndex(sc => sc.Name)
                .IsUnique();

            modelBuilder.Entity<SeatingClass>()
                .HasIndex(sc => sc.Abbreviation)
                .IsUnique();

            modelBuilder.Entity<Station>()
                .HasMany(s => s.TripsTo)
                .WithOne(t => t.DestinationStation)
                .HasForeignKey(t => t.DestinationStationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Station>()
                .HasMany(s => s.TripsFrom)
                .WithOne(t => t.OriginStation)
                .HasForeignKey(t => t.OriginStationId)
                .OnDelete(DeleteBehavior.Restrict);
        }
	}
}