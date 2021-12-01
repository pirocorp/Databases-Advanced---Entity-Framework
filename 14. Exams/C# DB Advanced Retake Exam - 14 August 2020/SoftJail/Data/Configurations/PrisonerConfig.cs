namespace SoftJail.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    using static Common.ValidationConstants.Prisoner;

    public class PrisonerConfig : IEntityTypeConfiguration<Prisoner>
    {
        public void Configure(EntityTypeBuilder<Prisoner> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(b => b.FullName)
                .HasMaxLength(FullNameMaxLength)
                .IsRequired();

            builder
                .Property(p => p.Nickname)
                .IsRequired();

            builder
                .HasOne(p => p.Cell)
                .WithMany(c => c.Prisoners)
                .HasForeignKey(p => p.CellId);
        }
    }
}
