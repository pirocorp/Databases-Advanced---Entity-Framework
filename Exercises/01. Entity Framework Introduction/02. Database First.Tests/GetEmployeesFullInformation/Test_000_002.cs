namespace SoftUni.Tests.GetEmployeesFullInformation
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    using NUnit.Framework;

    using SoftUni;
    using SoftUni.Data;
    using SoftUni.Models;

    [TestFixture]
    public class Test_000_002
    {
        [Test]
        public void ValidateOutput()
        {
            var services = new ServiceCollection()
                    .AddDbContext<SoftUniContext>(b => b.UseInMemoryDatabase(Guid.NewGuid().ToString()));

            var serviceProvider = services.BuildServiceProvider();

            var context = serviceProvider.GetService<SoftUniContext>();

            Seed(context);

            var assertService = serviceProvider.GetService<SoftUniContext>();

            var result = StartUp.GetEmployeesFullInformation(assertService).Trim();

            var expected = AssertMethod(assertService).Trim();

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

            for (var i = 0; i < 5; i++)
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
            var sb = new StringBuilder();

            foreach (var employee in context.Employees)
            {
                sb.AppendLine(
                    $"{employee.FirstName} {employee.LastName} {employee.MiddleName} {employee.JobTitle} {employee.Salary}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}