namespace SoftUni.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> entity)
        {
            entity.Property(e => e.ProjectId).HasColumnName("ProjectID");

            entity.Property(e => e.Description).HasColumnType("ntext");

            entity.Property(e => e.EndDate).HasColumnType("smalldatetime");

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.StartDate).HasColumnType("smalldatetime");
        }
    }
}
