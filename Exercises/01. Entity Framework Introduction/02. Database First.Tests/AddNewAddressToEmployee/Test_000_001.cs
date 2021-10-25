namespace SoftUni.Tests.AddNewAddressToEmployee
{
    using System.Linq;
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
                .AddDbContext<SoftUniContext>(b => b.UseInMemoryDatabase("SoftUni"));

            var serviceProvider = services.BuildServiceProvider();

            var context = serviceProvider.GetService<SoftUniContext>();

            this.Seed(context);

            StartUp.AddNewAddressToEmployee(context);

            string result = "Vitoshka 15";

            string expected = AssertMethod(context).Trim();

            Assert.AreEqual(expected, result, "Returned value is incorrect!");
        }

        private void Seed(SoftUniContext context)
        {
            var employee = new Employee
            {
                FirstName = "Svetlin",
                LastName = "Nakov",
                MiddleName = "TheGod",
                JobTitle = "InspirationManager",
                Salary = 0.00m,
            };

            context.Employees.Add(employee);
            context.SaveChanges();
        }

        public static string AssertMethod(SoftUniContext context)
        {
            return context.Employees.FirstOrDefault(x => x.LastName == "Nakov")
                .Address.AddressText;
        }
    }
}
