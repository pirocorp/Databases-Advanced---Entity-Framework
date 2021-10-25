namespace SoftUni.Tests.GetDepartmentsWithMoreThan5Employees
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    using NUnit.Framework;

    using SoftUni;
    using SoftUni.Data;
    using SoftUni.Models;

    [TestFixture]
    public class Test_000_001
    {
        [Test]
        public void ValidateOutput()
        {
            var services = new ServiceCollection()
                .AddDbContext<SoftUniContext>(b => b.UseInMemoryDatabase("SoftUni"));

            var serviceProvider = services.BuildServiceProvider();

            var context = serviceProvider.GetService<SoftUniContext>();

            this.Seed(context);

            string result = StartUp.GetDepartmentsWithMoreThan5Employees(context).Trim();

            string expected = AssertMethod(context).Trim();

            Assert.AreEqual(expected, result, "Returned value is incorrect!");
        }

        private void Seed(SoftUniContext context)
        {
            List<Employee> employees = new List<Employee>();
            List<Department> departments = new List<Department>();

            var firstNames = new[]
            {
            "Svetlin", "Mariela", "Ani", "Slavi", "Pesho",
            "Kircata", "Gosheto", "OnziTam", "DamnGod", "Test123",
        };

            var lastNames = new[]
            {
            "Nakov", "Cureva", "Topova", "Ludakov", "Peshev",
            "Wakov", "Sliperov", "Topalov", "Hristov", "Petrov"
        };

            var middleNames = new[]
            {
            "Petrov", "Kirov", "Stoyanov", "Angelov", "Vladimirov",
            "Karlov", "Markov", "Kostov", "Pavlov", "Georgiev"
        };

            var jobTitles = new[]
            {
            "Bartender", "Waiter", "Artist", "Shefa", "Driver",
            "IT", "DevOps", "DataScientist", "BusDriver", "Trainer",
        };

            var salaries = new[]
            {
            642.12m, 5235, 4000.32m, 25432, 2535235.33m,
            52354.12m, 956, 658.32m, 95846, 75343215.33m,
        };

            for (int i = 0; i < 20; i++)
            {
                var employee = new Employee
                {
                    FirstName = firstNames[i % firstNames.Length] + i,
                    LastName = lastNames[i % lastNames.Length] + i,
                    MiddleName = middleNames[i % middleNames.Length] + i,
                    JobTitle = jobTitles[i % jobTitles.Length] + i,
                    Salary = salaries[i % salaries.Length] + i,
                };

                employees.Add(employee);
            }

            for (int i = 0; i < employees.Count - 1; i++)
            {
                employees[i].Manager = employees[i + 1];
            }

            employees[employees.Count - 1].Manager = employees[0];

            var department = new Department
            {
                Name = "TrainingTeam",
                Manager = employees[8],
            };

            var secondDepartment = new Department
            {
                Name = "TrainingTeam2",
                Manager = employees[9],
            };

            for (int j = 0; j < 5; j++)
            {
                department.Employees.Add(employees[j]);
            }

            for (int j = 5; j < 20; j++)
            {
                secondDepartment.Employees.Add(employees[j]);
            }

            departments.Add(department);

            context.Employees.AddRange(employees);
            context.Departments.AddRange(departments);
            context.SaveChanges();
        }

        public static string AssertMethod(SoftUniContext context)
        {
            StringBuilder content = new StringBuilder();

            var departments = context
                .Departments.Where(department => department.Employees.Count > 5)
                .Include(x => x.Manager)
                .Include(x => x.Employees)
                .OrderBy(department => department.Employees.Count)
                .ThenBy(x => x.Name)
                .ToArray();

            foreach (var department in departments)
            {
                content.AppendLine($"{department.Name} - {department.Manager.FirstName} {department.Manager.LastName}");

                var employees = department.Employees.OrderBy(x => x.FirstName).ThenBy(x => x.LastName);

                foreach (var employee in employees)
                {
                    content.AppendLine($"{employee.FirstName} {employee.LastName} - {employee.JobTitle}");
                }
            }

            return content.ToString().TrimEnd();
        }
    }
}
