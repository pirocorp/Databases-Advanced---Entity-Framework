
namespace Softuni.Tests
{
    using System.Text;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using Data;
    using Models;

    [TestFixture]
    public class Test_003_002
    {
        private IServiceProvider serviceProvider;


        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<SoftUniContext>()
                .UseInMemoryDatabase(databaseName: "SoftUni")
                .Options;

            var services = new ServiceCollection()
                .AddDbContext<SoftUniContext>(b => b.UseInMemoryDatabase("SoftUni"));

            this.serviceProvider = services.BuildServiceProvider();
        }

        [Test]
        public void ValidateOutput()
        {
            var context = this.serviceProvider.GetService<SoftUniContext>();

            this.Seed(context);

            var assertService = this.serviceProvider.GetService<SoftUniContext>();

            string result = StartUp.GetEmployeesFullInformation(assertService).Trim();

            string expected = AssertMethod(assertService).Trim();

            Assert.AreEqual(expected, result, "Returned value is incorrect!");
        }

        private void Seed(SoftUniContext context)
        {
            ICollection<Employee> employees = new List<Employee>();

            var firstNames = new[] { "Gosho", "Ivan", "Kiro", "Pesho", "Kiko" };
            var lastNames = new[] { "Goshev", "Ivanov", "Kirev", "Peshov", "Kikov" };
            var middleNames = new[] { "Stoyanov", "Atanasov", "Petkov", "Angelov", "Vladimirov" };
            var jobTitles = new[] { "Developer", "Designer", "Scientist", "Shefa", "Dynerdzhiyata" };
            var salaries = new[] { 2110.12m, 200.21m, 4000.32m, 23241.12m, 145543253.33m };

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