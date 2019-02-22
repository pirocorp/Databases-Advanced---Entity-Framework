namespace PetClinic.Data.Configs
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;

    public class AnimalConfig : IEntityTypeConfiguration<Animal>
    {
        public void Configure(EntityTypeBuilder<Animal> builder)
        {
            builder.HasKey(a => a.Id);

            builder.HasOne(a => a.Passport)
                .WithOne(p => p.Animal)
                .HasForeignKey<Animal>(a => a.PassportSerialNumber);
        }
    }
}