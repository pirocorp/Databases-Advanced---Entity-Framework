namespace SoftUni.Tests.GetEmployee147
{
    using System;
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

            this.Seed(context);

            string result = StartUp.GetEmployee147(context).Trim();

            string expected = AssertMethod(context).Trim();

            Assert.AreEqual(expected, result, "Returned value is incorrect!");
        }

        private void Seed(SoftUniContext context)
        {
            var employeeProjects = new EmployeeProject[5];

            var projects = new[]
            {
            new Project{Name = "UnitTestProject",StartDate = DateTime.Parse("01/01/2019")},
            new Project{Name = "DamnSonProject",StartDate = DateTime.Parse("01/01/2019")},
            new Project{Name = "SoMuchEffort",StartDate = DateTime.Parse("01/01/2019")},
            new Project{Name = "Thyme",StartDate = DateTime.Parse("01/01/2019")},
            new Project{Name = "WowWow",StartDate = DateTime.Parse("01/01/2019")},
        };

            var employee = new Employee
            {
                EmployeeId = 147,
                FirstName = "Nasko",
                LastName = "Naskov",
                JobTitle = "TheNumberBoy",
            };

            for (var i = 0; i < projects.Length; i++)
            {
                var employeeProject = new EmployeeProject
                {
                    Employee = employee,
                    Project = projects[i]
                };

                employeeProjects[i] = employeeProject;
            }

            context.Employees.Add(employee);
            context.Projects.AddRange(projects);
            context.EmployeesProjects.AddRange(employeeProjects);
            context.SaveChanges();
        }

        public static string AssertMethod(SoftUniContext context)
        {
            var content = new StringBuilder();

            var employee = context.Employees.Find(147);

            var projects = employee.EmployeesProjects.OrderBy(project => project.Project.Name);

            content.AppendLine($"{employee.FirstName} {employee.LastName} - {employee.JobTitle}");

            foreach (var project in projects)
            {
                content.AppendLine(project.Project.Name);
            }

            return content.ToString().TrimEnd();
        }
    }
}
