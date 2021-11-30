namespace VaporStore.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class GameTagConfig : IEntityTypeConfiguration<GameTag>
    {
        public void Configure(EntityTypeBuilder<GameTag> builder)
        {
            builder
                .HasKey(gt => new { gt.GameId, gt.TagId });

            builder
                .HasOne(gt => gt.Game)
                .WithMany(g => g.GameTags)
                .HasForeignKey(gt => gt.GameId);

            builder
                .HasOne(gt => gt.Tag)
                .WithMany(t => t.GameTags)
                .HasForeignKey(gt => gt.TagId);
        }
    }
}
