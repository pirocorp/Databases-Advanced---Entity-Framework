namespace Demo.Data
{
    using Microsoft.EntityFrameworkCore;
    using Models;
    using ModelsConfig;

    public class DemoContext : DbContext
    {
        public DemoContext()
        {
        }

        public DemoContext(DbContextOptions options) 
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Storage> Storages { get; set; }

        public DbSet<ProductStock> ProductsStocks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductConfig());
            modelBuilder.ApplyConfiguration(new StorageConfig());
            modelBuilder.ApplyConfiguration(new ProductStockConfig());
        }
    }
}