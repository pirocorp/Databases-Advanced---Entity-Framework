using System;

namespace RelicFinder.Data
{
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class RelicFinderDbContext : DbContext
    {
        public RelicFinderDbContext()
        {
        }

        public RelicFinderDbContext(DbContextOptions options) 
            : base(options)
        {
        }

        public DbSet<Relic> Relics { get; set; }

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
