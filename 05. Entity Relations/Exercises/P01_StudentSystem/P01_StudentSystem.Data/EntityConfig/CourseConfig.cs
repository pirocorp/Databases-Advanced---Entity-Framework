namespace P01_StudentSystem.Data.EntityConfig
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class CourseConfig : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.Property(x => x.Name)
                .HasMaxLength(80)
                .IsUnicode()
                .IsRequired();

            builder.Property(x => x.Description)
                .IsUnicode()
                .IsRequired(false);

            builder
                .HasMany(c => c.HomeworkSubmissions)
                .WithOne(hs => hs.Course)
                .HasForeignKey(hs => hs.CourseId);

            builder
                .HasMany(c => c.Resources)
                .WithOne(r => r.Course)
                .HasForeignKey(r => r.CourseId);

            builder
                .HasMany(c => c.StudentsEnrolled)
                .WithOne(se => se.Course)
                .HasForeignKey(se => se.CourseId);
        }
    }
}