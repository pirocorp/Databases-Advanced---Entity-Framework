namespace SoftUni.Tests.GetAddressesByTown
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
    public class Test_002
    {
        [Test]
        public void ValidateOutput()
        {
            var services = new ServiceCollection()
                .AddDbContext<SoftUniContext>(b => b.UseInMemoryDatabase(Guid.NewGuid().ToString()));

            var serviceProvider = services.BuildServiceProvider();

            var context = serviceProvider.GetService<SoftUniContext>();

            this.Seed(context);

            var assertService = serviceProvider.GetService<SoftUniContext>();

            string result = StartUp.GetAddressesByTown(assertService).Trim();

            string expected = AssertMethod(assertService).Trim();

            Assert.AreEqual(expected, result, "Returned value is incorrect!");
        }


        private void Seed(SoftUniContext context)
        {
            List<Employee> employees = new List<Employee>();
            List<Address> addresses = new List<Address>();
            List<Town> towns = new List<Town>();

            var firstNames = new[]
            {
            "Svetlin", "Mariela", "Ani", "Slavi", "Pesho",
            "Kircata", "Gosheto", "OnziTam", "DamnGod", "Test123", "NonCoverName"
        };

            var lastNames = new[]
            {
            "Nakov", "Cureva", "Topova", "Ludakov", "Peshev",
            "Wakov", "Sliperov", "Topalov", "Hristov", "Petrov","NonCoverName"
        };

            var middleNames = new[]
            {
            "Petrov", "Kirov", "Stoyanov", "Angelov", "Vladimirov",
            "Karlov", "Markov", "Kostov", "Pavlov", "Georgiev","NonCoverName"
        };

            var jobTitles = new[]
            {
            "Bartender", "Waiter", "Artist", "Shefa", "Driver",
            "IT", "DevOps", "DataScientist", "BusDriver", "Trainer","NonCoverName"
        };

            var salaries = new[]
            {
            642.12m, 5235, 4000.32m, 25432, 2535235.33m,
            52354.12m, 956, 658.32m, 95846, 75343215.33m, 12312.3m
        };

            var addressesText = new[]
            {
            "Vitoshka 7889", "Tintyava 3", "G.M Dimitor 5", "Serdika 32", "Stara Planina 15",
            "Atansov 12", "Glavnata 115", "NoStreet 15", "Migos 125", "Kocho 315","NonCoverName"
        };

            var townNames = new string[]
            {
            "Plovdiv", "Sofia", "Varna", "SunnyBeach", "Pernik",
            "Yambol", "StaraZagora", "Montana", "Ruse", "Karnobat","NonCoverName"
            };

            for (var i = 0; i < townNames.Length; i++)
            {
                var town = new Town
                {
                    TownId = i + 1,
                    Name = townNames[i]
                };

                towns.Add(town);
            }

            for (int i = 0; i < 10; i++)
            {
                var address = new Address
                {
                    AddressText = addressesText[i],
                    Town = towns[i]
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

            employees[1].Address = addresses[0];
            employees[2].Address = addresses[0];

            context.Employees.AddRange(employees);
            context.SaveChanges();
        }

        public static string AssertMethod(SoftUniContext context)
        {
            StringBuilder content = new StringBuilder();

            var addresses =
                context.Addresses
                    .Include(x => x.Employees)
                    .Include(x => x.Town)
                    .OrderByDescending(address => address.Employees.Count)
                    .ThenBy(address => address.Town.Name)
                    .Take(10)
                    .ToList();

            foreach (var address in addresses)
            {
                content.AppendLine($"{address.AddressText}, {address.Town.Name} - {address.Employees.Count} employees");
            }

            return content.ToString().TrimEnd();
        }
    }
}
