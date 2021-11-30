namespace VaporStore.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class GameConfig : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.HasKey(g => g.Id);

            builder
                .Property(g => g.Name)
                .IsRequired();

            builder
                .Property(g => g.Price)
                .IsRequired();

            builder
                .HasOne(g => g.Developer)
                .WithMany(d => d.Games)
                .HasForeignKey(g => g.DeveloperId);

            builder
                .HasOne(g => g.Genre)
                .WithMany(gn => gn.Games)
                .HasForeignKey(g => g.GenreId);
        }
    }
}
