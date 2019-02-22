namespace Stations.DataProcessor
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;

    using Newtonsoft.Json;

    using Data;
    using Dto.Export;
    using Models.Enums;
    using Formatting = Newtonsoft.Json.Formatting;


    public class Serializer
	{
		public static string ExportDelayedTrains(StationsDbContext context, string dateAsString)
        {
            var date = DateTime.ParseExact(dateAsString, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            var trains = context.Trains
                .Where(t => t.Trips.Any(tr => tr.Status == TripStatus.Delayed &&
                                              tr.DepartureTime <= date))
                .Select(t => new
                {
                    t.TrainNumber,
                    DelayedTimes = t.Trips
                        .Count(tr => tr.Status == TripStatus.Delayed &&
                                     tr.DepartureTime <= date),
                    MaxDelayedTime = t.Trips
                        .Where(tr => tr.Status == TripStatus.Delayed &&
                                     tr.DepartureTime <= date)
                        .OrderByDescending(tr => tr.TimeDifference)
                        .Select(tr => tr.TimeDifference)
                        .First()
                })
                .OrderByDescending(t => t.DelayedTimes)
                .ThenByDescending(t => t.MaxDelayedTime)
                .ThenBy(t => t.TrainNumber)
                .ToArray();

             var result = JsonConvert.SerializeObject(trains, Formatting.Indented);
             return result;
        }

		public static string ExportCardsTicket(StationsDbContext context, string cardType)
        {
            var sb = new StringBuilder();

            var type = Enum.Parse<CardType>(cardType);

            var cards = context.Cards
                .Where(c => c.Type == type && 
                            c.BoughtTickets.Any())
                .Select(c => new ExportCardDto
                {
                    Name = c.Name,
                    Type = c.Type.ToString(),
                    Tickets = c.BoughtTickets
                        .Select(t => new ExportCardTicketDto
                        {
                            OriginStation = t.Trip.OriginStation.Name,
                            DestinationStation = t.Trip.DestinationStation.Name,
                            DepartureTime = t.Trip.DepartureTime.ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture)
                        })
                        .ToArray()
                })
                .OrderBy(c => c.Name)
                .ToArray();


            var serializer = new XmlSerializer(typeof(ExportCardDto[]), new XmlRootAttribute("Cards"));
            var xmlNamespaces = new XmlSerializerNamespaces(new []{XmlQualifiedName.Empty});
            serializer.Serialize(new StringWriter(sb), cards, xmlNamespaces);

            return sb.ToString().Trim();
        }
	}
}