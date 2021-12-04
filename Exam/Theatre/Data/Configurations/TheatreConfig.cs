namespace Theatre.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    using static ValidationConstants.Theatre;

    public class TheatreConfig : IEntityTypeConfiguration<Theatre>
    {
        public void Configure(EntityTypeBuilder<Theatre> builder)
        {
            builder
                .HasKey(t => t.Id);

            builder
                .Property(t => t.Name)
                .HasMaxLength(NameMaxLength)
                .IsRequired();

            builder
                .Property(t => t.Director)
                .HasMaxLength(DirectorMaxLength)
                .IsRequired();
        }
    }
}
