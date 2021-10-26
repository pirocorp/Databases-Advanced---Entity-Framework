namespace MusicHub.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class WriterConfiguration : IEntityTypeConfiguration<Writer>
    {
        public void Configure(EntityTypeBuilder<Writer> builder)
        {
            builder
                .HasKey(w => w.Id);

            builder
                .Property(w => w.Name)
                .HasMaxLength(20)
                .IsRequired();
        }
    }
}
