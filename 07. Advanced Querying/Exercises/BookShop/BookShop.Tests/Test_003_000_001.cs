using System;
using System.Linq;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using NUnit.Framework;

using BookShop;
using BookShop.Data;
using BookShop.Models;
using BookShop.Initializer;

[TestFixture]
public class Test_003_000_001
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

        serviceProvider = services.BuildServiceProvider();
    }

    [Test]
    public void ValidateOutput()
    {
        var service = this.serviceProvider.GetService<BookShopContext>();
        DbInitializer.Seed(service);

        var assertService = this.serviceProvider.GetService<BookShopContext>();

        var result = StartUp.GetBooksByPrice(assertService).Trim();

        Assert.AreEqual(1258, result.Length, "Returned value is incorrect!");
    }
}