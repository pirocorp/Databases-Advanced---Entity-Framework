namespace UsersData.Data.ModelsConfig
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;

    public class AlbumUserConfig : IEntityTypeConfiguration<AlbumUser>
    {
        public void Configure(EntityTypeBuilder<AlbumUser> builder)
        {
            builder.HasKey(au => new {au.AlbumId, au.UserId});

            builder
                .HasOne(au => au.Album)
                .WithMany(a => a.Users)
                .HasForeignKey(au => au.AlbumId);

            builder
                .HasOne(au => au.User)
                .WithMany(u => u.Albums)
                .HasForeignKey(au => au.UserId);
        }
    }
}