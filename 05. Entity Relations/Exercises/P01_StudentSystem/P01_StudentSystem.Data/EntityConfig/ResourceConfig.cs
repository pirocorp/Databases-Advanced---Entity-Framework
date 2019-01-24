namespace P01_StudentSystem.Data.EntityConfig
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;

    public class ResourceConfig : IEntityTypeConfiguration<Resource>
    {
        public void Configure(EntityTypeBuilder<Resource> builder)
        {
            builder.Property(r => r.Name)
                .HasMaxLength(50)
                .IsUnicode()
                .IsRequired();

            builder.Property(r => r.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsRequired();
        }
    }
}