namespace BusTicketsSystem.Data
{
    using Configurations;
    using Microsoft.EntityFrameworkCore;

    using Models;

    public class BusTicketsContext : DbContext
    {
        protected BusTicketsContext()
        {
        }

        public BusTicketsContext(DbContextOptions options) 
            : base(options)
        {
        }

        public DbSet<ArrivedTrip> ArrivedTrips { get; set; }

        public DbSet<BankAccount> BankAccounts { get; set; }

        public DbSet<BusCompany> BusCompanies { get; set; }

        public DbSet<BusStation> BusStations { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<Ticket> Tickets { get; set; }

        public DbSet<Town> Towns { get; set; }

        public DbSet<Trip> Trips { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ArrivedTripConfig());
            modelBuilder.ApplyConfiguration(new BankAccountConfig());
            modelBuilder.ApplyConfiguration(new BusCompanyConfig());
            modelBuilder.ApplyConfiguration(new BusStationConfig());
            modelBuilder.ApplyConfiguration(new CountryConfig());
            modelBuilder.ApplyConfiguration(new CustomerConfig());
            modelBuilder.ApplyConfiguration(new ReviewConfig());
            modelBuilder.ApplyConfiguration(new TicketConfig());
            modelBuilder.ApplyConfiguration(new TownConfig());
            modelBuilder.ApplyConfiguration(new TripConfig());
        }
    }
}