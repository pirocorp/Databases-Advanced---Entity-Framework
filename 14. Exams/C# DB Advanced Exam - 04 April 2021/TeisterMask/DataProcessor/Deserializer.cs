namespace TeisterMask.DataProcessor
{
    using System;
    using System.Collections.Generic;

    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using AutoMapper;
    using Data;
    using Data.Models;
    using ImportDto;
    using Newtonsoft.Json;

    using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedProject
            = "Successfully imported project - {0} with {1} tasks.";

        private const string SuccessfullyImportedEmployee
            = "Successfully imported employee - {0} with {1} tasks.";

        public static string ImportProjects(TeisterMaskContext context, string xmlString)
        {
            var output = new StringBuilder();

            var projectsDtos = DeserializeXml<ProjectDto>("Projects", xmlString);
            var mapper = GetMapper();

            foreach (var projectDto in projectsDtos)
            {
                if (!IsValid(projectDto))
                {
                    output.AppendLine(ErrorMessage);
                    continue;
                }

                var tasksDtos = projectDto.Tasks;
                projectDto.Tasks = Array.Empty<TaskDto>();

                var project = mapper.Map<Project>(projectDto);
                context.Projects.Add(project);
                context.SaveChanges();

                var tasks = new List<Task>();

                foreach (var taskDto in tasksDtos)
                {
                    if (!IsValid(taskDto))
                    {
                        output.AppendLine(ErrorMessage);
                        continue;
                    }

                    var task = mapper.Map<Task>(taskDto);

                    if (task.OpenDate < project.OpenDate || (project.DueDate.HasValue && task.DueDate > project.DueDate))
                    {
                        output.AppendLine(ErrorMessage);
                        continue;
                    }

                    task.ProjectId = project.Id;
                    tasks.Add(task);
                }

                output.AppendLine(string.Format(SuccessfullyImportedProject, project.Name, tasks.Count));
                context.Tasks.AddRange(tasks);
                context.SaveChanges();
            }

            return output.ToString();
        }

        public static string ImportEmployees(TeisterMaskContext context, string jsonString)
        {
            var output = new StringBuilder();

            var employeeDtos = JsonConvert.DeserializeObject<EmployeeDto[]>(jsonString);
            var mapper = GetMapper();

            foreach (var employeeDto in employeeDtos)
            {
                if (!IsValid(employeeDto))
                {
                    output.AppendLine(ErrorMessage);
                    continue;
                }

                var employee = mapper.Map<Employee>(employeeDto);

                context.Employees.Add(employee);
                context.SaveChanges();

                var tasks = employeeDto.Tasks.Distinct();
                var employeeTasks = new List<EmployeeTask>();


                foreach (var task in tasks)
                {
                    if (!context.Tasks.Any(t => t.Id == task))
                    {
                        output.AppendLine(ErrorMessage);
                        continue;
                    }

                    employeeTasks.Add(new EmployeeTask()
                    {
                        EmployeeId = employee.Id,
                        TaskId = task
                    });
                }

                context.EmployeesTasks.AddRange(employeeTasks);
                context.SaveChanges();

                output.AppendLine(string.Format(SuccessfullyImportedEmployee, employee.Username, employeeTasks.Count));
            }

            return output.ToString();
        }

        private static IEnumerable<TDto> DeserializeXml<TDto>(string rootAttribute, string inputXml)
        {
            var serializer = new XmlSerializer(typeof(TDto[]),
                new XmlRootAttribute(rootAttribute));

            var userDtos = (TDto[])serializer.Deserialize(new StringReader(inputXml));
            return userDtos;
        }

        private static IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<TeisterMaskProfile>();
            });

            return config.CreateMapper();
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}