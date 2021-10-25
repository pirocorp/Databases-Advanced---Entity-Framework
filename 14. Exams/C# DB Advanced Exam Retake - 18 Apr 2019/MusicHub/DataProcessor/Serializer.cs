namespace MusicHub.DataProcessor
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;
    using Data;
    using Data.Models;
    using ExportDtos;
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;
    using Formatting = Newtonsoft.Json.Formatting;

    public class Serializer
    {
        public static string ExportAlbumsInfo(MusicHubDbContext context, int producerId)
        {
            const string dateTimeFormat = "MM/dd/yyyy";

            var albums = context.Albums
                .Where(a => a.ProducerId == producerId)
                .OrderByDescending(a => a.Price)
                .Select(a => new AlbumsByProducerExportDto()
                {
                    AlbumName = a.Name,
                    ReleaseDate = a.ReleaseDate.ToString(dateTimeFormat),
                    ProducerName = a.Producer.Name,
                    Songs = a.Songs
                        .Select(s => new SongAlbumByProducerExportDto()
                        {
                            SongName = s.Name,
                            Price = s.Price.ToString("F2"),
                            Writer = s.Writer.Name
                        })
                        .OrderByDescending(s => s.SongName)
                        .ThenBy(s => s.Writer)
                        .ToArray(),
                    AlbumPrice = a.Price.ToString("F2"),
                })
                .ToArray();

            return JsonConvert.SerializeObject(albums, Formatting.Indented);
        }

        public static string ExportSongsAboveDuration(MusicHubDbContext context, int duration)
        {
            const string durationFormat = "c";
            var serializer = new XmlSerializer(typeof(SongExportDto[]), new XmlRootAttribute("Songs"));

            var songs = context.Songs
                .Where(s => s.Duration.TotalSeconds > duration)
                .Include(s => s.SongPerformers)
                .ThenInclude(sp => sp.Performer)
                .Select(s => new SongExportDto()
                {
                    SongName = s.Name,
                    Writer = s.Writer.Name,
                    Performer = GetSongPerformer(s),
                    AlbumProducer = s.Album.Producer.Name,
                    Duration = s.Duration.ToString(durationFormat)
                })
                .OrderBy(s => s.SongName)
                .ToArray();

            var sb = new StringBuilder();
            var namespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            serializer.Serialize(new StringWriter(sb), songs, namespaces);

            return sb.ToString().TrimEnd();
        }

        private static string GetSongPerformer(Song song)
        {
            var performer = song.SongPerformers.FirstOrDefault()?.Performer;

            if (performer == null)
            {
                return null;
            }

            return $"{performer.FirstName} {performer.LastName}";
        }
    }
}