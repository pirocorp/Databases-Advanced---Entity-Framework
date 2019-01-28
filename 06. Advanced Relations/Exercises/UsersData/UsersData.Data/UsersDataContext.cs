namespace UsersData.Data
{
    using Microsoft.EntityFrameworkCore;
    using Models;
    using P01_BillsPaymentSystem.Data;

    public class UsersDataContext : DbContext
    {
        public UsersDataContext()
        {
        }

        public UsersDataContext(DbContextOptions options) 
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
