namespace P03_FootballBetting.Data.ModelConfigs
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;

    public class PlayerConfig : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            builder.HasKey(p => p.PlayerId);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.IsInjured)
                .HasDefaultValue(false);

            builder.HasOne(p => p.Team)
                .WithMany(t => t.Players)
                .HasForeignKey(t => t.TeamId);

            builder.HasOne(p => p.Position)
                .WithMany(t => t.Players)
                .HasForeignKey(t => t.PositionId);
        }
    }
}