namespace SoftUni
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Data;
    using Models;

    public static class StartUp
    {
        public static void Main()
        {
            var context = new SoftUniContext();
            var result = GetEmployeesFullInformation(context);

            Console.WriteLine(result);
        }

        public static string GetEmployeesFullInformation(SoftUniContext context)
            => context.Employees
                .OrderBy(e => e.EmployeeId)
                .Select(e => $"{e.FirstName} {e.LastName} {e.MiddleName} {e.JobTitle} {e.Salary:F2}")
                .ToList()
                .FormatOutput();

        public static string GetEmployeesWithSalaryOver50000(SoftUniContext context)
            => context.Employees
                .Where(e => e.Salary > 50_000)
                .OrderBy(e => e.FirstName)
                .Select(e => $"{e.FirstName} - {e.Salary:F2}")
                .ToList()
                .FormatOutput();

        public static string GetEmployeesFromResearchAndDevelopment(SoftUniContext context)
            => context.Employees
                .Where(e => e.Department.Name == "Research and Development")
                .OrderBy(e => e.Salary)
                .ThenByDescending(e => e.FirstName)
                .Select(e => $"{e.FirstName} {e.LastName} from {e.Department.Name} - ${e.Salary:F2}")
                .FormatOutput();

        public static string AddNewAddressToEmployee(SoftUniContext context)
        {
            var address = new Address()
            {
                AddressText = "Vitoshka 15",
                TownId = 4,
            };

            var nakov = context.Employees.First(e => e.LastName == "Nakov");
            nakov.Address = address;
            context.SaveChanges();

            return context.Employees
                .OrderByDescending(e => e.AddressId)
                .Select(e => e.Address.AddressText)
                .Take(10)
                .ToList()
                .FormatOutput();
        }

        public static string GetEmployeesInPeriod(SoftUniContext context)
            => context.Employees
                .Where(e => e.EmployeesProjects.Any(p =>
                    p.Project.StartDate.Year >= 2001 && p.Project.StartDate.Year <= 2003))
                .Take(10)
                .Select(e => new
                {
                    Employee = $"{e.FirstName} {e.LastName} - Manager: {e.Manager.FirstName} {e.Manager.LastName}",
                    Projects = e.EmployeesProjects
                        .Select(p => $"--{p.Project.Name} - {p.Project.StartDate:M/d/yyyy h:mm:ss tt} - {(p.Project.EndDate == null ? "not finished" : p.Project.EndDate.Value.ToString("M/d/yyyy h:mm:ss tt"))}")
                })
                .Select(x => $"{x.Employee}{Environment.NewLine}{x.Projects.ToList().FormatOutput()}")
                .ToList()
                .FormatOutput();

        public static string GetAddressesByTown(SoftUniContext context)
            => context.Addresses
                .OrderByDescending(a => a.Employees.Count)
                .ThenBy(a => a.Town.Name)
                .ThenBy(a => a.AddressText)
                .Take(10)
                .Select(a => $"{a.AddressText}, {a.Town.Name} - {a.Employees.Count} employees")
                .FormatOutput();

        public static string GetEmployee147(SoftUniContext context)
            => context.Employees
                .Where(e => e.EmployeeId == 147)
                .Select(e => new
                {
                    Employee = $"{e.FirstName} {e.LastName} - {e.JobTitle}",
                    Projects = e.EmployeesProjects
                        .OrderBy(p => p.Project.Name)
                        .Select(p => p.Project.Name)
                })
                .Select(x => $"{x.Employee}{Environment.NewLine}{x.Projects.FormatOutput()}")
                .FormatOutput();

        public static string GetDepartmentsWithMoreThan5Employees(SoftUniContext context)
            => context.Departments
                .Where(d => d.Employees.Count > 5)
                .OrderBy(d => d.Employees.Count)
                .ThenBy(d => d.Name)
                .Select(d => new
                {
                    Department = $"{d.Name} - {d.Manager.FirstName} {d.Manager.LastName}",
                    Employees = d.Employees
                        .OrderBy(e => e.FirstName)
                        .ThenBy(e => e.LastName)
                        .Select(e => $"{e.FirstName} {e.LastName} - {e.JobTitle}"),
                })
                .Select(x => $"{x.Department}{Environment.NewLine}{x.Employees.FormatOutput()}")
                .ToList()
                .FormatOutput();

        public static string GetLatestProjects(SoftUniContext context)
            => context.Projects
                .OrderByDescending(p => p.StartDate)
                .Take(10)
                .OrderBy(p => p.Name)
                .Select(p => $"{p.Name}{Environment.NewLine}{p.Description}{Environment.NewLine}{p.StartDate:M/d/yyyy h:mm:ss tt}")
                .FormatOutput();

        public static string IncreaseSalaries(SoftUniContext context)
        {
            var validDepartments = new List<string> { "Engineering", "Tool Design", "Marketing", "Information Services" };

            var employees = context.Employees
                .Where(e => validDepartments.Contains(e.Department.Name))
                .ToList();

            employees.ForEach(e => e.Salary += e.Salary * 0.12M);
            context.SaveChanges();

            return employees
                .OrderBy(e => e.FirstName)
                .ThenBy(e => e.LastName)
                .Select(e => $"{e.FirstName} {e.LastName} (${e.Salary:F2})")
                .FormatOutput();
        }

        public static string GetEmployeesByFirstNameStartingWithSa(SoftUniContext context)
            => context.Employees
                .Where(e => e.FirstName.StartsWith("Sa"))
                .OrderBy(e => e.FirstName)
                .ThenBy(e => e.LastName)
                .Select(e => $"{e.FirstName} {e.LastName} - {e.JobTitle} - (${e.Salary:F2})")
                .FormatOutput();

        public static string DeleteProjectById(SoftUniContext context)
        {
            var project = context.Projects.Find(2);
            
            var employeeProjects = context.EmployeesProjects
                .Where(ep => ep.ProjectId == 2)
                .ToList();

            context.RemoveRange(employeeProjects);
            context.Remove(project);

            context.SaveChanges();

            return context.Projects
                .Take(10)
                .Select(p => p.Name)
                .FormatOutput();
        }

        public static string RemoveTown(SoftUniContext context)
        {
            var seattle = context.Towns.First(t => t.Name == "Seattle");
            var addresses = context.Addresses
                .Where(a => a.TownId == seattle.TownId)
                .ToList();

            var employees = context.Employees
                .Where(e => addresses.Select(a => a.AddressId).ToList().Contains(e.AddressId ?? -1))
                .ToList();

            employees.ForEach(e => e.AddressId = null);

            context.RemoveRange(addresses);
            context.Remove(seattle);
            context.SaveChanges();

            return $"{addresses.Count} addresses in Seattle were deleted";
        }

        private static string FormatOutput(this IEnumerable<string> output)
            => string.Join(Environment.NewLine, output);
    }
}
