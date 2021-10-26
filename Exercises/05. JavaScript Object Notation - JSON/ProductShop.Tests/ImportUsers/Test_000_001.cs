namespace ProductShop.Tests
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
        public void ImportUsersZeroTest()
        {
            var context = this.serviceProvider.GetService<ProductShopContext>();

            var inputJson =
                @"[{""firstName"":""Eugene"",""lastName"":""Stewart"",""age"":65},{""firstName"":""Fred"",""lastName"":""Allen"",""age"":57},{""firstName"":""Clarence"",""lastName"":""Fowler"",""age"":50},{""firstName"":""Betty"",""lastName"":""Lawson"",""age"":38},{""firstName"":""Anna"",""lastName"":""Clark"",""age"":41},{""firstName"":""Carl"",""lastName"":""Daniels"",""age"":59},{""firstName"":""Carl"",""lastName"":""Lawson"",""age"":19},{""firstName"":null,""lastName"":""Fox"",""age"":68},{""firstName"":""Gary"",""lastName"":""Stevens"",""age"":null},{""firstName"":null,""lastName"":""Mitchell"",""age"":41},{""firstName"":null,""lastName"":""Bennett"",""age"":null},{""firstName"":""Brenda"",""lastName"":""Howell"",""age"":75},{""firstName"":null,""lastName"":""Schmidt"",""age"":70},{""firstName"":""Brandon"",""lastName"":""Fuller"",""age"":30},{""firstName"":null,""lastName"":""Moreno"",""age"":37},{""firstName"":null,""lastName"":""Stewart"",""age"":39},{""firstName"":null,""lastName"":""Peterson"",""age"":72},{""firstName"":""Patrick"",""lastName"":""King"",""age"":null},{""firstName"":""Rachel"",""lastName"":""Johnson"",""age"":24},{""firstName"":""Sandra"",""lastName"":""Riley"",""age"":74},{""firstName"":""Gloria"",""lastName"":""Alexander"",""age"":61},{""firstName"":null,""lastName"":""Harrison"",""age"":""18""},{""firstName"":""Nicole"",""lastName"":""Harris"",""age"":43},{""firstName"":""Benjamin"",""lastName"":""Henry"",""age"":63},{""firstName"":""Nicole"",""lastName"":""Martinez"",""age"":28},{""firstName"":null,""lastName"":""Baker"",""age"":null},{""firstName"":""Patricia"",""lastName"":""Cooper"",""age"":72},{""firstName"":null,""lastName"":""Thompson"",""age"":46},{""firstName"":""Ann"",""lastName"":""Stevens"",""age"":null},{""firstName"":""Christina"",""lastName"":""Patterson"",""age"":63},{""firstName"":""Sarah"",""lastName"":""Day"",""age"":33},{""firstName"":""Jennifer"",""lastName"":""Riley"",""age"":null},{""firstName"":""Jacqueline"",""lastName"":""Perez"",""age"":25},{""firstName"":""Amanda"",""lastName"":""Frazier"",""age"":null},{""firstName"":""Joshua"",""lastName"":""Murray"",""age"":41},{""firstName"":""Jean"",""lastName"":""Henry"",""age"":null},{""firstName"":""Diana"",""lastName"":""Harvey"",""age"":46},{""firstName"":""Emily"",""lastName"":""Parker"",""age"":41},{""firstName"":""Paula"",""lastName"":""Hill"",""age"":74},{""firstName"":""Billy"",""lastName"":""Parker"",""age"":68},{""firstName"":""Jeremy"",""lastName"":""Woods"",""age"":20},{""firstName"":""Christine"",""lastName"":""Gomez"",""age"":28},{""firstName"":""Jonathan"",""lastName"":""Rodriguez"",""age"":null},{""firstName"":""Kathy"",""lastName"":""Gilbert"",""age"":51},{""firstName"":""Fred"",""lastName"":""Barnes"",""age"":null},{""firstName"":""Anna"",""lastName"":""Parker"",""age"":56},{""firstName"":""Betty"",""lastName"":""Ward"",""age"":70},{""firstName"":""Patricia"",""lastName"":""Fuller"",""age"":36},{""firstName"":""Bonnie"",""lastName"":""Fox"",""age"":18},{""firstName"":""Chris"",""lastName"":""Mitchell"",""age"":59},{""firstName"":null,""lastName"":""Cunningham"",""age"":null},{""firstName"":""Arthur"",""lastName"":""Reynolds"",""age"":32},{""firstName"":""Thomas"",""lastName"":""Snyder"",""age"":40},{""firstName"":""Marie"",""lastName"":""Williamson"",""age"":null},{""firstName"":""Wanda"",""lastName"":""Harris"",""age"":26},{""firstName"":""Doris"",""lastName"":""Cook"",""age"":61}]";

            var actualOutput =
                StartUp.ImportUsers(context, inputJson);

            var expectedOutput = $"Successfully imported 56";

            var assertContext = this.serviceProvider.GetService<ProductShopContext>();

            const int expectedUsersCount = 56;
            var actualGameCount = assertContext.Users.Count();

            Assert.That(actualGameCount, Is.EqualTo(expectedUsersCount),
                $"Inserted {nameof(context.Users)} count is incorrect!");

            Assert.That(actualOutput, Is.EqualTo(expectedOutput).NoClip,
                $"{nameof(StartUp.ImportUsers)} output is incorrect!");
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
                .AddDbContext<TContext>(t 
                    => t.UseInMemoryDatabase(Guid.NewGuid().ToString()));

            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider;
        }
    }
}