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
    public class Test_009_002
    {
        [Test]
        public void ValidateOutput()
        {
            var services = new ServiceCollection()
                .AddDbContext<SoftUniContext>(b => b.UseInMemoryDatabase("SoftUni"));

            var serviceProvider = services.BuildServiceProvider();

            var context = serviceProvider.GetService<SoftUniContext>();

            this.Seed(context);

            string result = StartUp.GetEmployee147(context).Trim();

            string expected = AssertMethod(context).Trim();

            Assert.AreEqual(expected, result, "Returned value is incorrect!");
        }

        private void Seed(SoftUniContext context)
        {
            var employee = new Employee
            {
                EmployeeId = 147,
                FirstName = "Nasko",
                LastName = "Naskov",
                JobTitle = "TheNumberBoy",
            };

            context.Employees.Add(employee);
            context.SaveChanges();
        }

        public static string AssertMethod(SoftUniContext context)
        {
            var content = new StringBuilder();

            var employee = context.Employees.Find(147);

            var projects = employee.EmployeesProjects.OrderBy(project => project.Project.Name);

            content.AppendLine($"{employee.FirstName} {employee.LastName} {employee.JobTitle}");

            foreach (var project in projects)
            {
                content.AppendLine(project.Project.Name);
            }

            return content.ToString().TrimEnd();
        }
    }
}