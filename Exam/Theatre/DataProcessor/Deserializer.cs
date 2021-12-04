namespace Theatre.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;
    using Data.Models;
    using Data.Models.Enums;
    using ImportDto;
    using Newtonsoft.Json;
    using Theatre.Data;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfulImportPlay
            = "Successfully imported {0} with genre {1} and a rating of {2}!";

        private const string SuccessfulImportActor
            = "Successfully imported actor {0} as a {1} character!";

        private const string SuccessfulImportTheatre
            = "Successfully imported theatre {0} with #{1} tickets!";

        public static string ImportPlays(TheatreContext context, string xmlString)
        {
            var output = new StringBuilder();

            var playImportDtos = DeserializeXml<PlayImportDto>("Plays", xmlString);

            foreach (var playDto in playImportDtos)
            {
                if (!IsValid(playDto))
                {
                    output.AppendLine(ErrorMessage);
                    continue;
                }

                ProcessPlay(playDto, output, context);
            }

            return output.ToString();
        }

        public static string ImportCasts(TheatreContext context, string xmlString)
        {
            var output = new StringBuilder();

            var castsImportDtos = DeserializeXml<CastImportDto>("Casts", xmlString);

            foreach (var castsDto in castsImportDtos)
            {
                if (!IsValid(castsDto))
                {
                    output.AppendLine(ErrorMessage);
                    continue;
                }

                ProcessCast(castsDto, output, context);
            }

            return output.ToString();
        }

        public static string ImportTtheatersTickets(TheatreContext context, string jsonString)
        {
            var output = new StringBuilder();

            var theatres = JsonConvert.DeserializeObject<TheatreImportDto[]>(jsonString);

            foreach (var theatreDto in theatres)
            {
                if (!IsValid(theatreDto))
                {
                    output.AppendLine(ErrorMessage);
                    continue;
                }

                ProcessTheatre(theatreDto, output, context);
            }

            return output.ToString();
        }

        private static bool IsValid(object obj)
        {
            var validator = new ValidationContext(obj);
            var validationRes = new List<ValidationResult>();

            var result = Validator.TryValidateObject(obj, validator, validationRes, true);
            return result;
        }

        private static IEnumerable<TDto> DeserializeXml<TDto>(string rootAttribute, string inputXml)
        {
            var serializer = new XmlSerializer(typeof(TDto[]),
                new XmlRootAttribute(rootAttribute));

            var userDtos = (TDto[])serializer.Deserialize(new StringReader(inputXml));
            return userDtos;
        }

        private static void ProcessPlay(PlayImportDto playDto, StringBuilder output, TheatreContext context)
        {
            var validDuration = TimeSpan.TryParseExact(
                    playDto.Duration, 
                    "c", 
                    CultureInfo.InvariantCulture, 
                    out var duration);

            var validGenre = Enum.TryParse<Genre>(playDto.Genre, out var genre);

            if (!validDuration || !validGenre || duration < ValidationConstants.Play.DurationMinValue)
            {
                output.AppendLine(ErrorMessage);
                return;
            }

            var play = new Play()
            {
                Title = playDto.Title,
                Duration = duration,
                Rating = playDto.Rating,
                Genre = genre,
                Description = playDto.Description,
                Screenwriter = playDto.Screenwriter
            };

            context.Add(play);
            context.SaveChanges();

            output.AppendLine(string.Format(SuccessfulImportPlay, play.Title, play.Genre.ToString(), play.Rating));
        }

        private static void ProcessCast(CastImportDto castsDto, StringBuilder output, TheatreContext context)
        {
            var cast = new Cast()
            {
                FullName = castsDto.FullName,
                IsMainCharacter = castsDto.IsMainCharacter,
                PhoneNumber = castsDto.PhoneNumber,
                PlayId = castsDto.PlayId
            };

            context.Add(cast);
            context.SaveChanges();

            var character = cast.IsMainCharacter ? "main" : "lesser";

            output.AppendLine(string.Format(SuccessfulImportActor, cast.FullName, character));
        }

        private static void ProcessTheatre(TheatreImportDto theatreDto, StringBuilder output, TheatreContext context)
        {
            var theatre = new Theatre()
            {
                Name = theatreDto.Name,
                NumberOfHalls = theatreDto.NumberOfHalls,
                Director = theatreDto.Director
            };

            context.Add(theatre);
            context.SaveChanges();

            var tickets = new List<Ticket>();

            foreach (var ticketDto in theatreDto.Tickets)
            {
                if (!IsValid(ticketDto)
                    || ticketDto.Price > ValidationConstants.Ticket.PriceMaxValue 
                    || ticketDto.Price < ValidationConstants.Ticket.PriceMinValue)
                {
                    output.AppendLine(ErrorMessage);
                    continue;
                }

                var ticket = new Ticket()
                {
                    Price = ticketDto.Price,
                    RowNumber = ticketDto.RowNumber,
                    PlayId = ticketDto.PlayId,
                    TheatreId = theatre.Id,
                };

                tickets.Add(ticket);
            }

            context.AddRange(tickets);
            context.SaveChanges();

            output.AppendLine(string.Format(SuccessfulImportTheatre, theatre.Name, tickets.Count));
        }
    }
}
