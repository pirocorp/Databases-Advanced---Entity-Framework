namespace Cinema.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;

    using Newtonsoft.Json;

    using Data;
    using Data.Models;
    using ImportDto;

    public class Deserializer
    {
        private const string ERROR_MESSAGE = "Invalid data!";
        private const string SUCCESSFUL_IMPORT_MOVIE 
            = "Successfully imported {0} with genre {1} and rating {2}!";
        private const string SUCCESSFUL_IMPORT_HALL_SEAT 
            = "Successfully imported {0}({1}) with {2} seats!";
        private const string SUCCESSFUL_IMPORT_PROJECTION 
            = "Successfully imported projection {0} on {1}!";
        private const string SUCCESSFUL_IMPORT_CUSTOMER_TICKET 
            = "Successfully imported customer {0} {1} with bought tickets: {2}!";

        public static string ImportMovies(CinemaContext context, string jsonString)
        {
            var movieImportDtos = JsonConvert.DeserializeObject<MovieImportDto[]>(jsonString);

            var movieEntities = new List<Movie>();
            var sb = new StringBuilder();

            foreach (var dto in movieImportDtos)
            {
                if (IsValid(dto))
                {
                    var movie = new Movie()
                    {
                        Title = dto.Title,
                        Genre = dto.Genre,
                        Duration = dto.Duration,
                        Rating = dto.Rating,
                        Director = dto.Director,
                    };

                    movieEntities.Add(movie);
                    sb.AppendLine(string.Format(SUCCESSFUL_IMPORT_MOVIE, movie.Title, movie.Genre, movie.Rating.ToString("F2")));
                }
                else
                {
                    sb.AppendLine(ERROR_MESSAGE);
                }
            }

            context.Movies.AddRange(movieEntities);
            context.SaveChanges();

            return sb.ToString().Trim();
        }
        
        public static string ImportHallSeats(CinemaContext context, string jsonString)
        {
            var importHallDtos = JsonConvert.DeserializeObject<HallSeatsImportDto[]>(jsonString);
            var halls = new List<Hall>();

            var sb = new StringBuilder();

            foreach (var dto in importHallDtos)
            {
                if (IsValid(dto))
                {
                    var hall = new Hall()
                    {
                        Name = dto.Name,
                        Is4Dx = dto.Is4Dx,
                        Is3D = dto.Is3D,
                        Seats = GetSeats(dto.Seats),
                    };

                    halls.Add(hall);

                    var hallType = GetHallType(hall);
                    sb.AppendLine(string.Format(SUCCESSFUL_IMPORT_HALL_SEAT, hall.Name, hallType, hall.Seats.Count));
                }
                else
                {
                    sb.AppendLine(ERROR_MESSAGE);
                }
            }

            context.Halls.AddRange(halls);
            context.SaveChanges();

            return sb.ToString().Trim();
        }

        public static string ImportProjections(CinemaContext context, string xmlString)
        {
            const string dateTimeInputFormat = "yyyy-MM-dd HH:mm:ss";
            const string dateTimeOutputFormat = "MM/dd/yyyy";

            var serializer = new XmlSerializer(typeof(ProjectionImportDto[]), new XmlRootAttribute("Projections"));
            var projectImportDtos = (ProjectionImportDto[])serializer.Deserialize(new StringReader(xmlString));

            var projections = new List<Projection>();
            var sb = new StringBuilder();

            foreach (var dto in projectImportDtos)
            {
                var isParsed = DateTime.TryParseExact(dto.DateTime, dateTimeInputFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var dateTime);

                if (IsValid(dto) 
                    && isParsed
                    && MovieExists(context, dto.MovieId) 
                    && HallExists(context, dto.HallId))
                {
                    var projection =  new Projection()
                    {
                        MovieId = dto.MovieId,
                        HallId = dto.HallId,
                        DateTime = dateTime,
                    };

                    var movie = context.Movies.Find(dto.MovieId);

                    projections.Add(projection);
                    sb.AppendLine(string.Format(SUCCESSFUL_IMPORT_PROJECTION, movie.Title, dateTime.ToString(dateTimeOutputFormat)));
                }
                else
                {
                    sb.AppendLine(ERROR_MESSAGE);
                }
            }

            context.Projections.AddRange(projections);
            context.SaveChanges();
            return sb.ToString().Trim();
        }

        public static string ImportCustomerTickets(CinemaContext context, string xmlString)
        {
            var serializer = new XmlSerializer(typeof(CustomerImportDto[]), new XmlRootAttribute("Customers"));
            var customerImportDtos = (CustomerImportDto[])serializer.Deserialize(new StringReader(xmlString));

            var customers = new List<Customer>();
            var sb = new StringBuilder();

            foreach (var dto in customerImportDtos)
            {
                if (IsValid(dto))
                {
                   var customer = new Customer()
                   {
                       FirstName = dto.FirstName,
                       LastName = dto.LastName,
                       Age = dto.Age,
                       Balance = dto.Balance,
                       Tickets = GetTickets(context, dto) 
                   };

                   customers.Add(customer);
                   sb.AppendLine(string.Format(SUCCESSFUL_IMPORT_CUSTOMER_TICKET, customer.FirstName, customer.LastName, customer.Tickets.Count));
                }
                else
                {
                    sb.AppendLine(ERROR_MESSAGE);
                }
            }

            context.Customers.AddRange(customers);
            context.SaveChanges();
            return sb.ToString().Trim();
        }

        private static bool IsValid(object obj)
        {
            var context = new ValidationContext(obj);
            var validationResults = new List<ValidationResult>();

            return Validator.TryValidateObject(obj, context, validationResults, true);
        }

        private static ICollection<Seat> GetSeats(int count)
        {
            var seats = new List<Seat>();

            for (var i = 0; i < count; i++)
            {
                seats.Add(new Seat());
            }

            return seats;
        }

        private static string GetHallType(Hall hall)
        {
            var hallType = "Normal";

            if (hall.Is4Dx && hall.Is3D)
            {
                hallType = "4Dx/3D";
            }
            else if (hall.Is4Dx)
            {
                hallType = "4Dx";
            }
            else if (hall.Is3D)
            {
                hallType = "3D";
            }

            return hallType;
        }

        private static bool HallExists(CinemaContext context, int id)
        {
            return context.Halls
                .Any(h => h.Id == id);
        }

        private static bool MovieExists(CinemaContext context, int id)
        {
            return context.Movies
                .Any(m => m.Id == id);
        }

        private static bool ProjectionExists(CinemaContext context, int id)
        {
            return context.Projections
                .Any(h => h.Id == id);
        }

        private static ICollection<Ticket> GetTickets(CinemaContext context, CustomerImportDto customer)
        {
            var tickets = new List<Ticket>();

            foreach (var dto in customer.Tickets)
            {
                if (ProjectionExists(context, dto.ProjectionId))
                {
                    var ticket = new Ticket()
                    {
                        ProjectionId = dto.ProjectionId,
                        Price = dto.Price,
                    };

                    tickets.Add(ticket);
                }
            }

            return tickets;
        }
    }
}