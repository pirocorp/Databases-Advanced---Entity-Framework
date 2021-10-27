namespace CarDealer.Tests.GetTotalSalesByCustomer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using NUnit.Framework;
    using CarDealer;
    using CarDealer.Data;
    using CarDealer.Models;

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
        public void ExportTotalSalesByCustomerZeroTests()
        {
            var context = this.serviceProvider.GetService<CarDealerContext>();

            SeedDatabase(context);

            var expectedOutputValue = "[{\"fullName\":\"Emmitt Benally\",\"boughtCars\":2,\"spentMoney\":12176.73},{\"fullName\":\"Natalie Poli\",\"boughtCars\":3,\"spentMoney\":9376.39},{\"fullName\":\"Marcelle Griego\",\"boughtCars\":1,\"spentMoney\":6211.03},{\"fullName\":\"Zada Attwood\",\"boughtCars\":1,\"spentMoney\":3405.02},{\"fullName\":\"Lino Subia\",\"boughtCars\":1,\"spentMoney\":1246.67},{\"fullName\":\"Hai Everton\",\"boughtCars\":1,\"spentMoney\":267.32}]";

            var expectedOutput = JToken.Parse(expectedOutputValue);
            var actualOutputValue = StartUp.GetTotalSalesByCustomer(context);
            var actualOutput = JToken.Parse(actualOutputValue);

            var expected = expectedOutput.ToString(Formatting.Indented);
            var actual = actualOutput.ToString(Formatting.Indented);

            Assert.That(actual, Is.EqualTo(expected).NoClip,
                $"{nameof(StartUp.GetTotalSalesByCustomer)} output is incorrect!");
        }

        private static void SeedDatabase(CarDealerContext context)
        {
            var partsJson =
                "[{\"name\":\"Bonnet/hood\",\"price\":1001.34,\"quantity\":10,\"supplierId\":17},{\"name\":\"Unexposed bumper\",\"price\":1003.34,\"quantity\":10,\"supplierId\":12},{\"name\":\"Exposed bumper\",\"price\":1400.34,\"quantity\":10,\"supplierId\":13},{\"name\":\"Cowl screen\",\"price\":1500.34,\"quantity\":10,\"supplierId\":22},{\"name\":\"Decklid\",\"price\":1060.34,\"quantity\":11,\"supplierId\":19},{\"name\":\"Fascia\",\"price\":100.34,\"quantity\":10,\"supplierId\":18},{\"name\":\"Fender\",\"price\":10.34,\"quantity\":10,\"supplierId\":16},{\"name\":\"Front clip\",\"price\":100,\"quantity\":10,\"supplierId\":11},{\"name\":\"Front fascia\",\"price\":11.99,\"quantity\":10,\"supplierId\":11},{\"name\":\"Grille\",\"price\":144.99,\"quantity\":10,\"supplierId\":12},{\"name\":\"Pillar\",\"price\":100.99,\"quantity\":10,\"supplierId\":32}]";

            var carsJson =
                "[{\"make\":\"Opel\",\"model\":\"Omega\",\"travelledDistance\":176664996,\"partsId\":[1,2,3,4,5,6,1,2,3,4,10]},{\"make\":\"Opel\",\"model\":\"Astra\",\"travelledDistance\":516628215,\"partsId\":[5,2,2]},{\"make\":\"Opel\",\"model\":\"Astra\",\"travelledDistance\":156191509,\"partsId\":[1,6,10]},{\"make\":\"Opel\",\"model\":\"Corsa\",\"travelledDistance\":347259126,\"partsId\":[10,9,8,7]},{\"make\":\"Opel\",\"model\":\"Kadet\",\"travelledDistance\":31737446,\"partsId\":[1,2,3]},{\"make\":\"Opel\",\"model\":\"Vectra\",\"travelledDistance\":238042093,\"partsId\":[1,2,3,4,5,6]},{\"make\":\"Opel\",\"model\":\"Insignia\",\"travelledDistance\":225253817,\"partsId\":[1,2,3,4,5]}]";

            var customersJson =
               "[{\"name\":\"Emmitt Benally\",\"birthDate\":\"1993-11-20T00:00:00\",\"isYoungDriver\":true},{\"name\":\"Natalie Poli\",\"birthDate\":\"1990-10-04T00:00:00\",\"isYoungDriver\":false},{\"name\":\"Marcelle Griego\",\"birthDate\":\"1990-10-04T00:00:00\",\"isYoungDriver\":true},{\"name\":\"Zada Attwood\",\"birthDate\":\"1982-10-01T00:00:00\",\"isYoungDriver\":true},{\"name\":\"Hai Everton\",\"birthDate\":\"1985-10-01T00:00:00\",\"isYoungDriver\":true},{\"name\":\"Lino Subia\",\"birthDate\":\"1985-12-21T00:00:00\",\"isYoungDriver\":false},{\"name\":\"Rico Peer\",\"birthDate\":\"1984-10-01T00:00:00\",\"isYoungDriver\":true},{\"name\":\"Teddy Hobby\",\"birthDate\":\"1975-10-01T00:00:00\",\"isYoungDriver\":true},{\"name\":\"Johnette Derryberry\",\"birthDate\":\"1995-10-01T00:00:00\",\"isYoungDriver\":true},{\"name\":\"Audrea Cardinal\",\"birthDate\":\"1976-10-01T00:00:00\",\"isYoungDriver\":false}]";

            var salesJson =
               "[{\"carId\":1,\"customerId\":1,\"discount\":30}," +
               "{\"carId\":2,\"customerId\":2,\"discount\":10}," +
               "{\"carId\":3,\"customerId\":2,\"discount\":20}," +
               "{\"carId\":1,\"customerId\":3,\"discount\":50}," +
               "{\"carId\":5,\"customerId\":4,\"discount\":30}," +
               "{\"carId\":6,\"customerId\":2,\"discount\":30}," +
               "{\"carId\":7,\"customerId\":1,\"discount\":10}," +
               "{\"carId\":4,\"customerId\":5,\"discount\":30}," +
            "{\"carId\":3,\"customerId\":6,\"discount\":20},]";

            var parts = JsonConvert.DeserializeObject<List<Part>>(partsJson);
            var carParts = JsonConvert.DeserializeObject<List<CarDtoTest>>(carsJson);
            var customers = JsonConvert.DeserializeObject<List<Customer>>(customersJson);
            var sales = JsonConvert.DeserializeObject<List<Sale>>(salesJson);

            var cars = new List<Car>();
            var partCars = new List<PartCar>();

            foreach (var carPart in carParts)
            {

                var car = new Car
                {
                    Make = carPart.Make,
                    Model = carPart.Model,
                    TravelledDistance = carPart.TravelledDistance
                };

                cars.Add(car);

                foreach (var partId in carPart.PartsId.Distinct())
                {
                    partCars.Add(new PartCar
                    {
                        Car = car,
                        PartId = partId
                    });
                }
            }

            context.Parts.AddRange(parts);
            context.Cars.AddRange(cars);
            context.PartCars.AddRange(partCars);
            context.Customers.AddRange(customers);
            context.Sales.AddRange(sales);
            context.SaveChanges();
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

        public class CarDtoTest
        {
            public CarDtoTest()
            {
                this.PartsId = new List<int>();
            }

            public string Make { get; set; }

            public string Model { get; set; }

            public long TravelledDistance { get; set; }

            public List<int> PartsId { get; set; }
        }
    }
}
