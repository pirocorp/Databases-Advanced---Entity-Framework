namespace ProductShop.Tests.ImportCategories
{
    //ReSharper disable InconsistentNaming, CheckNamespace

    using System;
    using System.Linq;
    using System.Reflection;

    using AutoMapper;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    using NUnit.Framework;

    using ProductShop;
    using ProductShop.Data;

    [TestFixture]
    public class Test_000_001
    {
        private IServiceProvider serviceProvider;

        private static readonly Assembly CurrentAssembly = typeof(StartUp).Assembly;

        [SetUp]
        public void Setup()
        {
            Mapper.Reset();
            Mapper.Initialize(cfg => cfg.AddProfile(GetType("ProductShopProfile")));

            this.serviceProvider = ConfigureServices<ProductShopContext>("ProductShop");
        }

        [Test]
        public void ImportCategoriesZeroTests()
        {
            var context = this.serviceProvider.GetService<ProductShopContext>();

            var inputJson =
                @"[{""name"":""Drugs""},
               {""name"":""Adult""},
               {""name"":""Electronics""},
               {""name"":""Garden""},
               {""name"":""Weapons""},
               {""name"":""For Children""},
               {""name"":""Sports""},
               {""name"":""Fashion""},
               {""name"":""Autoparts""},
               {""name"":""Business""},
               {""name"":""Other""},
               {""name"": null}]";


            var actualOutput =
                StartUp.ImportCategories(context, inputJson);

            var expectedOutput = $"Successfully imported 11";

            var assertContext = this.serviceProvider.GetService<ProductShopContext>();

            const int expectedCategoriesCount = 11;
            var actualGameCount = assertContext.Categories.Count();

            Assert.That(actualGameCount, Is.EqualTo(expectedCategoriesCount),
                $"Inserted {nameof(context.Categories)} count is incorrect!");

            Assert.That(actualOutput, Is.EqualTo(expectedOutput).NoClip,
                $"{nameof(StartUp.ImportCategories)} output is incorrect!");
        }

        private static Type GetType(string modelName)
        {
            var modelType = CurrentAssembly
                .GetTypes()
                .FirstOrDefault(t => t.Name == modelName);

            Assert.IsNotNull(modelType, $"{modelName} model not found!");

            return modelType;
        }

        private static IServiceProvider ConfigureServices<TContext>(string databaseName)
            where TContext : DbContext
        {
            var services = ConfigureDbContext<TContext>(databaseName);

            var context = services.GetService<TContext>();

            try
            {
                context.Model.GetEntityTypes();
            }
            catch (InvalidOperationException ex) when (ex.Source == "Microsoft.EntityFrameworkCore.Proxies")
            {
                services = ConfigureDbContext<TContext>(databaseName, useLazyLoading: true);
            }

            return services;
        }

        private static IServiceProvider ConfigureDbContext<TContext>(string databaseName, bool useLazyLoading = false)
            where TContext : DbContext
        {
            var services = new ServiceCollection()
               .AddDbContext<TContext>(t => t
               .UseInMemoryDatabase(Guid.NewGuid().ToString())
               );

            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider;
        }
    }
}
