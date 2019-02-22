namespace Stations.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;

    using Microsoft.EntityFrameworkCore;

    using AutoMapper;
    using Newtonsoft.Json;

    using Data;
    using Dto.Import;
    using Models;
    using Models.Enums;
    using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

    public static class Deserializer
	{
		private const string FAILURE_MESSAGE = "Invalid data format.";
		private const string SUCCESS_MESSAGE = "Record {0} successfully imported.";

		public static string ImportStations(StationsDbContext context, string jsonString)
        {
            var result = new StringBuilder();

            var deserializedStations = JsonConvert.DeserializeObject<StationDto[]>(jsonString);

            var validStations = new List<Station>();
            foreach (var stationDto in deserializedStations)
            {
                if (!IsValid(stationDto))
                {
                    result.AppendLine(FAILURE_MESSAGE);
                    continue;
                }

                if (stationDto.Town == null)
                {
                    stationDto.Town = stationDto.Name;
                }

                var stationExists = validStations.Any(s => s.Name == stationDto.Name);
                if (stationExists)
                {
                    result.AppendLine(FAILURE_MESSAGE);
                    continue;
                }

                var station = Mapper.Map<Station>(stationDto);
                validStations.Add(station);
                result.AppendLine(string.Format(SUCCESS_MESSAGE, station.Name));
            }

            context.Stations.AddRange(validStations);
            context.SaveChanges();
            return result.ToString().Trim();
        }

        public static string ImportClasses(StationsDbContext context, string jsonString)
        {
            var deserializedClasses = JsonConvert.DeserializeObject<ClassDto[]>(jsonString);

            var result = new StringBuilder();
            var validClasses = new List<SeatingClass>();
            foreach (var classDto in deserializedClasses)
            {
                if (!IsValid(classDto))
                {
                    result.AppendLine(FAILURE_MESSAGE);
                    continue;
                }

                if (validClasses.Any(c => c.Name == classDto.Name) ||
                    validClasses.Any(c => c.Abbreviation == classDto.Abbreviation))
                {
                    result.AppendLine(FAILURE_MESSAGE);
                    continue;
                }

                var seatingClass = Mapper.Map<SeatingClass>(classDto);
                validClasses.Add(seatingClass);
                result.AppendLine(String.Format(SUCCESS_MESSAGE, seatingClass.Name));
            }
            context.SeatingClasses.AddRange(validClasses);
            context.SaveChanges();

            return result.ToString().Trim();
        }

		public static string ImportTrains(StationsDbContext context, string jsonString)
		{
            //var jsonSettings = new JsonSerializerSettings
            //{
            //    NullValueHandling = NullValueHandling.Ignore
            //};

            var trainDtos = JsonConvert.DeserializeObject<TrainDto[]>(jsonString/*, jsonSettings*/);

            var result = new StringBuilder();
            var validTrains = new List<Train>();

            foreach (var trainDto in trainDtos)
            {
                if (!IsValid(trainDto))
                {
                    result.AppendLine(FAILURE_MESSAGE);
                    continue;
                }

                var trainAlreadyExists = validTrains.Any(t => t.TrainNumber == trainDto.TrainNumber);

                if (trainAlreadyExists)
                {
                    result.AppendLine(FAILURE_MESSAGE);
                    continue;
                }

                var seatsAreValid = trainDto.Seats.All(IsValid);
                if (!seatsAreValid)
                {
                    result.AppendLine(FAILURE_MESSAGE);
                    continue;
                }

                var seatingClassesAreValid = trainDto.Seats
                    .All(s => context.SeatingClasses.Any(x => x.Name == s.Name &&
                                                              x.Abbreviation == s.Abbreviation));

                if (!seatingClassesAreValid)
                {
                    result.AppendLine(FAILURE_MESSAGE);
                    continue;
                }

                var type = Enum.Parse<TrainType>(trainDto.Type ?? "HighSpeed");

                var trainSeats = trainDto.Seats.Select(s => new TrainSeat
                {
                    SeatingClass = context.SeatingClasses
                        .SingleOrDefault(sc => sc.Name == s.Name && 
                                               sc.Abbreviation == s.Abbreviation),
                    Quantity = s.Quantity.Value
                })
                .ToArray();

                var train = new Train
                {
                    TrainNumber = trainDto.TrainNumber,
                    Type = type,
                    TrainSeats = trainSeats
                };

                validTrains.Add(train);
                result.AppendLine(string.Format(SUCCESS_MESSAGE, train.TrainNumber));
            }

            context.Trains.AddRange(validTrains);
            context.SaveChanges();
            return result.ToString().Trim();
        }

		public static string ImportTrips(StationsDbContext context, string jsonString)
		{
            var tripDtos = JsonConvert.DeserializeObject<TripDto[]>(jsonString/*, jsonSettings*/);

            var result = new StringBuilder();
            var validTrips = new List<Trip>();

            foreach (var tripDto in tripDtos)
            {
                if (!IsValid(tripDto))
                {
                    result.AppendLine(FAILURE_MESSAGE);
                    continue;
                }

                var train = context.Trains.SingleOrDefault(t => t.TrainNumber == tripDto.Train);
                var originStation = context.Stations.SingleOrDefault(s => s.Name == tripDto.OriginStation);
                var destinationStation = context.Stations.SingleOrDefault(s => s.Name == tripDto.DestinationStation);
                if (train == null || 
                    originStation == null || 
                    destinationStation == null)
                {
                    result.AppendLine(FAILURE_MESSAGE);
                    continue;
                }

                var dateTimeFormat = "dd/MM/yyyy HH:mm";
                var departureTime = DateTime.ParseExact(tripDto.DepartureTime, dateTimeFormat, CultureInfo.InvariantCulture);
                var arrivalTime = DateTime.ParseExact(tripDto.ArrivalTime, dateTimeFormat, CultureInfo.InvariantCulture);

                TimeSpan timeDifference;

                if (tripDto.TimeDifference != null)
                {
                    var timeSpanFormat = @"hh\:mm";
                    timeDifference = TimeSpan.ParseExact(tripDto.TimeDifference, timeSpanFormat, CultureInfo.InvariantCulture);
                }

                if (departureTime > arrivalTime)
                {
                    result.AppendLine(FAILURE_MESSAGE);
                    continue;
                }

                var status = Enum.Parse<TripStatus>(tripDto.Status ?? "OnTime");

                var trip = new Trip
                {
                    Train = train,
                    OriginStation = originStation,
                    DestinationStation = destinationStation,
                    DepartureTime = departureTime,
                    ArrivalTime = arrivalTime,
                    Status = status,
                    TimeDifference = timeDifference
                };

                validTrips.Add(trip);
                result.AppendLine($"Trip from {trip.OriginStation.Name} to {trip.DestinationStation.Name} imported.");
            }

            context.Trips.AddRange(validTrips);
            context.SaveChanges();
            return result.ToString().Trim();
        }

		public static string ImportCards(StationsDbContext context, string xmlString)
        {
            var serializer = new XmlSerializer(typeof(CardDto[]), new XmlRootAttribute("Cards"));
            var deserializedCards = (CardDto[])serializer.Deserialize(new StringReader(xmlString));

            var result = new StringBuilder();
            var validCards = new List<CustomerCard>();
            foreach (var cardDto in deserializedCards)
            {
                if (!IsValid(cardDto))
                {
                    result.AppendLine(FAILURE_MESSAGE);
                    continue;
                }

                var cardType = Enum.Parse<CardType>(cardDto.CardType ?? "Normal");

                var card = new CustomerCard()
                {
                    Name = cardDto.Name,
                    Age = cardDto.Age,
                    Type = cardType
                };

                validCards.Add(card);
                result.AppendLine(string.Format(SUCCESS_MESSAGE, card.Name));
            }
            context.Cards.AddRange(validCards);
            context.SaveChanges();
            return result.ToString().Trim();
        }

		public static string ImportTickets(StationsDbContext context, string xmlString)
		{
            var serializer = new XmlSerializer(typeof(TicketDto[]), new XmlRootAttribute("Tickets"));
            var deserializedTickets = (TicketDto[])serializer.Deserialize(new MemoryStream(Encoding.UTF8.GetBytes(xmlString)));

            var sb = new StringBuilder();

            var validTickets = new List<Ticket>();
            foreach (var ticketDto in deserializedTickets)
            {
                if (!IsValid(ticketDto))
                {
                    sb.AppendLine(FAILURE_MESSAGE);
                    continue;
                }

                var departureTime =
                    DateTime.ParseExact(ticketDto.Trip.DepartureTime, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);

                var trip = context.Trips
                    .Include(t => t.OriginStation)
                    .Include(t => t.DestinationStation)
                    .Include(t => t.Train)
                    .ThenInclude(t => t.TrainSeats)
                    .SingleOrDefault(t => t.OriginStation.Name == ticketDto.Trip.OriginStation &&
                                                              t.DestinationStation.Name == ticketDto.Trip.DestinationStation &&
                                                              t.DepartureTime == departureTime);
                if (trip == null)
                {
                    sb.AppendLine(FAILURE_MESSAGE);
                    continue;
                }

                CustomerCard card = null;
                if (ticketDto.Card != null)
                {
                    card = context.Cards.SingleOrDefault(c => c.Name == ticketDto.Card.Name);

                    if (card == null)
                    {
                        sb.AppendLine(FAILURE_MESSAGE);
                        continue;
                    }
                }

                var seatingClassAbbreviation = ticketDto.Seat.Substring(0, 2);
                var quantity = int.Parse(ticketDto.Seat.Substring(2));

                var seatExists = trip.Train.TrainSeats
                    .SingleOrDefault(s => s.SeatingClass.Abbreviation == seatingClassAbbreviation && quantity <= s.Quantity);
                if (seatExists == null)
                {
                    sb.AppendLine(FAILURE_MESSAGE);
                    continue;
                }

                var seat = ticketDto.Seat;

                var ticket = new Ticket
                {
                    Trip = trip,
                    CustomerCard = card,
                    Price = ticketDto.Price,
                    SeatingPlace = seat
                };

                validTickets.Add(ticket);
                sb.AppendLine(string.Format("Ticket from {0} to {1} departing at {2} imported.",
                    trip.OriginStation.Name,
                    trip.DestinationStation.Name,
                    trip.DepartureTime.ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture)));
            }

            context.Tickets.AddRange(validTickets);
            context.SaveChanges();

            var result = sb.ToString();
            return result;
        }

        private static bool IsValid(object obj)
        {
            var validationContext = new ValidationContext(obj);
            var validationResults = new List<ValidationResult>();

            return Validator.TryValidateObject(obj, validationContext, validationResults, true);
        }
    }
}