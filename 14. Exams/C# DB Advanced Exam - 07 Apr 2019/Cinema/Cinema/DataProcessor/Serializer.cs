namespace Cinema.DataProcessor
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;
    using Data;
    using ExportDto;
    using Newtonsoft.Json;
    using Formatting = Newtonsoft.Json.Formatting;

    public class Serializer
    {
        public static string ExportTopMovies(CinemaContext context, int rating)
        {
            var movies = context.Movies
                .Where(m => m.Rating >= rating && m.Projections.Any(p => p.Tickets.Count > 0))
                .Select(m => new MovieExportDto
                {
                    MovieName = m.Title,
                    Rating = m.Rating.ToString("F2"),
                    TotalIncomes = m.Projections.Sum(p => p.Tickets.Sum(t => t.Price)).ToString("F2"),
                    Customers = m.Projections
                        .SelectMany(p => p.Tickets
                            .Select(t => t.Customer)
                            .Select(c => new CustomerMovieExportDto
                            {
                                FirstName = c.FirstName,
                                LastName = c.LastName,
                                Balance = c.Balance.ToString("F2"),
                            }))
                        .OrderByDescending(c => c.Balance)
                        .ThenBy(c => c.FirstName)
                        .ThenBy(c => c.LastName)
                        .ToList()
                })
                .OrderByDescending(m => double.Parse(m.Rating))
                .ThenByDescending(m => decimal.Parse(m.TotalIncomes))
                .Take(10)
                .ToList();

            return JsonConvert.SerializeObject(movies, Formatting.Indented);
        }

        public static string ExportTopCustomers(CinemaContext context, int age)
        {
            var timeSpanFormat = "hh\\:mm\\:ss";
            var serializer = new XmlSerializer(typeof(CustomerExportDto[]), new XmlRootAttribute("Customers"));

            var customers = context
                .Customers
                .Where(a => a.Age >= age)
                .OrderByDescending(x => x.Tickets.Sum(p => p.Price))
                .Take(10)
                .Select(x => new CustomerExportDto
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    SpentMoney = x.Tickets.Sum(p => p.Price).ToString("F2"),
                    SpentTime = TimeSpan.FromSeconds(
                            x.Tickets.Sum(s => s.Projection.Movie.Duration.TotalSeconds))
                        .ToString(timeSpanFormat)
                })
                .ToArray();

            var sb = new StringBuilder();
            var namespaces = new XmlSerializerNamespaces(new []{XmlQualifiedName.Empty});
            serializer.Serialize(new StringWriter(sb), customers, namespaces);
            
            return sb.ToString().TrimEnd();
        }
    }
}