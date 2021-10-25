namespace SoftUni.Tests.AddNewAddressToEmployee
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

    // ReSharper disable InconsistentNaming
    // ReSharper disable CheckNamespace

    [TestFixture]
    public class Test_001
    {
        [Test]
        public void ValidateOutput()
        {
            var services = new ServiceCollection()
                .AddDbContext<SoftUniContext>(b => b.UseInMemoryDatabase("SoftUni"));

            var serviceProvider = services.BuildServiceProvider();

            var context = serviceProvider.GetService<SoftUniContext>();

            this.Seed(context);

            var assertService = serviceProvider.GetService<SoftUniContext>();

            string result = StartUp.AddNewAddressToEmployee(assertService).Trim();

            string expected = AssertMethod(assertService).Trim();

            Assert.AreEqual(expected, result, "Returned value is incorrect!");
        }

        private void Seed(SoftUniContext context)
        {
            ICollection<Employee> employees = new List<Employee>();

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

            var addressesText = new[]
            {
            "Vitoshka 7889","Tintyava 3","G.M Dimitor 5","Serdika 32","Stara Planina 15",
            "Atansov 12","Glavnata 115","NoStreet 15","Migos 125","Kocho 315",
        };

            var townIds = new[]
            {
            4, 1, 2, 3, 4, 5, 1, 2 ,3 , 4
        };

            List<Address> addresses = new List<Address>();

            for (int i = 0; i < 10; i++)
            {
                var address = new Address
                {
                    AddressText = addressesText[i],
                    TownId = townIds[i]
                };

                addresses.Add(address);
            }

            for (int i = 0; i < 10; i++)
            {
                var employee = new Employee
                {
                    FirstName = firstNames[i],
                    LastName = lastNames[i],
                    MiddleName = middleNames[i],
                    JobTitle = jobTitles[i],
                    Salary = salaries[i],
                    Address = addresses[i]
                };

                employees.Add(employee);
            }

            context.Employees.AddRange(employees);
            context.SaveChanges();
        }

        public static string AssertMethod(SoftUniContext context)
        {
            var content = new StringBuilder();

            var address = new Address
            {
                AddressText = "Vitoshka 15",
                TownId = 4
            };

            var nakovEmployee = context.Employees
                .First(employee => employee.LastName == "Nakov");

            nakovEmployee.Address = address;

            context.SaveChanges();

            var employeeAddresses = context.Employees
                .OrderByDescending(employee => employee.Address.AddressId)
                .Take(10)
                .Select(employee => employee.Address.AddressText);

            foreach (string employeeAddress in employeeAddresses)
            {
                content.AppendLine(employeeAddress);
            }

            return content.ToString().TrimEnd();
        }
    }
}
