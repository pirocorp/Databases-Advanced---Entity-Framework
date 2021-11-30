namespace VaporStore.DataProcessor
{
	using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using Common;
    using Data;
    using Data.Models;
    using Data.Models.Enums;
    using Dto.Import;
    using Newtonsoft.Json;

    public static class Deserializer
	{
        private const string ErrorMessage = "Invalid Data";

        private const string GameImportSuccessMessage = "Added {0} ({1}) with {2} tags";

        private const string UserImportSuccessMessage = "Imported {0} with {1} cards";

        private const string PurchaseImportSuccessMessage = "Imported {0} for {1}";

		public static string ImportGames(VaporStoreDbContext context, string jsonString)
		{
            var output = new StringBuilder();

            var games = JsonConvert.DeserializeObject<GameImportDto[]>(jsonString);

            foreach (var game in games)
            {
                if (!IsValid(game))
                {
                    output.AppendLine(ErrorMessage);
					continue;
                }

                ProcessGame(game, output, context);
            }

            return output.ToString();
		}

        public static string ImportUsers(VaporStoreDbContext context, string jsonString)
		{
            var output = new StringBuilder();

            var users = JsonConvert.DeserializeObject<UserImportDto[]>(jsonString);

            foreach (var userDto in users)
            {
                if (!IsValid(userDto))
                {
                    output.AppendLine(ErrorMessage);
                    continue;
                }

                ProcessUser(userDto, output, context);
            }

            return output.ToString();
		}

        public static string ImportPurchases(VaporStoreDbContext context, string xmlString)
        {
            var output = new StringBuilder();

            var purchaseDtos = DeserializeXml<PurchaseImportDto>("Purchases", xmlString);

            foreach (var purchaseDto in purchaseDtos)
            {
                if (!IsValid(purchaseDto))
                {
                    output.AppendLine(ErrorMessage);
                    continue;
                }

                ProcessPurchase(purchaseDto, output, context);
            }

            return output.ToString();
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }

        private static IEnumerable<TDto> DeserializeXml<TDto>(string rootAttribute, string inputXml)
        {
            var serializer = new XmlSerializer(typeof(TDto[]),
                new XmlRootAttribute(rootAttribute));

            var userDtos = (TDto[])serializer.Deserialize(new StringReader(inputXml));
            return userDtos;
        }

        private static void ProcessPurchase(PurchaseImportDto purchaseDto, StringBuilder output, VaporStoreDbContext context)
        {
            var validDate = DateTime.TryParseExact(
                purchaseDto.Date,
                ValidationConstants.DateTimeFormat,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out var dateTime);

            var validType = Enum.TryParse<PurchaseType>(purchaseDto.Type, out var purchaseType);

            var card = context.Cards.FirstOrDefault(c => c.Number == purchaseDto.Card);

            var game = context.Games.FirstOrDefault(c => c.Name == purchaseDto.Title);

            if (!validDate || !validType || card == null || game == null)
            {
                output.AppendLine(ErrorMessage);
                return;
            }

            var username = context.Users.FirstOrDefault(u => u.Cards.Any(c => c.Number == card.Number))?.Username;

            var purchase = new Purchase()
            {
                ProductKey = purchaseDto.Key,
                Date = dateTime,
                Type = purchaseType,
                Card = card,
                Game = game
            };

            output.AppendLine(string.Format(PurchaseImportSuccessMessage, game.Name, username));
            context.Add(purchase);
            context.SaveChanges();
        }

        private static void ProcessUser(UserImportDto userDto, StringBuilder output, VaporStoreDbContext context)
        {
            if (userDto.Cards.Length < 1)
            {
                output.AppendLine(ErrorMessage);
                return;
            }

            var cards = new List<Card>();

            foreach (var cardDto in userDto.Cards)
            {
                if (!IsValid(cardDto))
                {
                    output.AppendLine(ErrorMessage);
                    return;
                }

                var success = Enum.TryParse<CardType>(cardDto.Type, out var cardType);

                if (!success)
                {
                    output.AppendLine(ErrorMessage);
                    return;
                }

                var card = new Card()
                {
                    Number = cardDto.Number,
                    Cvc = cardDto.CVC,
                    Type = cardType
                };

                cards.Add(card);
            }

            var user = new User()
            {
                Username = userDto.Username,
                FullName = userDto.FullName,
                Email = userDto.Email,
                Age = userDto.Age,
                Cards = cards
            };

            output.AppendLine(string.Format(UserImportSuccessMessage, user.Username, cards.Count));
            context.Add(user);
            context.SaveChanges();
        }

        private static void ProcessGame(GameImportDto gameDto, StringBuilder output, VaporStoreDbContext context)
        {
            if (gameDto.Tags.Length < 1)
            {
                output.AppendLine(ErrorMessage);
                return;
            }

            var success = DateTime.TryParseExact(
                gameDto.ReleaseDate, 
                ValidationConstants.DateFormat,
                CultureInfo.InvariantCulture, 
                DateTimeStyles.None, 
                out var releaseDate);

            if (!success)
            {
                output.AppendLine(ErrorMessage);
                return;
            }

            var developer = GetDeveloper(gameDto.Developer, context);
            var genre = GetGenre(gameDto.Genre, context);

            var tags = new List<Tag>();

            foreach (var gameTag in gameDto.Tags)
            {
                tags.Add(GetTag(gameTag, context));
            }

            var game = new Game()
            {
                Name = gameDto.Name,
                Price = gameDto.Price,
                ReleaseDate = releaseDate,
                Developer = developer,
                Genre = genre,
            };

            game.GameTags = tags.Select(t => new GameTag() { Tag = t, Game = game }).ToList();


            output.AppendLine(string.Format(GameImportSuccessMessage, game.Name, genre.Name, tags.Count));
            context.Add(game);
            context.SaveChanges();
        }

        private static Developer GetDeveloper(string gameDeveloper, VaporStoreDbContext context)
        {
            if (!context.Developers.Any(d => d.Name == gameDeveloper))
            {
                context.Add(new Developer()
                {
                    Name = gameDeveloper
                });

                context.SaveChanges();
            }

            return context.Developers.First(d => d.Name == gameDeveloper);
        }

        private static Genre GetGenre(string genre, VaporStoreDbContext context)
        {
            if (!context.Genres.Any(d => d.Name == genre))
            {
                context.Add(new Genre()
                {
                    Name = genre
                });

                context.SaveChanges();
            }

            return context.Genres.First(d => d.Name == genre);
        }

        private static Tag GetTag(string tag, VaporStoreDbContext context)
        {
            if (!context.Tags.Any(d => d.Name == tag))
            {
                context.Add(new Tag()
                {
                    Name = tag
                });

                context.SaveChanges();
            }

            return context.Tags.First(d => d.Name == tag);
        }
    }
}