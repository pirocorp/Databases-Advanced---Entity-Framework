namespace Softuni.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Models;
    using NUnit.Framework;

    [TestFixture]
    public class Test_003_001
    {
        private IServiceProvider serviceProvider;

        [SetUp]
        public void Setup()
        {
            //var options = new DbContextOptionsBuilder<SoftUniContext>()
            //    .UseInMemoryDatabase(databaseName: "SoftUni")
            //    .Options;

            var services = new ServiceCollection()
                .AddDbContext<SoftUniContext>(b => b.UseInMemoryDatabase("SoftUni"));

            this.serviceProvider = services.BuildServiceProvider();
        }

        [Test]
        public void ValidateOutput()
        {
            var context = this.serviceProvider.GetService<SoftUniContext>();

            this.Seed(context);

            string result = StartUp.GetEmployeesFullInformation(context).Trim();
            string expected = AssertMethod(context).Trim();

            Assert.AreEqual(expected, result, "Returned value is incorrect!");
        }

        private void Seed(SoftUniContext context)
        {
            ICollection<Employee> employees = new List<Employee>();

            var firstNames = new[] { "Atanas", "Mariela", "Ani", "Slavi", "Pesho" };
            var lastNames = new[] { "Atanasow", "Cureva", "Topova", "Ludakov", "Peshev" };
            var middleNames = new[] { "Petrov", "Kirov", "Stoyanov", "Angelov", "Vladimirov" };
            var jobTitles = new[] { "Bartender", "Waiter", "Artist", "Shefa", "Driver" };
            var salaries = new[] { 642.12m, 5235, 4000.32m, 25432, 2535235.33m };

            for (int i = 0; i < 5; i++)
            {
                var employee = new Employee
                {
                    FirstName = firstNames[i],
                    LastName = lastNames[i],
                    MiddleName = middleNames[i],
                    JobTitle = jobTitles[i],
                    Salary = salaries[i],
                };

                employees.Add(employee);
            }

            context.Employees.AddRange(employees);
            context.SaveChanges();
        }

        public static string AssertMethod(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var employee in context.Employees)
            {
                sb.AppendLine($"{employee.FirstName} {employee.LastName} {employee.MiddleName} {employee.JobTitle} {employee.Salary}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}