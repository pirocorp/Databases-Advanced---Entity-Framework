namespace VaporStore.DataProcessor
{
	using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Xml;
    using System.Xml.Serialization;
    using Data;
    using Data.Models.Enums;
    using ExportResults;
    using Newtonsoft.Json;
    using Formatting = Newtonsoft.Json.Formatting;

    public static class Serializer
	{
		public static string ExportGamesByGenres(VaporStoreDbContext context, string[] genreNames)
        {
            var genres = context.Genres
                .Where(g => genreNames.Contains(g.Name))
                .ToList()
                .Select(g => new
                {
                    Id = g.Id,
                    Genre = g.Name,
                    Games = g.Games
                        .Where(gm => gm.Purchases.Any())
                        .Select(gm => new
                        {
                            Id = gm.Id,
                            Title = gm.Name,
                            Developer = gm.Developer.Name,
                            Tags = string.Join(", ", gm.GameTags.Select(gt => gt.Tag.Name).ToList()),
                            Players = gm.Purchases.Count
                        })
                        .OrderByDescending(g => g.Players)
                        .ThenBy(g => g.Id)
                        .ToList(),
                    TotalPlayers = g.Games.Sum(g => g.Purchases.Count)
                })
                .OrderByDescending(g => g.TotalPlayers)
                .ThenBy(g => g.Id)
                .ToList();


            return SerializeJson(genres);
        }

		public static string ExportUserPurchasesByType(VaporStoreDbContext context, string storeType)
        {
            var purchaseType = Enum.Parse<PurchaseType>(storeType);

            var users = context.Users
                .Where(u => u.Cards.Any(c => c.Purchases.Any(p => p.Type == purchaseType)))
                .Select(u => new UserExportDto()
                {
                    Username = u.Username,
                    Purchases = u.Cards
                        .SelectMany(c => c.Purchases)
                        .Where(p => p.Type == purchaseType)
                        .OrderBy(p => p.Date)
                        .Select(p => new PurchaseExportDto()
                        {
                            Card = p.Card.Number,
                            Cvc = p.Card.Cvc,
                            Date = p.Date.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture),
                            Game = new GameExportDto()
                            {
                                Title = p.Game.Name,
                                Genre = p.Game.Genre.Name,
                                Price = p.Game.Price
                            }
                        })
                        .ToArray()
                })
                .ToArray()
                .OrderByDescending(u => u.TotalSpent)
                .ThenBy(u => u.Username)
                .ToList();

            return SerializeXml("Users", users);
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