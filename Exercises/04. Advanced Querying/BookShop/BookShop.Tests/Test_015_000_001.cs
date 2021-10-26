using System;
using System.Linq;
using BookShop.Data;
using BookShop.Initializer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace BookShop.Tests
{
    [TestFixture]
    public class Test_015_000_001
    {
        private IServiceProvider serviceProvider;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<BookShopContext>()
                .UseInMemoryDatabase(databaseName: "BookShop")
                .Options;

            var services = new ServiceCollection()
                .AddDbContext<BookShopContext>(b => b.UseInMemoryDatabase("BookShop"));

            this.serviceProvider = services.BuildServiceProvider();
        }

        [Test]
        public void ValidateOutput()
        {
            var service = this.serviceProvider.GetService<BookShopContext>();

            DbInitializer.Seed(service);

            var booksDeleted = StartUp.RemoveBooks(service);

            var assertService = this.serviceProvider.GetService<BookShopContext>();

            var bookCount = assertService.Books.Count();

            Assert.AreEqual(34, booksDeleted, "Incorrect amount of books deleted!");

            Assert.AreEqual(156, bookCount, "Incorrect amount of books left in the database");
        }
    }
}