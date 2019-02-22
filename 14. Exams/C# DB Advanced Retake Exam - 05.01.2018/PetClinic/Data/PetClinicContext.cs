namespace PetClinic.Data
{
    using Microsoft.EntityFrameworkCore;

    using Configs;
    using Models;

    public class PetClinicContext : DbContext
    {
        public PetClinicContext()
        {

        }

        public PetClinicContext(DbContextOptions options)
            : base(options)
        {

        }

        public DbSet<AnimalAid> AnimalAids { get; set; }
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Passport> Passports { get; set; }
        public DbSet<ProcedureAnimalAid> ProceduresAnimalAids { get; set; }
        public DbSet<Procedure> Procedures { get; set; }
        public DbSet<Vet> Vets { get; set; }    

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new AnimalAidConfig());
            builder.ApplyConfiguration(new AnimalConfig());
            builder.ApplyConfiguration(new PassportConfig());
            builder.ApplyConfiguration(new ProcedureAnimalAidConfig());
            builder.ApplyConfiguration(new ProcedureConfig());
            builder.ApplyConfiguration(new VetConfig());
        }
    }
}