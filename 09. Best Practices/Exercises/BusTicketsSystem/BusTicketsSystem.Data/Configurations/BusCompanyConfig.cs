namespace BusTicketsSystem.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;

    public class BusCompanyConfig : IEntityTypeConfiguration<BusCompany>
    {
        public void Configure(EntityTypeBuilder<BusCompany> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasOne(e => e.Country)
                .WithMany(c => c.BusCompanies)
                .HasForeignKey(e => e.CountryId);

            builder.Property(e => e.Name)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(100);

            builder.Property(e => e.Rating)
                .HasDefaultValue(0);
        }
    }
}