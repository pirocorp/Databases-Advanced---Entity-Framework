namespace MusicHub.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using Data;
    using Data.Models;
    using Data.Models.Enums;
    using ImportDtos;
    using Newtonsoft.Json;

    public class Deserializer
    {
        private const string ERROR_MESSAGE = "Invalid data";

        private const string SUCCESSFULLY_IMPORTED_WRITER 
            = "Imported {0}";
        private const string SUCCESSFULLY_IMPORTED_PRODUCER_WITH_PHONE 
            = "Imported {0} with phone: {1} produces {2} albums";
        private const string SUCCESSFULLY_IMPORTED_PRODUCER_WITH_NO_PHONE
            = "Imported {0} with no phone number produces {1} albums";
        private const string SUCCESSFULLY_IMPORTED_SONG 
            = "Imported {0} ({1} genre) with duration {2}";
        private const string SUCCESSFULLY_IMPORTED_PERFORMER
            = "Imported {0} ({1} songs)";

        public static string ImportWriters(MusicHubDbContext context, string jsonString)
        {
            var writerDtos = JsonConvert.DeserializeObject<WriterImportDto[]>(jsonString);

            var writerEntities = new List<Writer>();
            var sb = new StringBuilder();

            foreach (var dto in writerDtos)
            {
                if (IsValid(dto))
                {
                    var writer = new Writer()
                    {
                        Name = dto.Name,
                        Pseudonym = dto.Pseudonym,
                    };

                    writerEntities.Add(writer);
                    sb.AppendLine(string.Format(SUCCESSFULLY_IMPORTED_WRITER, writer.Name));
                }
                else
                {
                    sb.AppendLine(ERROR_MESSAGE);
                }
            }

            context.Writers.AddRange(writerEntities);
            context.SaveChanges();

            return sb.ToString().Trim();
        }

        public static string ImportProducersAlbums(MusicHubDbContext context, string jsonString)
        {
            var producerImportDtos = JsonConvert.DeserializeObject<ProducerImportDto[]>(jsonString);

            var producerEntities = new List<Producer>();
            var sb = new StringBuilder();

            foreach (var dto in producerImportDtos)
            {
                var albumsAreValid = TryGetAlbums(dto, out var albums);

                if (IsValid(dto) && albumsAreValid)
                {
                    var producer = new Producer()
                    {
                        Name = dto.Name,
                        Pseudonym = dto.Pseudonym,
                        PhoneNumber = dto.PhoneNumber,
                        Albums = albums,
                    };

                    producerEntities.Add(producer);

                    if (string.IsNullOrWhiteSpace(producer.PhoneNumber))
                    {
                        sb.AppendLine(string.Format(SUCCESSFULLY_IMPORTED_PRODUCER_WITH_NO_PHONE, 
                            producer.Name, producer.Albums.Count));
                    }
                    else
                    {
                        sb.AppendLine(string.Format(SUCCESSFULLY_IMPORTED_PRODUCER_WITH_PHONE,
                            producer.Name, producer.PhoneNumber, producer.Albums.Count));
                    }
                }
                else
                {
                    sb.AppendLine(ERROR_MESSAGE);
                }
            }

            context.Producers.AddRange(producerEntities);
            context.SaveChanges();
            return sb.ToString().Trim();
        }
        
        public static string ImportSongs(MusicHubDbContext context, string xmlString)
        {
            const string durationFormat = "c";
            const string dateTimeFormat = "dd/MM/yyyy";

            var serializer = new XmlSerializer(typeof(SongImportDto[]), new XmlRootAttribute("Songs"));
            var songImportDtos = (SongImportDto[])serializer.Deserialize(new StringReader(xmlString));
            
            var songEntities = new List<Song>();
            var sb = new StringBuilder();

            foreach (var dto in songImportDtos)
            {
                var isDurationValid = TimeSpan.TryParseExact(dto.Duration, durationFormat, 
                    CultureInfo.InvariantCulture, TimeSpanStyles.None, out var duration);

                var isCreatedOnValid = DateTime.TryParseExact(dto.CreatedOn, dateTimeFormat, 
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out var createdOn);

                var isGenreValid = Enum.TryParse<Genre>(dto.Genre, out var genre);
                
                if (IsValid(dto) 
                    && isDurationValid 
                    && isCreatedOnValid 
                    && isGenreValid
                    && AlbumIdIsValid(context, dto.AlbumId)
                    && WriterExists(context, dto.WriterId))
                {
                    var song = new Song()
                    {
                        Name = dto.Name,
                        Duration = duration,
                        CreatedOn = createdOn,
                        Genre = genre,
                        AlbumId = dto.AlbumId,
                        WriterId = dto.WriterId,
                        Price = dto.Price,
                    };

                    songEntities.Add(song);
                    sb.AppendLine(string.Format(SUCCESSFULLY_IMPORTED_SONG, song.Name, song.Genre, song.Duration.ToString(durationFormat)));
                }
                else
                {
                    sb.AppendLine(ERROR_MESSAGE);
                }
            }

            context.Songs.AddRange(songEntities);
            context.SaveChanges();
            return sb.ToString().Trim();
        }

        public static string ImportSongPerformers(MusicHubDbContext context, string xmlString)
        {
            var serializer = new XmlSerializer(typeof(PerformerImportDto[]), new XmlRootAttribute("Performers"));
            var performerImportDtos = (PerformerImportDto[])serializer.Deserialize(new StringReader(xmlString));

            var performerEntities = new List<Performer>();
            var sb = new StringBuilder();

            foreach (var dto in performerImportDtos)
            {
                if (IsValid(dto) && ValidateSongs(context, dto.PerformersSongs))
                {
                    var performer = new Performer()
                    {
                        FirstName = dto.FirstName,
                        LastName = dto.LastName,
                        Age = dto.Age,
                        NetWorth = dto.NetWorth,
                    };

                    performerEntities.Add(performer);
                    performer.PerformerSongs = GetPerformerSongs(performer, dto);

                    sb.AppendLine(string.Format(SUCCESSFULLY_IMPORTED_PERFORMER, performer.FirstName, performer.PerformerSongs.Count));
                }
                else
                {
                    sb.AppendLine(ERROR_MESSAGE);
                }
            }

            context.Performers.AddRange(performerEntities);
            context.SaveChanges();
            return sb.ToString().Trim();
        }
        
        private static bool IsValid(object obj)
        {
            var context = new ValidationContext(obj);
            var validationResults = new List<ValidationResult>();

            return Validator.TryValidateObject(obj, context, validationResults, true);
        }

        private static bool TryGetAlbums(ProducerImportDto producer, out HashSet<Album> albums)
        {
            const string dateTimeFormat = "dd/MM/yyyy";

            albums = new HashSet<Album>();

            foreach (var dto in producer.Albums)
            {
                var isParsed = DateTime.TryParseExact(dto.ReleaseDate, dateTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var dateTime);

                if (IsValid(dto) && isParsed)
                {
                    var album = new Album()
                    {
                        Name = dto.Name,
                        ReleaseDate = dateTime,
                    };

                    albums.Add(album);
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        private static bool AlbumIdIsValid(MusicHubDbContext context, int? id)
        {
            if (id == null)
            {
                return true;
            }

            return context.Albums
                .Any(h => h.Id == id.Value);
        }

        private static bool WriterExists(MusicHubDbContext context, int id)
        {
            return context.Writers
                .Any(h => h.Id == id);
        }

        private static bool SongExists(MusicHubDbContext context, int id)
        {
            return context.Songs
                .Any(h => h.Id == id);
        }

        private static bool ValidateSongs(MusicHubDbContext context, PerformerSongImportDto[] songs)
        {
            foreach (var song in songs)
            {
                if (!SongExists(context, song.Id))
                {
                    return false;
                }
            }

            return true;
        }

        private static HashSet<SongPerformer> GetPerformerSongs(Performer performer, PerformerImportDto dto)
        {
            var songs = new HashSet<SongPerformer>();

            foreach (var song in dto.PerformersSongs)
            {
                songs.Add(new SongPerformer()
                {
                    SongId = song.Id,
                    PerformerId = performer.Id,
                });
            }

            return songs;
        }
    }
}