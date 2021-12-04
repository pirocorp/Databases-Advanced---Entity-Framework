namespace Theatre.DataProcessor
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Xml;
    using System.Xml.Serialization;
    using ExportDto;
    using Newtonsoft.Json;
    using Theatre.Data;
    using Formatting = Newtonsoft.Json.Formatting;

    public class Serializer
    {
        public static string ExportTheatres(TheatreContext context, int numbersOfHalls)
        {
            var theatres = context.Theatres
                .Where(t => t.NumberOfHalls >= numbersOfHalls && t.Tickets.Count >= 20)
                .OrderByDescending(t => t.NumberOfHalls)
                .ThenBy(t => t.Name)
                .Select(t => new
                {
                    Name = t.Name,
                    Halls = t.NumberOfHalls,
                    Tickets = t.Tickets
                        .Where(x => x.RowNumber >= 1 && x.RowNumber <= 5)
                        .Select(y => new
                        {
                            Price = y.Price,
                            RowNumber = y.RowNumber
                        })
                })
                .ToList()
                .Select(x => new
                {
                    x.Name,
                    x.Halls,
                    TotalIncome = x.Tickets.Sum(t => t.Price),
                    Tickets = x.Tickets.OrderByDescending(t => t.Price).ToArray()
                });

            return SerializeJson(theatres);
        }

        public static string ExportPlays(TheatreContext context, double rating)
        {
            var plays = context.Plays
                .Where(p => p.Rating <= rating)
                .OrderBy(p => p.Title)
                .ThenByDescending(p => p.Genre)
                .Select(p => new PlayExportDto()
                {
                    Title = p.Title,
                    Duration = p.Duration.ToString("c"),
                    Rating = p.Rating == 0 ? "Premier" : $"{p.Rating}",
                    Genre = p.Genre.ToString(),
                    Actors = p.Casts
                        .Where(c => c.IsMainCharacter)
                        .Select(c => new ActorExportDto()
                        {
                            FullName = c.FullName,
                            MainCharacter = $"Plays main character in '{p.Title}'."
                        })
                        .OrderByDescending(a => a.FullName)
                        .ToArray()
                })
                .ToList();
                
            return SerializeXml("Plays", plays);
        }

        private static string SerializeJson(object value)
            => JsonConvert.SerializeObject(value, new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore
            });

        private static string SerializeXml<TDto>(string rootAttribute, TDto element)
        {
            var serializer = new XmlSerializer(typeof(TDto),
                new XmlRootAttribute(rootAttribute));

            var xmlNamespaces = new XmlSerializerNamespaces(new []{XmlQualifiedName.Empty});

            StringWriter writer;
            using (writer = new StringWriter())
            {
                serializer.Serialize(writer, element, xmlNamespaces);
            }

            return writer.ToString();
        }
    }
}
