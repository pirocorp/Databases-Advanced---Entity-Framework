namespace Softuni
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using Data;
    using Models;

    public class StartUp
    {
        public static void Main()
        {
            var result = RemoveTown(new SoftUniContext());
            Console.WriteLine(result);
        }

        public static string RemoveTown(SoftUniContext db)
        {
            var town = db.Towns.FirstOrDefault(t => t.Name == "Seattle");

            var addressesInTown = db.Addresses
                .Where(a => a.TownId == town.TownId)
                .ToList();

            var addressIdsInTown = addressesInTown.Select(a => a.AddressId).ToList();

            var employeesWithGivenAddresses = db.Employees
                .Where(e => e.Address != null && addressIdsInTown.Contains((int) e.AddressId))
                .ToList();

            foreach (var employee in employeesWithGivenAddresses)
            {
                employee.AddressId = null;
            }

            db.Addresses.RemoveRange(addressesInTown);
            db.Towns.Remove(town);
            db.SaveChanges();

            GC.Collect();
            GC.WaitForPendingFinalizers();

            return $"{addressIdsInTown.Count} address in {town.Name} was deleted";
        }

        public static string DeleteProjectById(SoftUniContext db)
        {
            var project = db.Projects.Find(2);

            var employeesProjects = db.EmployeesProjects
                .Where(ep => ep.ProjectId == 2)
                .ToList();

            db.EmployeesProjects.RemoveRange(employeesProjects);

            db.Projects.Remove(project);
            db.SaveChanges();

            var projects = db.Projects.Take(10).Select(p => p.Name).ToList();
            return string.Join(Environment.NewLine, projects);
        }

        public static string GetEmployeesByFirstNameStartingWithSa(SoftUniContext db)
        {
            var employees = db.Employees
                .Where(e => e.FirstName.StartsWith("Sa"))
                .OrderBy(e => e.FirstName)
                .ThenBy(e => e.LastName)
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    e.JobTitle,
                    e.Salary
                })
                .ToList();

            var result = new StringBuilder();

            foreach (var employee in employees)
            {
                result.AppendLine($"{employee.FirstName} {employee.LastName} - {employee.JobTitle} - (${employee.Salary:F2})");
            }

            GC.Collect();
            GC.WaitForPendingFinalizers();

            return result.ToString().Trim();
        }

        public static string IncreaseSalaries(SoftUniContext db)
        {
            var employees = db.Employees
                .Where(e => e.Department.Name == "Engineering" ||
                            e.Department.Name == "Tool Design" ||
                            e.Department.Name == "Marketing" ||
                            e.Department.Name == "Information Services")
                .ToList();

            foreach (var employee in employees)
            {
                employee.Salary *= 1.12M;
            }

            db.SaveChanges();

            employees = db.Employees
                .Where(e => e.Department.Name == "Engineering" ||
                            e.Department.Name == "Tool Design" ||
                            e.Department.Name == "Marketing" ||
                            e.Department.Name == "Information Services")
                .ToList();

            var result = new StringBuilder();

            foreach (var employee in employees)
            {
                result.AppendLine($"{employee.FirstName} {employee.LastName} (${employee.Salary:F2})");
            }

            GC.Collect();
            GC.WaitForPendingFinalizers();

            return result.ToString().Trim();
        }

        public static string GetLatestProjects(SoftUniContext db)
        {
            var projects = db.Projects
                .OrderByDescending(p => p.StartDate)
                .Select(p => new
                {
                    p.Name,
                    p.Description,
                    p.StartDate
                })
                .Take(10)
                .ToList()
                .OrderBy(p => p.Name)
                .ToList();

            var result = new StringBuilder();

            foreach (var project in projects)
            {
                var format = "M/d/yyyy h:mm:ss tt";
                result.AppendLine($"{project.Name}");
                result.AppendLine($"{project.Description}");
                result.AppendLine($"{project.StartDate.ToString()}");
            }

            GC.Collect();
            GC.WaitForPendingFinalizers();

            return result.ToString().Trim();
        }

        public static string GetDepartmentsWithMoreThan5Employees(SoftUniContext db)
        {
            var departments = db.Departments
                .Where(d => d.Employees.Count > 5)
                .OrderBy(d => d.Employees.Count)
                .ThenBy(d => d.Name)
                .Select(d => new
                {
                    DepartmentName = d.Name,
                    ManagerFirstName = d.Manager.FirstName,
                    ManagerLastName = d.Manager.LastName,
                    Employees = d.Employees
                        .OrderBy(e => e.FirstName)
                        .ThenBy(e => e.LastName)
                        .ToList()
                })
                .ToList();


            var result = new StringBuilder();

            foreach (var department in departments)
            {
                result.AppendLine($"{department.DepartmentName} - {department.ManagerFirstName} {department.ManagerLastName}");

                foreach (var employee in department.Employees)
                {
                    result.AppendLine($"{employee.FirstName} {employee.LastName} - {employee.JobTitle}");
                }
            }

            GC.Collect();
            GC.WaitForPendingFinalizers();

            return result.ToString().Trim();
        }

        public static string GetEmployee147(SoftUniContext db)
        {
            var employee = db.Employees
                .Where(e => e.EmployeeId == 147)
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    e.JobTitle,
                    Projects = e.EmployeesProjects
                        .OrderBy(ep => ep.Project.Name)
                        .Select(ep => ep.Project)
                        .ToList()
                })
                .FirstOrDefault();

            var result = new StringBuilder();

            result.AppendLine($"{employee.FirstName} {employee.LastName} {employee.JobTitle}");
            result.AppendLine($"{string.Join(Environment.NewLine, employee.Projects.Select(p => p.Name))}");

            GC.Collect();
            GC.WaitForPendingFinalizers();

            return result.ToString().Trim();
        }

        public static string GetAddressesByTown(SoftUniContext db)
        {
            var addresses = db
                .Addresses
                .OrderByDescending(a => a.Employees.Count)
                .ThenBy(a => a.Town.Name)
                .ThenBy(a => a.AddressText)
                .Select(a => new
                {
                    a.AddressText,
                    TownName = a.Town.Name,
                    EmployeesCount = a.Employees.Count
                })
                .Take(10)
                .ToList();

            var result = new StringBuilder();

            foreach (var address in addresses)
            {
                result.AppendLine($"{address.AddressText}, {address.TownName} - {address.EmployeesCount} employees");
            }

            GC.Collect();
            GC.WaitForPendingFinalizers();

            return result.ToString().Trim();
        }

        public static string GetEmployeesInPeriod(SoftUniContext db)
        {
            var employees = db.Employees
                .Where(e => e.EmployeesProjects.Any(ep => ep.Project.StartDate.Year >= 2001 && 
                                                          ep.Project.StartDate.Year <= 2003))
                .Take(10)
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    ManagerFirstName = e.Manager.FirstName,
                    ManagerLastName = e.Manager.LastName,
                    Projects = e.EmployeesProjects.Select(ep => ep.Project).ToList(),
                })
                .ToList();

            var format = "M/d/yyyy h:mm:ss tt";
            var result = new StringBuilder();

            foreach (var employee in employees)
            {
                result.AppendLine($"{employee.FirstName} {employee.LastName} - Manager: {employee.ManagerFirstName} {employee.ManagerLastName}");

                foreach (var project in employee.Projects)
                {
                    var endDateResult = project?.EndDate == null
                        ? "not finished"
                        : project.EndDate?.ToString(format, CultureInfo.InvariantCulture);
                    result.AppendLine($"--{project.Name} - {project.StartDate.ToString(format, CultureInfo.InvariantCulture)} - {endDateResult}");
                }
            }

            GC.Collect();
            GC.WaitForPendingFinalizers();

            return result.ToString().Trim();
        }

        public static string AddNewAddressToEmployee(SoftUniContext db)
        {
            var employee = db.Employees
                .FirstOrDefault(e => e.LastName == "Nakov");

            var address = new Address()
            {
                AddressText = "Vitoshka 15",
                TownId = 4
            };

            employee.Address = address;

            db.SaveChanges();

            var addresses = db
                .Employees
                .OrderByDescending(e => e.Address.AddressId)
                .Take(10)
                .Select(e => e.Address.AddressText)
                .ToList();

            GC.Collect();
            GC.WaitForPendingFinalizers();

            return string.Join(Environment.NewLine, addresses);
        }

        public static string GetEmployeesWithSalaryOver50000(SoftUniContext db)
        {
            var employees = db.Employees
                .Where(e => e.Salary > 50000)
                .OrderBy(e => e.FirstName)
                .Select(e => new
                {
                    e.FirstName,
                    e.Salary
                })
                .ToList();

            var result = new StringBuilder();

            foreach (var employee in employees)
            {
                result.AppendLine($"{employee.FirstName} - {employee.Salary:F2}");
            }

            GC.Collect();
            GC.WaitForPendingFinalizers();

            return result.ToString().Trim();
        }

        public static string GetEmployeesFromResearchAndDevelopment(SoftUniContext db)
        {
            var employees = db.Employees
                .Where(e => e.Department.Name == "Research and Development")
                .OrderBy(e => e.Salary)
                .ThenByDescending(e => e.FirstName)
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    DepartmentName = e.Department.Name,
                    e.Salary
                })
                .ToList();

            var result = new StringBuilder();

            foreach (var employee in employees)
            {
                result.AppendLine($"{employee.FirstName} {employee.LastName} from {employee.DepartmentName} - ${employee.Salary:F2}");
            }

            GC.Collect();
            GC.WaitForPendingFinalizers();

            return result.ToString().Trim();
        }

        public static string GetEmployeesFullInformation(SoftUniContext db)
        {
            //using (db)
            {
                var employees = db.Employees
                    .OrderBy(e => e.EmployeeId)
                    .Select(e => new
                    {
                        e.FirstName,
                        e.LastName,
                        e.MiddleName,
                        e.JobTitle,
                        e.Salary
                    })
                    .ToList();

                var sb = new StringBuilder();

                foreach (var employee in employees)
                {
                    sb.AppendLine($"{employee.FirstName} {employee.LastName} {employee.MiddleName} {employee.JobTitle} {Math.Round(employee.Salary, 2)}");
                }

                return sb.ToString().Trim();
            }
        }
    }
}