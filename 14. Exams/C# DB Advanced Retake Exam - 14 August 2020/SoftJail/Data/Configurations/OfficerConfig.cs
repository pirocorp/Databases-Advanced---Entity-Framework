namespace SoftJail.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    using static Common.ValidationConstants.Officer;

    public class OfficerConfig : IEntityTypeConfiguration<Officer>
    {
        public void Configure(EntityTypeBuilder<Officer> builder)
        {
            builder
                .HasKey(o => o.Id);

            builder
                .Property(o => o.FullName)
                .HasMaxLength(FullNameMaxLength)
                .IsRequired();

            builder
                .HasOne(o => o.Department)
                .WithMany()
                .HasForeignKey(o => o.DepartmentId);
        }
    }
}
