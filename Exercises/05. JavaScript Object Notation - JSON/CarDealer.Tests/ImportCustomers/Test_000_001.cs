﻿namespace CarDealer.Tests.ImportCustomers
{
    using System;
    using System.Linq;
    using System.Reflection;
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using NUnit.Framework;
    using CarDealer;
    using CarDealer.Data;

    [TestFixture]
    public class Test_000_001
    {
        private IServiceProvider serviceProvider;

        private static readonly Assembly CurrentAssembly = typeof(StartUp).Assembly;

        [SetUp]
        public void Setup()
        {
            Mapper.Reset();
            Mapper.Initialize(cfg => cfg.AddProfile(GetType("CarDealerProfile")));

            this.serviceProvider = ConfigureServices<CarDealerContext>("CarDealer");
        }

        [Test]
        public void ImportCustomersZeroTest()
        {
            var context = this.serviceProvider.GetService<CarDealerContext>();

            var customersJson =
                "[{\"name\":\"Emmitt Benally\",\"birthDate\":\"1993-11-20T00:00:00\",\"isYoungDriver\":true},{\"name\":\"Natalie Poli\",\"birthDate\":\"1990-10-04T00:00:00\",\"isYoungDriver\":false},{\"name\":\"Marcelle Griego\",\"birthDate\":\"1990-10-04T00:00:00\",\"isYoungDriver\":true},{\"name\":\"Zada Attwood\",\"birthDate\":\"1982-10-01T00:00:00\",\"isYoungDriver\":true},{\"name\":\"Hai Everton\",\"birthDate\":\"1985-10-01T00:00:00\",\"isYoungDriver\":true},{\"name\":\"Lino Subia\",\"birthDate\":\"1985-12-21T00:00:00\",\"isYoungDriver\":false},{\"name\":\"Rico Peer\",\"birthDate\":\"1984-10-01T00:00:00\",\"isYoungDriver\":true},{\"name\":\"Teddy Hobby\",\"birthDate\":\"1975-10-01T00:00:00\",\"isYoungDriver\":true},{\"name\":\"Johnette Derryberry\",\"birthDate\":\"1995-10-01T00:00:00\",\"isYoungDriver\":true},{\"name\":\"Audrea Cardinal\",\"birthDate\":\"1976-10-01T00:00:00\",\"isYoungDriver\":false},{\"name\":\"Francis Mckim\",\"birthDate\":\"1977-10-01T00:00:00\",\"isYoungDriver\":true},{\"name\":\"Carole Witman\",\"birthDate\":\"1987-10-01T00:00:00\",\"isYoungDriver\":false},{\"name\":\"Oren Perlman\",\"birthDate\":\"1988-10-01T00:00:00\",\"isYoungDriver\":true},{\"name\":\"Brett Brickley\",\"birthDate\":\"1980-10-01T00:00:00\",\"isYoungDriver\":true},{\"name\":\"Kristian Engberg\",\"birthDate\":\"1981-10-03T00:00:00\",\"isYoungDriver\":false},{\"name\":\"Carri Knapik\",\"birthDate\":\"1972-02-02T00:00:00\",\"isYoungDriver\":true},{\"name\":\"Rema Revelle\",\"birthDate\":\"1970-05-01T00:00:00\",\"isYoungDriver\":true},{\"name\":\"Taina Achenbach\",\"birthDate\":\"1994-10-01T00:00:00\",\"isYoungDriver\":false},{\"name\":\"Yvonne Mccalla\",\"birthDate\":\"1992-03-02T00:00:00\",\"isYoungDriver\":true},{\"name\":\"Nisha Markwell\",\"birthDate\":\"1994-04-04T00:00:00\",\"isYoungDriver\":false},{\"name\":\"Cinthia Lasala\",\"birthDate\":\"1992-11-01T00:00:00\",\"isYoungDriver\":true},{\"name\":\"Daniele Zarate\",\"birthDate\":\"1995-10-05T00:00:00\",\"isYoungDriver\":false},{\"name\":\"Faustina Burgher\",\"birthDate\":\"1994-06-01T00:00:00\",\"isYoungDriver\":false},{\"name\":\"Jimmy Grossi\",\"birthDate\":\"1986-07-01T00:00:00\",\"isYoungDriver\":true},{\"name\":\"Sylvie Mcelravy\",\"birthDate\":\"1983-10-01T00:00:00\",\"isYoungDriver\":false},{\"name\":\"Hipolito Lamoreaux\",\"birthDate\":\"1982-08-01T00:00:00\",\"isYoungDriver\":true},{\"name\":\"Garret Capron\",\"birthDate\":\"1975-07-01T00:00:00\",\"isYoungDriver\":false},{\"name\":\"Donnetta Soliz\",\"birthDate\":\"1963-10-01T00:00:00\",\"isYoungDriver\":true},{\"name\":\"Louann Holzworth\",\"birthDate\":\"1960-10-01T00:00:00\",\"isYoungDriver\":false},{\"name\":\"Ann Mcenaney\",\"birthDate\":\"1992-03-02T00:00:00\",\"isYoungDriver\":true}]";

            var actualOutput =
                StartUp.ImportCustomers(context, customersJson);

            var expectedOutput = "Successfully imported 30.";

            var assertContext = this.serviceProvider.GetService<CarDealerContext>();

            const int expectedCarsCount = 30;
            var actualCarsCount = assertContext.Customers.Count();

            Assert.That(actualCarsCount, Is.EqualTo(expectedCarsCount),
                $"Inserted {nameof(context.Customers)} count is incorrect!");

            Assert.That(actualOutput, Is.EqualTo(expectedOutput).NoClip,
                $"{nameof(StartUp.ImportCustomers)} output is incorrect!");
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
