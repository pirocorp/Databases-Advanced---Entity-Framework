namespace SoftJail.DataProcessor
{
    using Data;
    using System;
    using System.IO;
    using System.Linq;
    using System.Xml;
    using System.Xml.Serialization;
    using ExportDto;
    using Newtonsoft.Json;
    using Formatting = Newtonsoft.Json.Formatting;

    public class Serializer
    {
        public static string ExportPrisonersByCells(SoftJailDbContext context, int[] ids)
        {
            var prisoners = context.Prisoners
                .Where(p => ids.Contains(p.Id))
                .Select(p => new
                {
                    Id = p.Id,
                    Name = p.FullName,
                    CellNumber = p.Cell.CellNumber,
                    Officers = p.PrisonerOfficers
                        .Select(po => new
                        {
                            OfficerName = po.Officer.FullName,
                            Department = po.Officer.Department.Name,
                            Salary = po.Officer.Salary
                        })
                })
                .ToList()
                .OrderBy(p => p.Name)
                .ThenBy(p => p.Id)
                .Select(p => new
                {
                    p.Id,
                    p.Name,
                    p.CellNumber,
                    Officers = p.Officers
                        .OrderBy(o => o.OfficerName)
                        .Select(o => new
                        {
                            o.OfficerName,
                            o.Department
                        }),
                    TotalOfficerSalary = Math.Round(p.Officers.Sum(o => o.Salary), 2)
                })
                .ToList();

            return SerializeJson(prisoners);
        }

        public static string ExportPrisonersInbox(SoftJailDbContext context, string prisonersNames)
        {
            var names = prisonersNames.Split(",").ToList();

            var prisoners = context.Prisoners
                .Where(p => names.Contains(p.FullName))
                .OrderBy(p => p.FullName)
                .ThenBy(p => p.Id)
                .Select(p => new PrisonerExportDto()
                {
                    Id = p.Id,
                    Name = p.FullName,
                    IncarcerationDate = p.IncarcerationDate.ToString("yyyy-MM-dd"),
                    EncryptedMessages = p.Mails
                        .Select(m => new MessageExportDto()
                        {
                            Description = m.Description,
                        })
                        .ToArray(),
                })
                .ToList();

            prisoners.ForEach(p =>
            {
                foreach (var message in p.EncryptedMessages)
                {
                    message.Description = new string(message.Description.Reverse().ToArray());
                }
            });

            return SerializeXml("Prisoners", prisoners);
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