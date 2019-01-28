namespace UsersData.Data.ModelsConfig
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class AlbumPictureConfig : IEntityTypeConfiguration<AlbumPicture>
    {
        public void Configure(EntityTypeBuilder<AlbumPicture> builder)
        {
            builder.HasKey(ap => new {ap.AlbumId, ap.PictureId});

            builder
                .HasOne(ap => ap.Album)
                .WithMany(a => a.Pictures)
                .HasForeignKey(ap => ap.AlbumId);

            builder
                .HasOne(ap => ap.Picture)
                .WithMany(p => p.Albums)
                .HasForeignKey(ap => ap.PictureId);
        }
    }
}