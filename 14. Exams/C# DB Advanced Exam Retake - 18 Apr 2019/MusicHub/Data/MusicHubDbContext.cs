namespace MusicHub.Data
{
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class MusicHubDbContext : DbContext
    {
        public MusicHubDbContext()
        {
        }

        public MusicHubDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Album> Albums { get; set; }

        public DbSet<Performer> Performers { get; set; }

        public DbSet<Producer> Producers { get; set; }

        public DbSet<Song> Songs { get; set; }

        public DbSet<SongPerformer> SongsPerformers { get; set; }

        public DbSet<Writer> Writers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<SongPerformer>(songPerformer =>
                {
                    songPerformer.HasKey(sp => new {sp.SongId, sp.PerformerId});

                    songPerformer.HasOne(sp => sp.Song)
                        .WithMany(s => s.SongPerformers)
                        .HasForeignKey(sp => sp.SongId)
                        .OnDelete(DeleteBehavior.Restrict);

                    songPerformer.HasOne(sp => sp.Performer)
                        .WithMany(p => p.PerformerSongs)
                        .HasForeignKey(sp => sp.PerformerId)
                        .OnDelete(DeleteBehavior.Restrict);
                });

            builder.Entity<Album>(album =>
            {
                album.HasOne(a => a.Producer)
                    .WithMany(p => p.Albums)
                    .HasForeignKey(a => a.ProducerId)
                    .OnDelete(DeleteBehavior.Restrict);

                album.HasMany(a => a.Songs)
                    .WithOne(s => s.Album)
                    .HasForeignKey(s => s.AlbumId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<Writer>(writer =>
            {
                writer.HasMany(w => w.Songs)
                    .WithOne(s => s.Writer)
                    .HasForeignKey(s => s.WriterId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
