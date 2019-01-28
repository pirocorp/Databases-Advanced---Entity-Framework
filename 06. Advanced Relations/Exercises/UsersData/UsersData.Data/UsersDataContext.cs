namespace UsersData.Data
{
    using Microsoft.EntityFrameworkCore;
    using Models;
    using ModelsConfig;
    using P01_BillsPaymentSystem.Data;

    public class UsersDataContext : DbContext
    {
        public UsersDataContext()
        {
        }

        public UsersDataContext(DbContextOptions options) 
            : base(options)
        {
        }

        public DbSet<Album> Albums { get; set; }

        public DbSet<AlbumPicture> AlbumsPictures { get; set; }

        public DbSet<AlbumTag> AlbumsTags { get; set; }

        public DbSet<AlbumUser> AlbumsUsers { get; set; }

        public DbSet<Picture> Pictures { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<UserFriend> UsersFriends { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AlbumConfig());
            modelBuilder.ApplyConfiguration(new AlbumPictureConfig());
            modelBuilder.ApplyConfiguration(new AlbumTagConfig());
            modelBuilder.ApplyConfiguration(new AlbumUserConfig());
            modelBuilder.ApplyConfiguration(new PictureConfig());
            modelBuilder.ApplyConfiguration(new TagConfig());
            modelBuilder.ApplyConfiguration(new UserConfig());
            modelBuilder.ApplyConfiguration(new UserFriendConfig());
        }
    }
}
