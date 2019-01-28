namespace P012_BillsPaymentSystemTPH.Data
{
    using Microsoft.EntityFrameworkCore;
    using ModelConfigs;
    using Models;
    using Models.PaymentMethods;

    public class BillsPaymentSystemTphContext : DbContext
    {
        public BillsPaymentSystemTphContext()
        {
        }

        public BillsPaymentSystemTphContext(DbContextOptions options) 
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<BankAccount> BankAccounts { get; set; }

        public DbSet<CreditCard> CreditCards { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PaymentMethodConfig());
        }
    }
}