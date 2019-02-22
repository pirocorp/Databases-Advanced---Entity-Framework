namespace PetClinic.Data.Configs
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;

    public class AnimalAidConfig : IEntityTypeConfiguration<AnimalAid>
    {
        public void Configure(EntityTypeBuilder<AnimalAid> builder)
        {
            builder.HasKey(a => a.Id);
        }
    }
}