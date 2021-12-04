namespace Theatre.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    using static ValidationConstants.Play;

    public class PlayConfig : IEntityTypeConfiguration<Play>
    {
        public void Configure(EntityTypeBuilder<Play> builder)
        {
            builder
                .HasKey(p => p.Id);

            builder
                .Property(p => p.Title)
                .HasMaxLength(TitleMaxLength)
                .IsRequired();

            builder
                .Property(p => p.Description)
                .HasMaxLength(DescriptionMaxLength)
                .IsRequired();

            builder
                .Property(p => p.Screenwriter)
                .HasMaxLength(ScreenwriterMaxLength)
                .IsRequired();
        }
    }
}
