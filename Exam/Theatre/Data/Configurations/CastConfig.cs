namespace Theatre.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    using static ValidationConstants.Cast;

    public class CastConfig : IEntityTypeConfiguration<Cast>
    {
        public void Configure(EntityTypeBuilder<Cast> builder)
        {
            builder
                .HasKey(c => c.Id);

            builder
                .Property(c => c.FullName)
                .HasMaxLength(FullNameMaxLength)
                .IsRequired();

            builder
                .Property(c => c.PhoneNumber)
                .IsRequired();

            builder
                .HasOne(c => c.Play)
                .WithMany(p => p.Casts)
                .HasForeignKey(c => c.PlayId);
        }
    }
}
