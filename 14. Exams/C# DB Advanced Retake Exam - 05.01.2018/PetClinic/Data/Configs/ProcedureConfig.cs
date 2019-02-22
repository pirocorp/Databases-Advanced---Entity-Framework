namespace PetClinic.Data.Configs
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;

    public class ProcedureConfig : IEntityTypeConfiguration<Procedure>
    {
        public void Configure(EntityTypeBuilder<Procedure> builder)
        {
            builder.HasKey(p => p.Id);

            builder.HasOne(p => p.Animal)
                .WithMany(a => a.Procedures)
                .HasForeignKey(p => p.AnimalId);

            builder.HasOne(p => p.Vet)
                .WithMany(v => v.Procedures)
                .HasForeignKey(p => p.VetId);
        }
    }
}