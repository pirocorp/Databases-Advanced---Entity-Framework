namespace TeisterMask.DataProcessor
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Xml;
    using System.Xml.Serialization;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Data;
    using ExportDto;
    using Newtonsoft.Json;

    using Formatting = Newtonsoft.Json.Formatting;

    public class Serializer
    {
        public static string ExportProjectWithTheirTasks(TeisterMaskContext context)
        {
            var mapper = GetMapper();

            var projects = context.Projects
                .Where(p => p.Tasks.Count > 0)
                .ToArray()
                .AsQueryable()
                .ProjectTo<ProjectDto>(mapper.ConfigurationProvider)
                .OrderByDescending(p => p.TasksCount)
                .ThenBy(p => p.Name)
                .ToList();

            return SerializeXml("Projects", projects);
        }

        public static string ExportMostBusiestEmployees(TeisterMaskContext context, DateTime date)
        {
            var employees = context.Employees
                .Where(e => e.EmployeesTasks.Select(et => et.Task).Any(t => t.OpenDate >= date))
                .OrderByDescending(e => e.EmployeesTasks.Count(t => t.Task.OpenDate >= date))
                .ThenBy(e => e.Username)
                .Select(e => new
                {
                    Username = e.Username,
                    Tasks = e.EmployeesTasks
                        .Select(et => et.Task)
                        .Where(t => t.OpenDate >= date)
                        .OrderByDescending(t => t.DueDate)
                        .ThenBy(t => t.Name)
                        .Select(t => new
                        {
                            TaskName = t.Name,
                            OpenDate = t.OpenDate.ToString("d", CultureInfo.InvariantCulture),
                            DueDate = t.DueDate.ToString("d", CultureInfo.InvariantCulture),
                            LabelType = t.LabelType.ToString(),
                            ExecutionType = t.ExecutionType.ToString()
                        })
                })
                .Take(10)
                .ToList();

            return JsonConvert.SerializeObject(
                employees,
                new JsonSerializerSettings()
                {
                    Formatting = Formatting.Indented,
                    NullValueHandling = NullValueHandling.Ignore
                });
        }

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

        private static IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<TeisterMaskProfile>();
            });

            return config.CreateMapper();
        }
    }
}