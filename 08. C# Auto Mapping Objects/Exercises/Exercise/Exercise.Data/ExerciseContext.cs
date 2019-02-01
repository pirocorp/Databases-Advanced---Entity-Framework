namespace Exercise.Data
{
    using Microsoft.EntityFrameworkCore;

    using Models;

    public class ExerciseContext : DbContext
    {
        public ExerciseContext()
        {
        }

        public ExerciseContext(DbContextOptions options) 
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasOne(e => e.Manager)
                    .WithMany(m => m.ManagedEmployees)
                    .HasForeignKey(e => e.ManagerId);
            });
        }
    }
}