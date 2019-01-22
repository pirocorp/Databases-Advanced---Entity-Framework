//Entity Framework Fluent API is used to configure domain classes to override conventions.
//Used in OnModelCreating Method
//In Entity Framework Core, the ModelBuilder class acts as a Fluent API. By using it,
//we can configure many different things, as it provides more configuration options than data annotation attributes. 

//Entity Framework Core Fluent API configures the following aspects of a model:

//1. Model Configuration: Configures an EF model to database mappings.Configures the default Schema,
//DB functions, additional data annotation attributes and entities to be excluded from mapping.
//2. Entity Configuration: Configures entity to table and relationships mapping e.g.PrimaryKey, AlternateKey,
//Index, table name, one-to-one, one-to-many, many-to-many relationships etc.
//3. Property Configuration: Configures property to column mapping e.g.column name, default value, nullability,
//Foreign key, data type, concurrency column etc.
//------------------------------------------------------------------------------------------------------------------------
//On one end Describing Many to One relation -- In Parent Table Location
//Many to One relation with Fluent API is described as
//builder
//    .HasMany(x => x.ICollection) -- Describe many end as navigation property
//	  .WithOne(x => x.Entity) -- Describe one end as navigation property
//	  .HasForeignKey(x => x.SomeId); -- Describe which property is Foreign Key

//On Many end Describe One to Many relation -- On Foreign Key Location
//One to Many relation with Fluent API is described as
//builder
//    .HasOne(x => x.Entity) -- Describe one end as navigation property
//	  .WithMany(x => x.ICollection) -- Describe many end as navigation property
//	  .HasForeignKey(x => x.SomeId); -- Describe which property is Foreign Key
//------------------------------------------------------------------------------------------------------------------------
//Cascade delete automatically deletes the child row when the related parent row is deleted.
//You can specify any of the following DeleteBehavior values, based on your requirement:
//Cascade : Dependent entities will be deleted when the principal entity is deleted.
//ClientSetNull: The values of foreign key properties in the dependent entities will be set to null.
//Restrict: Prevents Cascade delete.
//SetNull: The values of foreign key properties in the dependent entities will be set to null.

//Configure Cascade Delete using Fluent API
//modelBuilder.Entity<PrincipleEntity>()
//    .HasMany<DependentEntity>(pe => pe.NavigationalProperty) //NavigationalProperty -> Collection of DependentEntities
//    .WithOne(de => de.NavigationalProperty) //NavigationalProperty -> Reference to PrincipleEntity
//    .HasForeignKey(de => de.ForeignKey)
//    .OnDelete(DeleteBehavior.Cascade);
//------------------------------------------------------------------------------------------------------------------------
//To configure a one-to-one relationship using Fluent API in EF Core, use the HasOne, WithOne and HasForeignKey methods. 
//On principle end
//modelBuilder.Entity<PrincipleEntity>()
//    .HasOne<DependentEntity>(pe => pe.NavigationalProperty)
//    .WithOne(de => de.NavigationalProperty)
//    .HasForeignKey<DependentEntity>(de => de.ForeignKey);
//On dependent end
//modelBuilder.Entity<DependentEntity>()
//    .HasOne<PrincipleEntity>(de => de.NavigationalProperty)
//    .WithOne(pe => pe.NavigationalProperty)
//    .HasForeignKey<DependentEntity>(de => de.ForeignKey);
//--------------------------------------------------------------------------------------------------------------------------
//Many-to-Many Relationships in Entity Framework Core
//In Entity Framework Core, we must create a joining entity class for a joining table. 
//The steps for configuring many-to-many relationships would the following:
//1. Define a new joining entity class which includes the foreign key property and the
//reference navigation property for each entity.
//2. Define a one-to-many relationship between other two entities and the joining entity,
//by including a collection navigation property in entities at both sides.
//3. Configure both the foreign keys in the joining entity as a composite key using Fluent API.
//
//modelBuilder.Entity<JoiningEntity>()
//    .HasKey(je => new { je.FirstForeignKey, je.SecondForeignKey });
//
//modelBuilder.Entity<JoiningEntity>()
//    .HasOne<FirstPrincipleEntity>(je => je.NavigationalProperty) //NavigationalProperty -> Reference to PrincipleEntity
//    .WithMany(fpe => fpe.NavigationalProperty) //NavigationalProperty -> Collection of DependentEntities
//    .HasForeignKey(je => je.FirstForeignKey);
//
//modelBuilder.Entity<JoiningEntity>()
//    .HasOne<SecondPrincipleEntity>(je => je.NavigationalProperty) //NavigationalProperty -> Reference to PrincipleEntity
//    .WithMany(spe => spe.NavigationalProperty) //NavigationalProperty -> Collection of DependentEntities
//    .HasForeignKey(je => je.SecondForeignKey);
namespace P01_HospitalDatabase.Data
{
    using EntityConfiguration;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class HospitalContext : DbContext
    {
        public HospitalContext()
        {
        }

        public HospitalContext(DbContextOptions options)
        {
        }

        public DbSet<Diagnose> Diagnoses { get; set; }

        public DbSet<Visitation> Visitations { get; set; }

        public DbSet<Patient> Patients { get; set; }

        public DbSet<Medicament> Medicaments { get; set; }

        public DbSet<PatientMedicament> PatientsMedicaments { get; set; }

        public DbSet<Doctor> Doctors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PatientConfig());
            modelBuilder.ApplyConfiguration(new VisitationConfig());
            modelBuilder.ApplyConfiguration(new DiagnoseConfig());
            modelBuilder.ApplyConfiguration(new MedicamentConfig());
            modelBuilder.ApplyConfiguration(new PatientMedicamentConfig());
            modelBuilder.ApplyConfiguration(new DoctorConfig());
        }
    }
}
