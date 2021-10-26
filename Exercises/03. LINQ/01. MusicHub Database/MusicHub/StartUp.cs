namespace MusicHub
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Data;
    using Initializer;

    public static class StartUp
    {
        public static void Main(string[] args)
        {
            MusicHubDbContext context = 
                new MusicHubDbContext();

            DbInitializer.ResetDatabase(context);

            //Test your solutions here
        }

        public static string ExportAlbumsInfo(MusicHubDbContext context, int producerId)
        {
            var albums = context.Albums
                .Where(a => a.ProducerId == producerId)
                .Select(a => new
                {
                    a.Name,
                    a.ReleaseDate,
                    ProducerName = a.Producer.Name,
                    Songs = a.Songs
                        .Select(s => new
                        {
                            Name = s.Name,
                            s.Price,
                            Writer = s.Writer.Name
                        }).ToList()
                })
                .ToList();

            var result = new StringBuilder();

            var orderedAlbums = albums
                .OrderByDescending(a => a.Songs.Sum(s => s.Price))
                .ToList();

            foreach (var album in orderedAlbums)
            {
                result.AppendLine($"-AlbumName: {album.Name}");
                result.AppendLine($"-ReleaseDate: {album.ReleaseDate:MM/dd/yyyy}");
                result.AppendLine($"-ProducerName: {album.ProducerName}");
                result.AppendLine($"-Songs:");

                var sortedSongs = album.Songs
                    .OrderByDescending(s => s.Name)
                    .ThenBy(s => s.Writer)
                    .ToList();

                for (var i = 0; i < sortedSongs.Count; i++)
                {
                    var song = sortedSongs[i];

                    result.AppendLine($"---#{i + 1}");
                    result.AppendLine($"---SongName: {song.Name}");
                    result.AppendLine($"---Price: {song.Price:F2}");
                    result.AppendLine($"---Writer: {song.Writer}");
                }

                result.AppendLine($"-AlbumPrice: {album.Songs.Sum(s => s.Price):F2}");
            }

            return result.ToString().Trim();
        }

        public static string ExportSongsAboveDuration(MusicHubDbContext context, int duration)
        {
            var songs = context.Songs
                .Where(s => s.Duration.TotalSeconds > duration)
                .Select(s => new
                {
                    Name = s.Name,
                    PerformerFullName = s.SongPerformers
                                            .FirstOrDefault()
                                            .Performer
                                            .FirstName
                                        + " "
                                        + s.SongPerformers
                                            .FirstOrDefault()
                                            .Performer
                                            .LastName,
                    WriterName = s.Writer.Name,
                    AlbumProducer = s.Album.Producer.Name,
                    Duration = s.Duration.ToString("c")
                })
                .OrderBy(s => s.Name)
                .ThenBy(s => s.WriterName)
                .ThenBy(s => s.PerformerFullName)
                .ToList();

            var result = new StringBuilder();
            var songCount = 1;

            foreach (var song in songs)
            {
                result.AppendLine($"-Song #{songCount++}");
                result.AppendLine($"---SongName: {song.Name}");
                result.AppendLine($"---Writer: {song.WriterName}");
                result.AppendLine($"---Performer: {song.PerformerFullName.Trim()}");
                result.AppendLine($"---AlbumProducer: {song.AlbumProducer}");
                result.AppendLine($"---Duration: {song.Duration}");
            }

            return result.ToString();
        }

        private static string FormatOutput(this IEnumerable<string> enumeration)
            => string.Join(Environment.NewLine, enumeration);
    }
}
