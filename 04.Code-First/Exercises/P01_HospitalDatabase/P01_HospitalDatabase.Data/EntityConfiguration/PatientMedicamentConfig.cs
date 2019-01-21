//On Many end Describe One to Many relation -- In Child Table (Foreign Key) Location
//One to Many relation with Fluent API is described as
//builder.HasOne(x => x.Entity) -- Describe one end as navigation property
//	  .WithMany(x => x.ICollection) -- Describe many end as navigation property
//	  .HasForeignKey(x => x.SomeId) -- Describe which property is Foreign Key

namespace P01_HospitalDatabase.Data.EntityConfiguration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;

    public class PatientMedicamentConfig : IEntityTypeConfiguration<PatientMedicament>
    {
        public void Configure(EntityTypeBuilder<PatientMedicament> builder)
        {
            builder.HasKey(pm => new { pm.MedicamentId, pm.PatientId });

            builder.Property(pm => pm.PatientId)
                .HasColumnName("PatientID");

            builder.Property(pm => pm.MedicamentId)
                .HasColumnName("MedicamentID");

            //Example
            //builder.HasOne(x => x.Patient)
            //    .WithMany(x => x.Prescriptions)
            //    .HasForeignKey(x => x.PatientId);
        }
    }
}