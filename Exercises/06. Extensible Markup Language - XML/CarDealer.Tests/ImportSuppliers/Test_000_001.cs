namespace CarDealer.Tests.ImportSuppliers
{
    //ReSharper disable InconsistentNaming, CheckNamespace

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
            var config = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<CarDealerProfile>();
                });

            this.serviceProvider = ConfigureServices<CarDealerContext>(Guid.NewGuid().ToString());
        }

        [Test]
        public void ImportSuppliersZeroTest()
        {
            var context = this.serviceProvider.GetService<CarDealerContext>();

            var suppliersXml =
                "<?xml version=\"1.0\" encoding=\"UTF-8\" ?><Suppliers><Supplier><name>3M Company</name><isImporter>true</isImporter></Supplier><Supplier><name>Agway Inc.</name><isImporter>false</isImporter></Supplier><Supplier><name>Anthem, Inc.</name><isImporter>true</isImporter></Supplier><Supplier><name>Airgas, Inc.</name><isImporter>false</isImporter></Supplier><Supplier><name>Atmel Corporation</name><isImporter>true</isImporter></Supplier><Supplier><name>Big Lots, Inc.</name><isImporter>true</isImporter></Supplier><Supplier><name>Caterpillar Inc.</name><isImporter>false</isImporter></Supplier><Supplier><name>Casey&#x27;s General Stores Inc.</name><isImporter>true</isImporter></Supplier><Supplier><name>Cintas Corp.</name><isImporter>false</isImporter></Supplier><Supplier><name>Chubb Corp</name><isImporter>true</isImporter></Supplier><Supplier><name>Cintas Corp.</name><isImporter>false</isImporter></Supplier><Supplier><name>CNF Inc.</name><isImporter>true</isImporter></Supplier><Supplier><name>CMGI Inc.</name><isImporter>true</isImporter></Supplier><Supplier><name>The Clorox Co.</name><isImporter>false</isImporter></Supplier><Supplier><name>Danaher Corporation</name><isImporter>true</isImporter></Supplier><Supplier><name>E.I. Du Pont de Nemours and Company</name><isImporter>false</isImporter></Supplier><Supplier><name>E*Trade Group, Inc.</name><isImporter>true</isImporter></Supplier><Supplier><name>Emcor Group Inc.</name><isImporter>true</isImporter></Supplier><Supplier><name>GenCorp Inc.</name><isImporter>false</isImporter></Supplier><Supplier><name>IDT Corporation</name><isImporter>true</isImporter></Supplier><Supplier><name>Level 3 Communications Inc.</name><isImporter>false</isImporter></Supplier><Supplier><name>Merck &amp; Co., Inc.</name><isImporter>true</isImporter></Supplier><Supplier><name>Nicor Inc</name><isImporter>false</isImporter></Supplier><Supplier><name>Olin Corp.</name><isImporter>true</isImporter></Supplier><Supplier><name>Paychex Inc</name><isImporter>true</isImporter></Supplier><Supplier><name>Saks Inc</name><isImporter>false</isImporter></Supplier><Supplier><name>Sunoco Inc.</name><isImporter>true</isImporter></Supplier><Supplier><name>Textron Inc</name><isImporter>true</isImporter></Supplier><Supplier><name>VF Corporation</name><isImporter>false</isImporter></Supplier><Supplier><name>Wyeth</name><isImporter>true</isImporter></Supplier><Supplier><name>Zale</name><isImporter>false</isImporter></Supplier></Suppliers>";

            var actualOutput =
                StartUp.ImportSuppliers(context, suppliersXml);

            var expectedOutput = "Successfully imported 31";

            var assertContext = this.serviceProvider.GetService<CarDealerContext>();

            const int expectedSuppliersCount = 31;
            var actualGameCount = assertContext.Suppliers.Count();

            Assert.That(actualGameCount, Is.EqualTo(expectedSuppliersCount),
                $"Inserted {nameof(context.Suppliers)} count is incorrect!");

            Assert.That(actualOutput, Is.EqualTo(expectedOutput).NoClip,
                $"{nameof(StartUp.ImportSuppliers)} output is incorrect!");
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
            var services = new ServiceCollection();

            services
                .AddDbContext<TContext>(
                    options => options
                        .UseInMemoryDatabase(databaseName)
                        .UseLazyLoadingProxies(useLazyLoading)
                );

            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider;
        }
    }
}