namespace P01_HospitalDatabase.Data.EntityConfiguration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;

    public class VisitationConfig : IEntityTypeConfiguration<Visitation>
    {
        public void Configure(EntityTypeBuilder<Visitation> builder)
        {
            builder.HasKey(v => v.VisitationId);

            builder.Property(v => v.VisitationId)
                .HasColumnName("VisitationID");

            builder.Property(v => v.PatientId)
                .HasColumnName("PatientID");

            builder.Property(v => v.DoctorId)
                .HasColumnName("DoctorID");

            builder.Property(x => x.Comments)
                .HasMaxLength(250)
                .IsUnicode();
        }
    }
}