namespace UsersData.Data
{
    using Microsoft.EntityFrameworkCore;
    using Models;
    using ModelsConfig;
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

        public DbSet<UserFriend> UsersFriends { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfig());
            modelBuilder.ApplyConfiguration(new UserFriendConfig());
        }
    }
}
