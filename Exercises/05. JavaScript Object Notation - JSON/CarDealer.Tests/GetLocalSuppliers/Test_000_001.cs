﻿namespace CarDealer.Tests.GetLocalSuppliers
{
    //ReSharper disable InconsistentNaming, CheckNamespace

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
        public void ExportLocalSuppliersZeroTests()
        {
            var context = this.serviceProvider.GetService<CarDealerContext>();

            SeedDatabase(context);

            var expectedOutputValue = "[{\"Id\":2,\"Name\":\"Agway Inc.\",\"PartsCount\":3},{\"Id\":4,\"Name\":\"Airgas, Inc.\",\"PartsCount\":2},{\"Id\":7,\"Name\":\"Caterpillar Inc.\",\"PartsCount\":2},{\"Id\":9,\"Name\":\"Cintas Corp.\",\"PartsCount\":2},{\"Id\":11,\"Name\":\"Cintas Corp.\",\"PartsCount\":12},{\"Id\":14,\"Name\":\"The Clorox Co.\",\"PartsCount\":4},{\"Id\":16,\"Name\":\"E.I. Du Pont de Nemours and Company\",\"PartsCount\":6},{\"Id\":19,\"Name\":\"GenCorp Inc.\",\"PartsCount\":9},{\"Id\":21,\"Name\":\"Level 3 Communications Inc.\",\"PartsCount\":1},{\"Id\":23,\"Name\":\"Nicor Inc\",\"PartsCount\":1},{\"Id\":26,\"Name\":\"Saks Inc\",\"PartsCount\":1},{\"Id\":29,\"Name\":\"VF Corporation\",\"PartsCount\":1},{\"Id\":31,\"Name\":\"Zale\",\"PartsCount\":5}]";

            var expectedOutput = JToken.Parse(expectedOutputValue);
            var actualOutputValue = StartUp.GetLocalSuppliers(context);
            var actualOutput = JToken.Parse(actualOutputValue);

            var expected = expectedOutput.ToString(Formatting.None);
            var actual = actualOutput.ToString(Formatting.None);

            Assert.That(actual, Is.EqualTo(expected).NoClip,
                $"{nameof(StartUp.GetLocalSuppliers)} output is incorrect!");
        }

        private static void SeedDatabase(CarDealerContext context)
        {

            var suppliersJson =
                @"[{""name"":""3M Company"",""isImporter"":true},{""name"":""Agway Inc."",""isImporter"":false},{""name"":""Anthem, Inc."",""isImporter"":true},{""name"":""Airgas, Inc."",""isImporter"":false},{""name"":""Atmel Corporation"",""isImporter"":true},{""name"":""Big Lots, Inc."",""isImporter"":true},{""name"":""Caterpillar Inc."",""isImporter"":false},{""name"":""Casey's General Stores Inc."",""isImporter"":true},{""name"":""Cintas Corp."",""isImporter"":false},{""name"":""Chubb Corp"",""isImporter"":true},{""name"":""Cintas Corp."",""isImporter"":false},{""name"":""CNF Inc."",""isImporter"":true},{""name"":""CMGI Inc."",""isImporter"":true},{""name"":""The Clorox Co."",""isImporter"":false},{""name"":""Danaher Corporation"",""isImporter"":true},{""name"":""E.I. Du Pont de Nemours and Company"",""isImporter"":false},{""name"":""E*Trade Group, Inc."",""isImporter"":true},{""name"":""Emcor Group Inc."",""isImporter"":true},{""name"":""GenCorp Inc."",""isImporter"":false},{""name"":""IDT Corporation"",""isImporter"":true},{""name"":""Level 3 Communications Inc."",""isImporter"":false},{""name"":""Merck & Co., Inc."",""isImporter"":true},{""name"":""Nicor Inc"",""isImporter"":false},{""name"":""Olin Corp."",""isImporter"":true},{""name"":""Paychex Inc"",""isImporter"":true},{""name"":""Saks Inc"",""isImporter"":false},{""name"":""Sunoco Inc."",""isImporter"":true},{""name"":""Textron Inc"",""isImporter"":true},{""name"":""VF Corporation"",""isImporter"":false},{""name"":""Wyeth"",""isImporter"":true},{""name"":""Zale"",""isImporter"":false}]";

            var partsJson =
               "[{\"name\":\"Bonnet/hood\",\"price\":1001.34,\"quantity\":10,\"supplierId\":17},{\"name\":\"Unexposed bumper\",\"price\":1003.34,\"quantity\":10,\"supplierId\":12},{\"name\":\"Exposed bumper\",\"price\":1400.34,\"quantity\":10,\"supplierId\":13},{\"name\":\"Cowl screen\",\"price\":1500.34,\"quantity\":10,\"supplierId\":22},{\"name\":\"Decklid\",\"price\":1060.34,\"quantity\":11,\"supplierId\":19},{\"name\":\"Fascia\",\"price\":100.34,\"quantity\":10,\"supplierId\":18},{\"name\":\"Fender\",\"price\":10.34,\"quantity\":10,\"supplierId\":16},{\"name\":\"Front clip\",\"price\":100,\"quantity\":10,\"supplierId\":11},{\"name\":\"Front fascia\",\"price\":11.99,\"quantity\":10,\"supplierId\":11},{\"name\":\"Grille\",\"price\":144.99,\"quantity\":10,\"supplierId\":12},{\"name\":\"Pillar\",\"price\":100.99,\"quantity\":10,\"supplierId\":32},{\"name\":\"Quarter panel\",\"price\":100.99,\"quantity\":200,\"supplierId\":12},{\"name\":\"Radiator \",\"price\":100.99,\"quantity\":10,\"supplierId\":56},{\"name\":\"Rocker\",\"price\":100.99,\"quantity\":10,\"supplierId\":41},{\"name\":\"Roof rack\",\"price\":100.99,\"quantity\":10,\"supplierId\":1},{\"name\":\"Spoiler\",\"price\":3000.99,\"quantity\":10,\"supplierId\":12},{\"name\":\"Rims\",\"price\":4020.99,\"quantity\":10,\"supplierId\":31},{\"name\":\"Trim package\",\"price\":1900.99,\"quantity\":10,\"supplierId\":65},{\"name\":\"Trunk/boot/hatch\",\"price\":2200.99,\"quantity\":300,\"supplierId\":32},{\"name\":\"Valance\",\"price\":1002.99,\"quantity\":10,\"supplierId\":22},{\"name\":\"Welded assembly\",\"price\":1020.99,\"quantity\":10,\"supplierId\":13},{\"name\":\"Front Right Outer door handle\",\"price\":999.99,\"quantity\":345,\"supplierId\":12},{\"name\":\"Front Left Side Outer door handle\",\"price\":999.99,\"quantity\":10,\"supplierId\":12},{\"name\":\"Rear Right Side Outer door handle\",\"price\":80.99,\"quantity\":10,\"supplierId\":13},{\"name\":\"Rear Left Side Outer door handle\",\"price\":120.99,\"quantity\":10,\"supplierId\":12},{\"name\":\"Front Right Side Inner door handle\",\"price\":100.99,\"quantity\":10,\"supplierId\":13},{\"name\":\"Front Left Side Inner door handle\",\"price\":77.99,\"quantity\":1032,\"supplierId\":13},{\"name\":\"Rear Right Side Inner door handle\",\"price\":79.99,\"quantity\":10,\"supplierId\":19},{\"name\":\"Rear Left Side Inner door handle\",\"price\":93.99,\"quantity\":234,\"supplierId\":19},{\"name\":\"Back Door Outer Door Handle\",\"price\":43.99,\"quantity\":10,\"supplierId\":15},{\"name\":\"Front Right Side Window motor\",\"price\":44.99,\"quantity\":10,\"supplierId\":13},{\"name\":\"Front Left Side Window motor\",\"price\":56.99,\"quantity\":10,\"supplierId\":17},{\"name\":\"Rear Right Side Window motor\",\"price\":22.99,\"quantity\":10,\"supplierId\":19},{\"name\":\"Rear Left Side Window motor\",\"price\":144.99,\"quantity\":10,\"supplierId\":19},{\"name\":\"Door control module\",\"price\":421.99,\"quantity\":10,\"supplierId\":17},{\"name\":\"Door seal\",\"price\":444.99,\"quantity\":10,\"supplierId\":16},{\"name\":\"Door water-shield\",\"price\":123.99,\"quantity\":10,\"supplierId\":15},{\"name\":\"Hinge\",\"price\":11.99,\"quantity\":10,\"supplierId\":14},{\"name\":\"Door latch\",\"price\":2331.99,\"quantity\":10,\"supplierId\":13},{\"name\":\"Door lock and power door locks\",\"price\":120.99,\"quantity\":10,\"supplierId\":12},{\"name\":\"Center-locking\",\"price\":313.99,\"quantity\":10,\"supplierId\":12},{\"name\":\"Fuel tank\",\"price\":100.98,\"quantity\":10,\"supplierId\":13},{\"name\":\"Front Right Side Door Glass\",\"price\":100.91,\"quantity\":10,\"supplierId\":16},{\"name\":\"Front Left Side Door Glass\",\"price\":100.92,\"quantity\":10,\"supplierId\":19},{\"name\":\"Rear Right Side Door Glass\",\"price\":100.9,\"quantity\":10,\"supplierId\":16},{\"name\":\"Rear Left Side Door Glass\",\"price\":100.66,\"quantity\":10,\"supplierId\":15},{\"name\":\"Rear Right Quarter Glass\",\"price\":100.66,\"quantity\":10,\"supplierId\":12},{\"name\":\"Rear Left Quarter Glass\",\"price\":100.55,\"quantity\":10,\"supplierId\":51},{\"name\":\"Sunroof\",\"price\":103.43,\"quantity\":10,\"supplierId\":12},{\"name\":\"Sunroof Rail\",\"price\":100.25,\"quantity\":10,\"supplierId\":31},{\"name\":\"Sunroof Glass\",\"price\":205.24,\"quantity\":10,\"supplierId\":22},{\"name\":\"Window motor\",\"price\":123.49,\"quantity\":10,\"supplierId\":13},{\"name\":\"Window regulator\",\"price\":100.99,\"quantity\":10,\"supplierId\":61},{\"name\":\"Windshield\",\"price\":100.99,\"quantity\":10,\"supplierId\":14},{\"name\":\"Window seal\",\"price\":100.99,\"quantity\":10,\"supplierId\":31},{\"name\":\"Engine\",\"price\":100.99,\"quantity\":10,\"supplierId\":12},{\"name\":\"Camshaft\",\"price\":100.98,\"quantity\":10,\"supplierId\":12},{\"name\":\"Crank case\",\"price\":2.93,\"quantity\":10,\"supplierId\":11},{\"name\":\"Crank pulley\",\"price\":2.8,\"quantity\":10,\"supplierId\":9},{\"name\":\"Crankshaft\",\"price\":4.99,\"quantity\":10,\"supplierId\":6},{\"name\":\"Cylinder head\",\"price\":6.99,\"quantity\":10,\"supplierId\":5},{\"name\":\"Distributor\",\"price\":7.99,\"quantity\":10,\"supplierId\":4},{\"name\":\"Distributor cap\",\"price\":9.99,\"quantity\":10,\"supplierId\":1},{\"name\":\"Drive belt\",\"price\":3.99,\"quantity\":10,\"supplierId\":7},{\"name\":\"Engine block\",\"price\":40.99,\"quantity\":10,\"supplierId\":1},{\"name\":\"Engine cradle\",\"price\":39.99,\"quantity\":10,\"supplierId\":11},{\"name\":\"Engine shake damper and vibration absorber\",\"price\":100.99,\"quantity\":10,\"supplierId\":11},{\"name\":\"Engine valve\",\"price\":49.99,\"quantity\":10,\"supplierId\":11},{\"name\":\"Fan belt\",\"price\":10.99,\"quantity\":10,\"supplierId\":11},{\"name\":\"Gudgeon pin\",\"price\":44.99,\"quantity\":10,\"supplierId\":16},{\"name\":\"Harmonic balancer\",\"price\":67.99,\"quantity\":10,\"supplierId\":12},{\"name\":\"Heater\",\"price\":82.99,\"quantity\":10,\"supplierId\":11},{\"name\":\"Piston\",\"price\":94.99,\"quantity\":10,\"supplierId\":15},{\"name\":\"Poppet valve\",\"price\":88.99,\"quantity\":10,\"supplierId\":11},{\"name\":\"Rocker arm\",\"price\":98.99,\"quantity\":10,\"supplierId\":11},{\"name\":\"Rocker cover\",\"price\":10.99,\"quantity\":10,\"supplierId\":13},{\"name\":\"Starter motor\",\"price\":0.99,\"quantity\":10,\"supplierId\":13},{\"name\":\"Turbocharger\",\"price\":0.4,\"quantity\":10,\"supplierId\":2},{\"name\":\"Tappet\",\"price\":300.29,\"quantity\":10,\"supplierId\":19},{\"name\":\"Timing tape\",\"price\":40.9,\"quantity\":10,\"supplierId\":14},{\"name\":\"Valve cover\",\"price\":130.99,\"quantity\":10,\"supplierId\":13},{\"name\":\"Valve housing\",\"price\":400.99,\"quantity\":10,\"supplierId\":31},{\"name\":\"Valve spring\",\"price\":104.99,\"quantity\":10,\"supplierId\":51},{\"name\":\"Valve stem seal\",\"price\":120.99,\"quantity\":10,\"supplierId\":13},{\"name\":\"Water pump pulley\",\"price\":150.99,\"quantity\":10,\"supplierId\":11},{\"name\":\"Air blower\",\"price\":140.99,\"quantity\":10,\"supplierId\":11},{\"name\":\"Coolant hose \",\"price\":150.99,\"quantity\":10,\"supplierId\":12},{\"name\":\"Cooling fan\",\"price\":120.99,\"quantity\":10,\"supplierId\":12},{\"name\":\"Fan blade\",\"price\":150.99,\"quantity\":10,\"supplierId\":1},{\"name\":\"Fan clutch\",\"price\":120.99,\"quantity\":10,\"supplierId\":34},{\"name\":\"Radiator bolt\",\"price\":160.99,\"quantity\":10,\"supplierId\":33},{\"name\":\"Radiator (fan) shroud\",\"price\":300.99,\"quantity\":10,\"supplierId\":32},{\"name\":\"Radiator gasket\",\"price\":140.99,\"quantity\":10,\"supplierId\":31},{\"name\":\"Radiator pressure cap\",\"price\":600.99,\"quantity\":10,\"supplierId\":30},{\"name\":\"Overflow tank\",\"price\":1200.99,\"quantity\":10,\"supplierId\":29},{\"name\":\"Thermostat\",\"price\":1050.99,\"quantity\":10,\"supplierId\":28},{\"name\":\"Water neck\",\"price\":900.99,\"quantity\":10,\"supplierId\":27},{\"name\":\"Water neck o-ring\",\"price\":43.99,\"quantity\":10,\"supplierId\":26},{\"name\":\"Water pipe\",\"price\":44.94,\"quantity\":10,\"supplierId\":25},{\"name\":\"Water pump\",\"price\":44.93,\"quantity\":10,\"supplierId\":24},{\"name\":\"Water pump gasket\",\"price\":120.29,\"quantity\":10,\"supplierId\":23},{\"name\":\"Water tank\",\"price\":100.99,\"quantity\":10,\"supplierId\":22},{\"name\":\"Oil filter\",\"price\":200.99,\"quantity\":10,\"supplierId\":21},{\"name\":\"Oil gasket\",\"price\":130.99,\"quantity\":10,\"supplierId\":20},{\"name\":\"Oil pan\",\"price\":140.99,\"quantity\":10,\"supplierId\":19},{\"name\":\"Oil pipe\",\"price\":100.29,\"quantity\":10,\"supplierId\":18},{\"name\":\"Oil pump\",\"price\":100.19,\"quantity\":10,\"supplierId\":17},{\"name\":\"Oil strainer\",\"price\":45.29,\"quantity\":10,\"supplierId\":16},{\"name\":\"Catalytic converter\",\"price\":540.99,\"quantity\":10,\"supplierId\":15},{\"name\":\"Exhaust clamp and bracket\",\"price\":140.99,\"quantity\":10,\"supplierId\":14},{\"name\":\"Exhaust flange gasket\",\"price\":130.99,\"quantity\":10,\"supplierId\":13},{\"name\":\"Resonator\",\"price\":106.99,\"quantity\":10,\"supplierId\":12},{\"name\":\"Muffler\",\"price\":106.9,\"quantity\":10,\"supplierId\":12},{\"name\":\"Exhaust pipe\",\"price\":130.99,\"quantity\":10,\"supplierId\":10},{\"name\":\"Heat shield\",\"price\":10.99,\"quantity\":10,\"supplierId\":9},{\"name\":\"Air filter\",\"price\":200.99,\"quantity\":10,\"supplierId\":8},{\"name\":\"Choke cable\",\"price\":130.99,\"quantity\":10,\"supplierId\":7},{\"name\":\"Fuel cooler\",\"price\":140.99,\"quantity\":10,\"supplierId\":6},{\"name\":\"Fuel filter\",\"price\":162.99,\"quantity\":10,\"supplierId\":5},{\"name\":\"Fuel rail\",\"price\":107.99,\"quantity\":10,\"supplierId\":4},{\"name\":\"Fuel pressure regulator\",\"price\":100.99,\"quantity\":10,\"supplierId\":3},{\"name\":\"Steering arm\",\"price\":104.99,\"quantity\":10,\"supplierId\":2},{\"name\":\"Steering box\",\"price\":103.99,\"quantity\":10,\"supplierId\":1},{\"name\":\"Clutch assembly\",\"price\":120.99,\"quantity\":10,\"supplierId\":19},{\"name\":\"Master cylinder\",\"price\":130.99,\"quantity\":10,\"supplierId\":54},{\"name\":\"Speed reducer\",\"price\":14.99,\"quantity\":10,\"supplierId\":13},{\"name\":\"Transmission gear\",\"price\":15.99,\"quantity\":10,\"supplierId\":43},{\"name\":\"Transmission pan\",\"price\":106.99,\"quantity\":10,\"supplierId\":12},{\"name\":\"Differential seal\",\"price\":109.99,\"quantity\":10,\"supplierId\":3},{\"name\":\"Differential clutch\",\"price\":120.99,\"quantity\":10,\"supplierId\":3},{\"name\":\"Clutch shoe\",\"price\":10.49,\"quantity\":10,\"supplierId\":2}]";

            var suppliers = JsonConvert.DeserializeObject<List<Supplier>>(suppliersJson);
            var parts = JsonConvert.DeserializeObject<List<Part>>(partsJson);

            context.Suppliers.AddRange(suppliers);
            context.Parts.AddRange(parts);
            context.SaveChanges();


            context.Customers.AddRange();
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
    }
}
