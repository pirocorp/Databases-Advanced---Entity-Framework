namespace TeisterMask.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

    using Data;
    using Data.Models;
    using Data.Models.Enums;
    using ImportDtos;
    using Newtonsoft.Json;

    public class Deserializer
    {
        private const string DATE_TIME_FORMAT = "dd/MM/yyyy";

        private const string ERROR_MESSAGE = "Invalid data!";

        private const string SUCCESSFULLY_IMPORTED_PROJECT
            = "Successfully imported project - {0} with {1} tasks.";

        private const string SUCCESSFULLY_IMPORTED_EMPLOYEE
            = "Successfully imported employee - {0} with {1} tasks.";

        public static string ImportProjects(TeisterMaskContext context, string xmlString)
        {
            var serializer = new XmlSerializer(typeof(ProjectImportDto[]), new XmlRootAttribute("Projects"));
            var projectImportDto = (ProjectImportDto[])serializer.Deserialize(new StringReader(xmlString));

            var projectEntities = new List<Project>();
            var sb = new StringBuilder();

            foreach (var dto in projectImportDto)
            {
                var isOpenDateValid = DateTime.TryParseExact(dto.OpenDate, DATE_TIME_FORMAT,
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out var openDate);

                var isDueDateValid = true;
                DateTime? dueDate = null;

                if (!string.IsNullOrEmpty(dto.DueDate))
                {
                    isDueDateValid = DateTime.TryParseExact(dto.DueDate, DATE_TIME_FORMAT,
                        CultureInfo.InvariantCulture, DateTimeStyles.None, out var dueDateExact);

                    dueDate = dueDateExact;
                }

                var valid = IsValid(dto)
                            && isOpenDateValid
                            && isDueDateValid;

                if (valid)
                {
                    var project = new Project()
                    {
                        Name = dto.Name,
                        OpenDate = openDate,
                        DueDate = dueDate,
                        Tasks = GetTasks(sb, openDate, dueDate, dto),
                    };

                    projectEntities.Add(project);
                    sb.AppendLine(string.Format(SUCCESSFULLY_IMPORTED_PROJECT, project.Name, project.Tasks.Count));
                }
                else
                {
                    sb.AppendLine(ERROR_MESSAGE);
                }
            }

            context.Projects.AddRange(projectEntities);
            context.SaveChanges();
            return sb.ToString().Trim();
        }

        public static string ImportEmployees(TeisterMaskContext context, string jsonString)
        {
            var employeeImportDtos = JsonConvert.DeserializeObject<EmployeeImportDto[]>(jsonString);

            var employeeEntities = new HashSet<Employee>();
            var sb = new StringBuilder();

            foreach (var dto in employeeImportDtos)
            {
                if (IsValid(dto))
                {
                    var employee = new Employee()
                    {
                        Username = dto.Username,
                        Email = dto.Email,
                        Phone = dto.Phone,
                        EmployeesTasks = GetEmployeesTasks(context, sb, dto),
                    };

                    employeeEntities.Add(employee);
                    sb.AppendLine(string.Format(SUCCESSFULLY_IMPORTED_EMPLOYEE, 
                        employee.Username, employee.EmployeesTasks.Count));
                }
                else
                {
                    sb.AppendLine(ERROR_MESSAGE);
                }
            }

            context.Employees.AddRange(employeeEntities);
            context.SaveChanges();
            return sb.ToString().Trim();
        }
        
        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }

        private static HashSet<Task> GetTasks(StringBuilder sb, DateTime projectOpenDate, DateTime? projectDueDate, ProjectImportDto projectDto)
        {
            var tasks = new HashSet<Task>();

            foreach (var dto in projectDto.Tasks)
            {
                var isOpenDateValid = DateTime.TryParseExact(dto.OpenDate, DATE_TIME_FORMAT,
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out var taskOpenDate);

                var isDueDateValid = DateTime.TryParseExact(dto.DueDate, DATE_TIME_FORMAT,
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out var taskDueDate);

                var isExecutionTypeValid = Enum.IsDefined(typeof(ExecutionType), dto.ExecutionType);

                var isLabelTypeValid = Enum.IsDefined(typeof(LabelType), dto.LabelType);

                var areDatesInChronologicalOrder = false;

                var isTaskOpenDateAfterProjectStart = false;

                var isTaskDueDateBeforeProjectDueDate = false;

                if (isOpenDateValid && isDueDateValid)
                {
                    if (taskOpenDate < taskDueDate)
                    {
                        areDatesInChronologicalOrder = true;
                    }

                    if (taskOpenDate >= projectOpenDate)
                    {
                        isTaskOpenDateAfterProjectStart = true;
                    }

                    if (projectDueDate == null ||
                        taskDueDate <= projectDueDate)
                    {
                        isTaskDueDateBeforeProjectDueDate = true;
                    }
                }

                var valid = IsValid(dto)
                            && isOpenDateValid
                            && isDueDateValid
                            && isExecutionTypeValid
                            && isLabelTypeValid
                            //&& areDatesInChronologicalOrder
                            && isTaskOpenDateAfterProjectStart
                            && isTaskDueDateBeforeProjectDueDate;

                if (valid)
                {
                    var task = new Task()
                    {
                        Name = dto.Name,
                        OpenDate = taskOpenDate,
                        DueDate = taskDueDate,
                        ExecutionType = (ExecutionType)dto.ExecutionType,
                        LabelType = (LabelType)dto.LabelType
                    };

                    tasks.Add(task);
                }
                else
                {
                    sb.AppendLine(ERROR_MESSAGE);
                }
            }

            return tasks;
        }

        private static bool TaskExists(TeisterMaskContext context, int id)
        {
            return context.Tasks
                .Any(t => t.Id == id);
        }

        private static HashSet<EmployeeTask> GetEmployeesTasks(TeisterMaskContext context, StringBuilder sb, EmployeeImportDto dto)
        {
            var employeesTasks = new HashSet<EmployeeTask>();

            foreach (var task in dto.Tasks.Distinct())
            {
                if (!TaskExists(context, task))
                {
                    sb.AppendLine(ERROR_MESSAGE);
                    continue;
                }

                var employeeTask = new EmployeeTask()
                {
                    TaskId = task
                };

                employeesTasks.Add(employeeTask);
            }

            return employeesTasks;
        }
    }
}