namespace PetClinic.Data.Configs
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;

    public class ProcedureAnimalAidConfig : IEntityTypeConfiguration<ProcedureAnimalAid>
    {
        public void Configure(EntityTypeBuilder<ProcedureAnimalAid> builder)
        {
            builder.HasKey(pa => new {pa.ProcedureId, pa.AnimalAidId});

            builder.HasOne(pa => pa.Procedure)
                .WithMany(p => p.ProcedureAnimalAids)
                .HasForeignKey(pa => pa.ProcedureId);

            builder.HasOne(pa => pa.AnimalAid)
                .WithMany(a => a.AnimalAidProcedures)
                .HasForeignKey(pa => pa.AnimalAidId);
        }
    }
}