namespace TeisterMask.DataProcessor
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;
    using Data;
    using ExportDtos;
    using Newtonsoft.Json;
    using Formatting = Newtonsoft.Json.Formatting;

    public class Serializer
    {
        public static string ExportProjectWithTheirTasks(TeisterMaskContext context)
        {
            var serializer = new XmlSerializer(typeof(ProjectExportDto[]), new XmlRootAttribute("Projects"));

            var projects = context.Projects
                .Where(p => p.Tasks.Count >= 1)
                .OrderByDescending(p => p.Tasks.Count)
                .ThenBy(p => p.Name)
                .Select(p => new ProjectExportDto()
                {
                    TasksCount = p.Tasks.Count,
                    ProjectName = p.Name,
                    HasEndDate = p.DueDate == null ? "No" : "Yes",
                    Tasks = p.Tasks
                        .OrderBy(t => t.Name)
                        .Select(t => new ProjectTaskExportDto()
                        {
                            Name = t.Name,
                            Label = t.LabelType.ToString()
                        })
                        .ToArray(),
                })
                .ToArray();

            var sb = new StringBuilder();
            var namespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            serializer.Serialize(new StringWriter(sb), projects, namespaces);

            return sb.ToString().TrimEnd();
        }

        public static string ExportMostBusiestEmployees(TeisterMaskContext context, 
            DateTime date)
        {
            const string dateFormat = "d";

            var employees = context.Employees
                .Where(e => e.EmployeesTasks.Any(et => et.Task.OpenDate >= date))
                .OrderByDescending(e => e.EmployeesTasks.Count(et => et.Task.OpenDate >= date))
                .ThenBy(e => e.Username)
                .Take(10)
                .Select(e => new MostBusiestEmployeeExportDto()
                {
                    Username = e.Username,
                    Tasks = e.EmployeesTasks
                        .Select(et => et.Task)
                        .Where(t => t.OpenDate >= date)
                        .OrderByDescending(t => t.DueDate)
                        .ThenBy(t => t.Name)
                        .Select(t => new MostBusiestEmployeeTaskDto()
                        {
                            TaskName = t.Name,
                            OpenDate = t.OpenDate.ToString(dateFormat, CultureInfo.InvariantCulture),
                            DueDate = t.DueDate.ToString(dateFormat, CultureInfo.InvariantCulture),
                            LabelType = t.LabelType.ToString(),
                            ExecutionType = t.ExecutionType.ToString()
                        })
                        .ToArray()
                })
                .ToArray();

            return JsonConvert.SerializeObject(employees, Formatting.Indented);
        }
    }
}