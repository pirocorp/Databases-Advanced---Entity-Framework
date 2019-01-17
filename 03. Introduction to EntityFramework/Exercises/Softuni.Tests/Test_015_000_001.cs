using System.Collections.Generic;
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
    public class Test_015_000_001
    {
        [Test]
        public void ValidateOutput()
        {
            var services = new ServiceCollection()
                .AddDbContext<SoftUniContext>(b => b.UseInMemoryDatabase("SoftUni"));

            var serviceProvider = services.BuildServiceProvider();

            var context = serviceProvider.GetService<SoftUniContext>();

            this.Seed(context);

            string result = StartUp.RemoveTown(context).Trim();

            string expected = "4 address in Seattle was deleted";

            Assert.AreEqual(expected, result, "Returned value is incorrect!");
        }

        private void Seed(SoftUniContext context)
        {
            List<Town> towns = new List<Town>
            {
                new Town{Name = "Seattle"},
                new Town{Name = "Seattle123"},
                new Town{Name = "Seattle1234"},
            };

            List<Address> addresses = new List<Address>
            {
                new Address{AddressText = "TukTam 123", Town = towns[0]},
                new Address{AddressText = "Sofia 12", Town = towns[0]},
                new Address{AddressText = "Vitoshka 321", Town = towns[0]},
                new Address{AddressText = "Stoletov 1", Town = towns[0]},
                new Address{AddressText = "123", Town = towns[1]},
                new Address{AddressText = "Ivan Vazov", Town = towns[2]},
                new Address{AddressText = "Petur Petrov", Town = towns[1]},
                new Address{AddressText = "Damn Damn", Town = towns[1]},
            };

            List<Employee> employees = new List<Employee>
            {
                new Employee {FirstName = "Gosho", Address = addresses[0]},
                new Employee {FirstName = "Pesho", Address = addresses[0]},
                new Employee {FirstName = "Gosho1", Address = addresses[1]},
                new Employee {FirstName = "Gosho12", Address = addresses[2]},
                new Employee {FirstName = "Gosho21", Address = addresses[0]},
                new Employee {FirstName = "Gosho123", Address = addresses[1]},
                new Employee {FirstName = "Gosho321", Address = addresses[4]},
                new Employee {FirstName = "Gosho213", Address = addresses[5]},
                new Employee {FirstName = "Gosho1234", Address = addresses[6]},
                new Employee {FirstName = "Gosho4321", Address = addresses[1]},
                new Employee {FirstName = "Gosho3123", Address = addresses[2]},
            };

            context.Towns.AddRange(towns);
            context.Addresses.AddRange(addresses);
            context.Employees.AddRange(employees);
            context.SaveChanges();
        }
    }
}