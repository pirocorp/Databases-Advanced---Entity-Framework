namespace SoftUni.Tests.GetEmployeesFromResearchAndDevelopment
{
    using System;
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
                .AddDbContext<SoftUniContext>(b => b.UseInMemoryDatabase(Guid.NewGuid().ToString()));

            var serviceProvider = services.BuildServiceProvider();

            var context = serviceProvider.GetService<SoftUniContext>();

            Seed(context);

            var result = StartUp.GetEmployeesFromResearchAndDevelopment(context).Trim();

            var expected = AssertMethod(context).Trim();

            Assert.AreEqual(expected, result, "Returned value is incorrect!");
        }

        private void Seed(SoftUniContext context)
        {
            ICollection<Employee> employees = new List<Employee>();

            var firstNames = new[]
            {
            "Atanas", "Mariela", "Ani", "Slavi", "Pesho"
        };

            var lastNames = new[]
            {
            "Atanasow", "Cureva", "Topova", "Ludakov", "Peshev"
        };

            var middleNames = new[]
            {
            "Petrov", "Kirov", "Stoyanov", "Angelov", "Vladimirov"
        };

            var jobTitles = new[]
            {
            "Bartender", "Waiter", "Artist", "Shefa", "Driver"
        };

            var salaries = new[]
            {
            642.12m, 5235, 4000.32m, 25432, 2535235.33m
        };

            var departments = new[]
            {
            new Department
                {Name = "Research and Development"},
            new Department
                {Name = "Training"},
            new Department
                {Name = "None"}
        };

            for (var i = 0; i < 5; i++)
            {
                var employee = new Employee
                {
                    FirstName = firstNames[i],
                    LastName = lastNames[i],
                    MiddleName = middleNames[i],
                    JobTitle = jobTitles[i],
                    Salary = salaries[i],
                    Department = departments[i % departments.Length]
                };

                employees.Add(employee);
            }

            context.Employees.AddRange(employees);
            context.SaveChanges();
        }

        public static string AssertMethod(SoftUniContext context)
        {
            var content = new StringBuilder();

            var employees =
                context.Employees
                    .Where(employee => employee.Department.Name == "Research and Development")
                    .OrderBy(employee => employee.Salary)
                    .ThenByDescending(employee => employee.FirstName);

            foreach (var employee in employees)
            {
                content.AppendLine($"{employee.FirstName} {employee.LastName} " +
                                   $"from {employee.Department.Name} - ${employee.Salary:F2}");
            }

            return content.ToString().TrimEnd();
        }
    }
}