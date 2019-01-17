using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Softuni.Data;
using Softuni.Models;

// ReSharper disable InconsistentNaming
// ReSharper disable CheckNamespace

namespace Softuni.Tests
{
    [TestFixture]
    public class Test_012_002
    {
        [Test]
        public void ValidateOutput()
        {
            var services = new ServiceCollection()
                .AddDbContext<SoftUniContext>(b => b.UseInMemoryDatabase("SoftUni"));

            var serviceProvider = services.BuildServiceProvider();

            var context = serviceProvider.GetService<SoftUniContext>();

            this.Seed(context);

            string expected = AssertMethod(context).Trim();

            string result = StartUp.IncreaseSalaries(context).Trim();

            Assert.AreEqual(expected, result, "Returned value is incorrect!");
        }

        private void Seed(SoftUniContext context)
        {
            var departments = new List<Department>();

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
                642.1412342m, 5235, 4000.32m, 25432, 2535235.33m,
                52354.14322m, 956, 658.32m, 95846, 75343215.33m,
            };

            var departmentNames = new[]
            {
                "Engineering","Tool Design","Marketing",
                "ChillDepartment"
            };

            foreach (var departmentName in departmentNames)
            {
                var department = new Department
                {
                    Name = departmentName
                };

                int counter = 0;

                for (int i = 0; i < 2; i++)
                {
                    var employee = new Employee
                    {
                        FirstName = firstNames[counter],
                        LastName = lastNames[counter],
                        MiddleName = middleNames[counter],
                        JobTitle = jobTitles[counter],
                        Salary = salaries[counter],
                    };

                    counter++;
                    department.Employees.Add(employee);
                }

                departments.Add(department);
            }

            context.Departments.AddRange(departments);
            context.SaveChanges();
        }

        public static string AssertMethod(SoftUniContext context)
        {
            var content = new StringBuilder();

            var employees = context.Employees.Where(employee =>
                employee.Department.Name == "Engineering"
                || employee.Department.Name == "Tool Design"
                || employee.Department.Name == "Marketing"
                || employee.Department.Name == "Information Services");

            foreach (var employee in employees)
            {
                content.AppendLine($"{employee.FirstName} {employee.LastName} (${employee.Salary * 1.12m:F2})");
            }

            return content.ToString().TrimEnd();
        }
    }
}