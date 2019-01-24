namespace P01_StudentSystem.Data.EntityConfig
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;

    public class StudentConfig : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.Property(s => s.Name)
                .HasMaxLength(100)
                .IsUnicode()
                .IsRequired();

            builder.Property(s => s.PhoneNumber)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsRequired(false);

            builder
                .HasMany(s => s.HomeworkSubmissions)
                .WithOne(hs => hs.Student)
                .HasForeignKey(hs => hs.StudentId);

            builder
                .HasMany(s => s.CourseEnrollments)
                .WithOne(ce => ce.Student)
                .HasForeignKey(ce => ce.StudentId);
        }
    }
}