namespace MusicHub.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class SongConfiguration : IEntityTypeConfiguration<Song>
    {
        public void Configure(EntityTypeBuilder<Song> builder)
        {
            builder
                .HasKey(s => s.Id);

            builder
                .Property(s => s.Name)
                .HasMaxLength(20)
                .IsRequired();

            builder
                .Property(s => s.Duration)
                .IsRequired();

            builder
                .Property(s => s.CreatedOn)
                .IsRequired();

            builder
                .HasOne(s => s.Album)
                .WithMany(a => a.Songs)
                .HasForeignKey(s => s.AlbumId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);

            builder
                .HasOne(s => s.Writer)
                .WithMany(w => w.Songs)
                .HasForeignKey(s => s.WriterId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Property(s => s.Price)
                .IsRequired();
        }
    }
}
