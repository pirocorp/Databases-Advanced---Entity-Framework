namespace TeamBuilder.Data.Configs
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;

    public class TeamConfig : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(25);

            builder.HasIndex(t => t.Name)
                .IsUnique();

            builder.Property(t => t.Description)
                .HasMaxLength(32);

            builder.Property(t => t.Acronym)
                .IsRequired()
                .HasMaxLength(3);

            builder.HasOne(t => t.Creator)
                .WithMany(c => c.CreatedTeams)
                .HasForeignKey(t => t.CreatorId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}