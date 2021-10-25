namespace SoftUni.Data
{
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class SoftUniContext : DbContext
    {
        public SoftUniContext()
        {
        }

        public SoftUniContext(DbContextOptions<SoftUniContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Addresses { get; set; }

        public virtual DbSet<Department> Departments { get; set; }

        public virtual DbSet<Employee> Employees { get; set; }

        public virtual DbSet<EmployeeProject> EmployeesProjects { get; set; }

        public virtual DbSet<Project> Projects { get; set; }

        public virtual DbSet<Town> Towns { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=PIRO\\SQLEXPRESS2019;Database=SoftUni;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            builder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
    }
}
