﻿namespace CarDealer.Tests.GetSalesWithAppliedDiscount
{
    //ReSharper disable InconsistentNaming, CheckNamespace

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Xml.Linq;
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
            var config = new MapperConfiguration(cfg =>
                 {
                     cfg.AddProfile<CarDealerProfile>();
                 });

            this.serviceProvider = ConfigureServices<CarDealerContext>(Guid.NewGuid().ToString());
        }

        [Test]
        public void ExportSalesWithAppliedDiscountZeroTests()
        {
            var context = this.serviceProvider.GetService<CarDealerContext>();

            SeedDatabase(context);

            var expectedXml = "<?xml version=\"1.0\" encoding=\"utf-16\"?><sales><sale><car make=\"BMW\" model=\"M5 F10\" travelled-distance=\"435603343\" /><discount>30</discount><customer-name>Hipolito Lamoreaux</customer-name><price>139.97</price><price-with-discount>97.979</price-with-discount></sale><sale><car make=\"Opel\" model=\"Astra\" travelled-distance=\"31468479\" /><discount>0</discount><customer-name>Garret Capron</customer-name><price>253.97</price><price-with-discount>253.97</price-with-discount></sale><sale><car make=\"Opel\" model=\"Insignia\" travelled-distance=\"339785118\" /><discount>10</discount><customer-name>Emmitt Benally</customer-name><price>2417.63</price><price-with-discount>2175.867</price-with-discount></sale><sale><car make=\"BMW\" model=\"F25\" travelled-distance=\"476132712\" /><discount>0</discount><customer-name>Sylvie Mcelravy</customer-name><price>3567.74</price><price-with-discount>3567.74</price-with-discount></sale><sale><car make=\"BMW\" model=\"F04\" travelled-distance=\"443756363\" /><discount>50</discount><customer-name>Marcelle Griego</customer-name><price>117.97</price><price-with-discount>58.985</price-with-discount></sale><sale><car make=\"Opel\" model=\"Vectra\" travelled-distance=\"238042093\" /><discount>30</discount><customer-name>Cinthia Lasala</customer-name><price>465.94</price><price-with-discount>326.158</price-with-discount></sale><sale><car make=\"Opel\" model=\"Insignia\" travelled-distance=\"225253817\" /><discount>15</discount><customer-name>Donnetta Soliz</customer-name><price>2940.09</price><price-with-discount>2499.0765</price-with-discount></sale><sale><car make=\"Opel\" model=\"Omega\" travelled-distance=\"277250812\" /><discount>20</discount><customer-name>Teddy Hobby</customer-name><price>4020.62</price><price-with-discount>3216.496</price-with-discount></sale><sale><car make=\"BMW\" model=\"E67\" travelled-distance=\"476830509\" /><discount>15</discount><customer-name>Johnette Derryberry</customer-name><price>1106.84</price><price-with-discount>940.814</price-with-discount></sale></sales>";

            var actualXml = StartUp.GetSalesWithAppliedDiscount(context);
            var expectedOutput = XDocument.Parse(expectedXml);
            var actualOutput = XDocument.Parse(actualXml);

            var expectedOutputXml = expectedOutput.ToString(SaveOptions.None);
            var actualOutputXml = actualOutput.ToString(SaveOptions.None);

            Assert.That(actualOutputXml, Is.EqualTo(expectedOutputXml).NoClip,
                $"{nameof(StartUp.GetSalesWithAppliedDiscount)} output is incorrect!");
        }

        private static void SeedDatabase(CarDealerContext context)
        {
            var datasetsJson =
               "{\"Customer\":[{\"Id\":1,\"Name\":\"Emmitt Benally\",\"BirthDate\":\"1993-11-20T00:00:00\",\"IsYoungDriver\":true},{\"Id\":2,\"Name\":\"Donnetta Soliz\",\"BirthDate\":\"1963-10-01T00:00:00\",\"IsYoungDriver\":true},{\"Id\":3,\"Name\":\"Garret Capron\",\"BirthDate\":\"1975-07-01T00:00:00\",\"IsYoungDriver\":false},{\"Id\":4,\"Name\":\"Hipolito Lamoreaux\",\"BirthDate\":\"1982-08-01T00:00:00\",\"IsYoungDriver\":true},{\"Id\":5,\"Name\":\"Sylvie Mcelravy\",\"BirthDate\":\"1983-10-01T00:00:00\",\"IsYoungDriver\":false},{\"Id\":6,\"Name\":\"Jimmy Grossi\",\"BirthDate\":\"1986-07-01T00:00:00\",\"IsYoungDriver\":true},{\"Id\":7,\"Name\":\"Faustina Burgher\",\"BirthDate\":\"1994-06-01T00:00:00\",\"IsYoungDriver\":false},{\"Id\":8,\"Name\":\"Daniele Zarate\",\"BirthDate\":\"1995-10-05T00:00:00\",\"IsYoungDriver\":false},{\"Id\":9,\"Name\":\"Cinthia Lasala\",\"BirthDate\":\"1992-11-01T00:00:00\",\"IsYoungDriver\":true},{\"Id\":10,\"Name\":\"Nisha Markwell\",\"BirthDate\":\"1994-04-04T00:00:00\",\"IsYoungDriver\":false},{\"Id\":11,\"Name\":\"Yvonne Mccalla\",\"BirthDate\":\"1992-03-02T00:00:00\",\"IsYoungDriver\":true},{\"Id\":12,\"Name\":\"Taina Achenbach\",\"BirthDate\":\"1994-10-01T00:00:00\",\"IsYoungDriver\":false},{\"Id\":13,\"Name\":\"Rema Revelle\",\"BirthDate\":\"1970-05-01T00:00:00\",\"IsYoungDriver\":true},{\"Id\":14,\"Name\":\"Carri Knapik\",\"BirthDate\":\"1972-02-02T00:00:00\",\"IsYoungDriver\":true},{\"Id\":15,\"Name\":\"Kristian Engberg\",\"BirthDate\":\"1981-10-03T00:00:00\",\"IsYoungDriver\":false},{\"Id\":16,\"Name\":\"Brett Brickley\",\"BirthDate\":\"1980-10-01T00:00:00\",\"IsYoungDriver\":true},{\"Id\":17,\"Name\":\"Oren Perlman\",\"BirthDate\":\"1988-10-01T00:00:00\",\"IsYoungDriver\":true},{\"Id\":18,\"Name\":\"Carole Witman\",\"BirthDate\":\"1987-10-01T00:00:00\",\"IsYoungDriver\":false},{\"Id\":19,\"Name\":\"Francis Mckim\",\"BirthDate\":\"1977-10-01T00:00:00\",\"IsYoungDriver\":true},{\"Id\":20,\"Name\":\"Audrea Cardinal\",\"BirthDate\":\"1976-10-01T00:00:00\",\"IsYoungDriver\":false},{\"Id\":21,\"Name\":\"Johnette Derryberry\",\"BirthDate\":\"1995-10-01T00:00:00\",\"IsYoungDriver\":true},{\"Id\":22,\"Name\":\"Teddy Hobby\",\"BirthDate\":\"1975-10-01T00:00:00\",\"IsYoungDriver\":true},{\"Id\":23,\"Name\":\"Rico Peer\",\"BirthDate\":\"1984-10-01T00:00:00\",\"IsYoungDriver\":true},{\"Id\":24,\"Name\":\"Lino Subia\",\"BirthDate\":\"1985-12-21T00:00:00\",\"IsYoungDriver\":false},{\"Id\":25,\"Name\":\"Hai Everton\",\"BirthDate\":\"1985-10-01T00:00:00\",\"IsYoungDriver\":true},{\"Id\":26,\"Name\":\"Zada Attwood\",\"BirthDate\":\"1982-10-01T00:00:00\",\"IsYoungDriver\":true},{\"Id\":27,\"Name\":\"Marcelle Griego\",\"BirthDate\":\"1990-10-04T00:00:00\",\"IsYoungDriver\":true},{\"Id\":28,\"Name\":\"Natalie Poli\",\"BirthDate\":\"1990-10-04T00:00:00\",\"IsYoungDriver\":false},{\"Id\":29,\"Name\":\"Louann Holzworth\",\"BirthDate\":\"1960-10-01T00:00:00\",\"IsYoungDriver\":false},{\"Id\":30,\"Name\":\"Ann Mcenaney\",\"BirthDate\":\"1992-03-02T00:00:00\",\"IsYoungDriver\":true}],\"Car\":[{\"Id\":1,\"Make\":\"Opel\",\"Model\":\"Omega\",\"TravelledDistance\":176664996},{\"Id\":2,\"Make\":\"BMW\",\"Model\":\"F25\",\"TravelledDistance\":476132712},{\"Id\":3,\"Make\":\"BMW\",\"Model\":\"M5 F10\",\"TravelledDistance\":140799461},{\"Id\":4,\"Make\":\"BMW\",\"Model\":\"F04\",\"TravelledDistance\":418839575},{\"Id\":5,\"Make\":\"BMW\",\"Model\":\"E88\",\"TravelledDistance\":27453411},{\"Id\":6,\"Make\":\"BMW\",\"Model\":\"M Roadster E85\",\"TravelledDistance\":475685374},{\"Id\":7,\"Make\":\"BMW\",\"Model\":\"1M Coupe\",\"TravelledDistance\":39826890},{\"Id\":8,\"Make\":\"BMW\",\"Model\":\"F20\",\"TravelledDistance\":401291910},{\"Id\":9,\"Make\":\"BMW\",\"Model\":\"X6 M\",\"TravelledDistance\":322292247},{\"Id\":10,\"Make\":\"BMW\",\"Model\":\"M6 E63\",\"TravelledDistance\":57189891},{\"Id\":11,\"Make\":\"BMW\",\"Model\":\"M6 E63\",\"TravelledDistance\":69863204},{\"Id\":12,\"Make\":\"BMW\",\"Model\":\"F20\",\"TravelledDistance\":487897422},{\"Id\":13,\"Make\":\"BMW\",\"Model\":\"F04\",\"TravelledDistance\":443756363},{\"Id\":14,\"Make\":\"BMW\",\"Model\":\"M5 F10\",\"TravelledDistance\":435603343},{\"Id\":15,\"Make\":\"BMW\",\"Model\":\"X6 M\",\"TravelledDistance\":227443571},{\"Id\":16,\"Make\":\"BMW\",\"Model\":\"E67\",\"TravelledDistance\":476830509},{\"Id\":17,\"Make\":\"Opel\",\"Model\":\"Omega\",\"TravelledDistance\":277250812},{\"Id\":18,\"Make\":\"Opel\",\"Model\":\"Corsa\",\"TravelledDistance\":267253567},{\"Id\":19,\"Make\":\"Opel\",\"Model\":\"Omega\",\"TravelledDistance\":254808828},{\"Id\":20,\"Make\":\"Opel\",\"Model\":\"Astra\",\"TravelledDistance\":516628215},{\"Id\":21,\"Make\":\"Opel\",\"Model\":\"Astra\",\"TravelledDistance\":156191509},{\"Id\":22,\"Make\":\"Opel\",\"Model\":\"Corsa\",\"TravelledDistance\":347259126},{\"Id\":23,\"Make\":\"Opel\",\"Model\":\"Kadet\",\"TravelledDistance\":31737446},{\"Id\":24,\"Make\":\"Opel\",\"Model\":\"Vectra\",\"TravelledDistance\":238042093},{\"Id\":25,\"Make\":\"Opel\",\"Model\":\"Insignia\",\"TravelledDistance\":225253817},{\"Id\":26,\"Make\":\"Opel\",\"Model\":\"Astra\",\"TravelledDistance\":31468479},{\"Id\":27,\"Make\":\"Opel\",\"Model\":\"Corsa\",\"TravelledDistance\":282499542},{\"Id\":28,\"Make\":\"Opel\",\"Model\":\"Omega\",\"TravelledDistance\":111313670},{\"Id\":29,\"Make\":\"Opel\",\"Model\":\"Insignia\",\"TravelledDistance\":168539389},{\"Id\":30,\"Make\":\"Opel\",\"Model\":\"Vectra\",\"TravelledDistance\":433351992},{\"Id\":31,\"Make\":\"Opel\",\"Model\":\"Astra\",\"TravelledDistance\":349837452},{\"Id\":32,\"Make\":\"Opel\",\"Model\":\"Omega\",\"TravelledDistance\":109910837},{\"Id\":33,\"Make\":\"Opel\",\"Model\":\"Corsa\",\"TravelledDistance\":241891505},{\"Id\":34,\"Make\":\"Opel\",\"Model\":\"Insignia\",\"TravelledDistance\":339785118},{\"Id\":35,\"Make\":\"BMW\",\"Model\":\"F20\",\"TravelledDistance\":284809463},{\"Id\":36,\"Make\":\"BMW\",\"Model\":\"X6 M\",\"TravelledDistance\":183346013}],\"Part\":[{\"Id\":1,\"Name\":\"Bonnet/hood\",\"Price\":1001.34,\"Quantity\":10,\"SupplierId\":17},{\"Id\":2,\"Name\":\"Thermostat\",\"Price\":1050.99,\"Quantity\":10,\"SupplierId\":28},{\"Id\":3,\"Name\":\"Overflow tank\",\"Price\":1200.99,\"Quantity\":10,\"SupplierId\":29},{\"Id\":4,\"Name\":\"Radiator pressure cap\",\"Price\":600.99,\"Quantity\":10,\"SupplierId\":30},{\"Id\":5,\"Name\":\"Radiator gasket\",\"Price\":140.99,\"Quantity\":10,\"SupplierId\":31},{\"Id\":6,\"Name\":\"Radiator (fan) shroud\",\"Price\":300.99,\"Quantity\":10,\"SupplierId\":22},{\"Id\":7,\"Name\":\"Radiator bolt\",\"Price\":160.99,\"Quantity\":10,\"SupplierId\":3},{\"Id\":8,\"Name\":\"Fan clutch\",\"Price\":120.99,\"Quantity\":10,\"SupplierId\":3},{\"Id\":9,\"Name\":\"Fan blade\",\"Price\":150.99,\"Quantity\":10,\"SupplierId\":1},{\"Id\":10,\"Name\":\"Cooling fan\",\"Price\":120.99,\"Quantity\":10,\"SupplierId\":12},{\"Id\":11,\"Name\":\"Coolant hose \",\"Price\":150.99,\"Quantity\":10,\"SupplierId\":12},{\"Id\":12,\"Name\":\"Air blower\",\"Price\":140.99,\"Quantity\":10,\"SupplierId\":11},{\"Id\":13,\"Name\":\"Water pump pulley\",\"Price\":150.99,\"Quantity\":10,\"SupplierId\":11},{\"Id\":14,\"Name\":\"Valve stem seal\",\"Price\":120.99,\"Quantity\":10,\"SupplierId\":13},{\"Id\":15,\"Name\":\"Valve spring\",\"Price\":104.99,\"Quantity\":10,\"SupplierId\":11},{\"Id\":16,\"Name\":\"Valve housing\",\"Price\":400.99,\"Quantity\":10,\"SupplierId\":31},{\"Id\":17,\"Name\":\"Valve cover\",\"Price\":130.99,\"Quantity\":10,\"SupplierId\":13},{\"Id\":18,\"Name\":\"Timing tape\",\"Price\":40.9,\"Quantity\":10,\"SupplierId\":14},{\"Id\":19,\"Name\":\"Tappet\",\"Price\":300.29,\"Quantity\":10,\"SupplierId\":19},{\"Id\":20,\"Name\":\"Turbocharger\",\"Price\":0.4,\"Quantity\":10,\"SupplierId\":2},{\"Id\":21,\"Name\":\"Starter motor\",\"Price\":0.99,\"Quantity\":10,\"SupplierId\":13},{\"Id\":22,\"Name\":\"Rocker cover\",\"Price\":10.99,\"Quantity\":10,\"SupplierId\":13},{\"Id\":23,\"Name\":\"Rocker arm\",\"Price\":98.99,\"Quantity\":10,\"SupplierId\":11},{\"Id\":24,\"Name\":\"Poppet valve\",\"Price\":88.99,\"Quantity\":10,\"SupplierId\":11},{\"Id\":25,\"Name\":\"Piston\",\"Price\":94.99,\"Quantity\":10,\"SupplierId\":15},{\"Id\":26,\"Name\":\"Heater\",\"Price\":82.99,\"Quantity\":10,\"SupplierId\":11},{\"Id\":27,\"Name\":\"Harmonic balancer\",\"Price\":67.99,\"Quantity\":10,\"SupplierId\":12},{\"Id\":28,\"Name\":\"Gudgeon pin\",\"Price\":44.99,\"Quantity\":10,\"SupplierId\":16},{\"Id\":29,\"Name\":\"Fan belt\",\"Price\":10.99,\"Quantity\":10,\"SupplierId\":11},{\"Id\":30,\"Name\":\"Engine valve\",\"Price\":49.99,\"Quantity\":10,\"SupplierId\":11},{\"Id\":31,\"Name\":\"Water neck\",\"Price\":900.99,\"Quantity\":10,\"SupplierId\":27},{\"Id\":32,\"Name\":\"Engine shake damper and vibration absorber\",\"Price\":100.99,\"Quantity\":10,\"SupplierId\":11},{\"Id\":33,\"Name\":\"Water neck o-ring\",\"Price\":43.99,\"Quantity\":10,\"SupplierId\":26},{\"Id\":34,\"Name\":\"Water pump\",\"Price\":44.93,\"Quantity\":10,\"SupplierId\":24},{\"Id\":35,\"Name\":\"Differential seal\",\"Price\":109.99,\"Quantity\":10,\"SupplierId\":3},{\"Id\":36,\"Name\":\"Transmission pan\",\"Price\":106.99,\"Quantity\":10,\"SupplierId\":12},{\"Id\":37,\"Name\":\"Transmission gear\",\"Price\":15.99,\"Quantity\":10,\"SupplierId\":23},{\"Id\":38,\"Name\":\"Speed reducer\",\"Price\":14.99,\"Quantity\":10,\"SupplierId\":13},{\"Id\":39,\"Name\":\"Master cylinder\",\"Price\":130.99,\"Quantity\":10,\"SupplierId\":14},{\"Id\":40,\"Name\":\"Clutch assembly\",\"Price\":120.99,\"Quantity\":10,\"SupplierId\":19},{\"Id\":41,\"Name\":\"Steering box\",\"Price\":103.99,\"Quantity\":10,\"SupplierId\":1},{\"Id\":42,\"Name\":\"Steering arm\",\"Price\":104.99,\"Quantity\":10,\"SupplierId\":2},{\"Id\":43,\"Name\":\"Fuel pressure regulator\",\"Price\":100.99,\"Quantity\":10,\"SupplierId\":3},{\"Id\":44,\"Name\":\"Fuel rail\",\"Price\":107.99,\"Quantity\":10,\"SupplierId\":4},{\"Id\":45,\"Name\":\"Fuel filter\",\"Price\":162.99,\"Quantity\":10,\"SupplierId\":5},{\"Id\":46,\"Name\":\"Fuel cooler\",\"Price\":140.99,\"Quantity\":10,\"SupplierId\":6},{\"Id\":47,\"Name\":\"Choke cable\",\"Price\":130.99,\"Quantity\":10,\"SupplierId\":7},{\"Id\":48,\"Name\":\"Air filter\",\"Price\":200.99,\"Quantity\":10,\"SupplierId\":8},{\"Id\":49,\"Name\":\"Heat shield\",\"Price\":10.99,\"Quantity\":10,\"SupplierId\":9},{\"Id\":50,\"Name\":\"Exhaust pipe\",\"Price\":130.99,\"Quantity\":10,\"SupplierId\":10},{\"Id\":51,\"Name\":\"Muffler\",\"Price\":106.9,\"Quantity\":10,\"SupplierId\":12},{\"Id\":52,\"Name\":\"Resonator\",\"Price\":106.99,\"Quantity\":10,\"SupplierId\":12},{\"Id\":53,\"Name\":\"Exhaust flange gasket\",\"Price\":130.99,\"Quantity\":10,\"SupplierId\":13},{\"Id\":54,\"Name\":\"Exhaust clamp and bracket\",\"Price\":140.99,\"Quantity\":10,\"SupplierId\":14},{\"Id\":55,\"Name\":\"Catalytic converter\",\"Price\":540.99,\"Quantity\":10,\"SupplierId\":15},{\"Id\":56,\"Name\":\"Oil strainer\",\"Price\":45.29,\"Quantity\":10,\"SupplierId\":16},{\"Id\":57,\"Name\":\"Oil pump\",\"Price\":100.19,\"Quantity\":10,\"SupplierId\":17},{\"Id\":58,\"Name\":\"Oil pipe\",\"Price\":100.29,\"Quantity\":10,\"SupplierId\":18},{\"Id\":59,\"Name\":\"Oil pan\",\"Price\":140.99,\"Quantity\":10,\"SupplierId\":19},{\"Id\":60,\"Name\":\"Oil gasket\",\"Price\":130.99,\"Quantity\":10,\"SupplierId\":20},{\"Id\":61,\"Name\":\"Oil filter\",\"Price\":200.99,\"Quantity\":10,\"SupplierId\":21},{\"Id\":62,\"Name\":\"Water tank\",\"Price\":100.99,\"Quantity\":10,\"SupplierId\":22},{\"Id\":63,\"Name\":\"Water pump gasket\",\"Price\":120.29,\"Quantity\":10,\"SupplierId\":23},{\"Id\":64,\"Name\":\"Water pipe\",\"Price\":44.94,\"Quantity\":10,\"SupplierId\":25},{\"Id\":65,\"Name\":\"Differential clutch\",\"Price\":120.99,\"Quantity\":10,\"SupplierId\":3},{\"Id\":66,\"Name\":\"Engine cradle\",\"Price\":39.99,\"Quantity\":10,\"SupplierId\":11},{\"Id\":67,\"Name\":\"Drive belt\",\"Price\":3.99,\"Quantity\":10,\"SupplierId\":7},{\"Id\":68,\"Name\":\"Back Door Outer Door Handle\",\"Price\":43.99,\"Quantity\":10,\"SupplierId\":15},{\"Id\":69,\"Name\":\"Rear Left Side Inner door handle\",\"Price\":93.99,\"Quantity\":234,\"SupplierId\":19},{\"Id\":70,\"Name\":\"Rear Right Side Inner door handle\",\"Price\":79.99,\"Quantity\":10,\"SupplierId\":19},{\"Id\":71,\"Name\":\"Front Left Side Inner door handle\",\"Price\":77.99,\"Quantity\":1032,\"SupplierId\":13},{\"Id\":72,\"Name\":\"Front Right Side Inner door handle\",\"Price\":100.99,\"Quantity\":10,\"SupplierId\":13},{\"Id\":73,\"Name\":\"Rear Left Side Outer door handle\",\"Price\":120.99,\"Quantity\":10,\"SupplierId\":12},{\"Id\":74,\"Name\":\"Rear Right Side Outer door handle\",\"Price\":80.99,\"Quantity\":10,\"SupplierId\":13},{\"Id\":75,\"Name\":\"Front Left Side Outer door handle\",\"Price\":999.99,\"Quantity\":10,\"SupplierId\":12},{\"Id\":76,\"Name\":\"Front Right Outer door handle\",\"Price\":999.99,\"Quantity\":345,\"SupplierId\":12},{\"Id\":77,\"Name\":\"Welded assembly\",\"Price\":1020.99,\"Quantity\":10,\"SupplierId\":13},{\"Id\":78,\"Name\":\"Valance\",\"Price\":1002.99,\"Quantity\":10,\"SupplierId\":22},{\"Id\":79,\"Name\":\"Trunk/boot/hatch\",\"Price\":2200.99,\"Quantity\":300,\"SupplierId\":30},{\"Id\":80,\"Name\":\"Trim package\",\"Price\":1900.99,\"Quantity\":10,\"SupplierId\":15},{\"Id\":81,\"Name\":\"Rims\",\"Price\":4020.99,\"Quantity\":10,\"SupplierId\":31},{\"Id\":82,\"Name\":\"Spoiler\",\"Price\":3000.99,\"Quantity\":10,\"SupplierId\":12},{\"Id\":83,\"Name\":\"Roof rack\",\"Price\":100.99,\"Quantity\":10,\"SupplierId\":1},{\"Id\":84,\"Name\":\"Rocker\",\"Price\":100.99,\"Quantity\":10,\"SupplierId\":21},{\"Id\":85,\"Name\":\"Radiator \",\"Price\":100.99,\"Quantity\":10,\"SupplierId\":26},{\"Id\":86,\"Name\":\"Quarter panel\",\"Price\":100.99,\"Quantity\":200,\"SupplierId\":12},{\"Id\":87,\"Name\":\"Pillar\",\"Price\":100.99,\"Quantity\":10,\"SupplierId\":31},{\"Id\":88,\"Name\":\"Grille\",\"Price\":144.99,\"Quantity\":10,\"SupplierId\":12},{\"Id\":89,\"Name\":\"Front fascia\",\"Price\":11.99,\"Quantity\":10,\"SupplierId\":11},{\"Id\":90,\"Name\":\"Front clip\",\"Price\":100.0,\"Quantity\":10,\"SupplierId\":11},{\"Id\":91,\"Name\":\"Fender\",\"Price\":10.34,\"Quantity\":10,\"SupplierId\":16},{\"Id\":92,\"Name\":\"Fascia\",\"Price\":100.34,\"Quantity\":10,\"SupplierId\":18},{\"Id\":93,\"Name\":\"Decklid\",\"Price\":1060.34,\"Quantity\":11,\"SupplierId\":19},{\"Id\":94,\"Name\":\"Cowl screen\",\"Price\":1500.34,\"Quantity\":10,\"SupplierId\":22},{\"Id\":95,\"Name\":\"Exposed bumper\",\"Price\":1400.34,\"Quantity\":10,\"SupplierId\":13},{\"Id\":96,\"Name\":\"Unexposed bumper\",\"Price\":1003.34,\"Quantity\":10,\"SupplierId\":12},{\"Id\":97,\"Name\":\"Front Right Side Window motor\",\"Price\":44.99,\"Quantity\":10,\"SupplierId\":13},{\"Id\":98,\"Name\":\"Engine block\",\"Price\":40.99,\"Quantity\":10,\"SupplierId\":1},{\"Id\":99,\"Name\":\"Front Left Side Window motor\",\"Price\":56.99,\"Quantity\":10,\"SupplierId\":17},{\"Id\":100,\"Name\":\"Rear Left Side Window motor\",\"Price\":144.99,\"Quantity\":10,\"SupplierId\":19},{\"Id\":101,\"Name\":\"Distributor cap\",\"Price\":9.99,\"Quantity\":10,\"SupplierId\":1},{\"Id\":102,\"Name\":\"Distributor\",\"Price\":7.99,\"Quantity\":10,\"SupplierId\":4},{\"Id\":103,\"Name\":\"Cylinder head\",\"Price\":6.99,\"Quantity\":10,\"SupplierId\":5},{\"Id\":104,\"Name\":\"Crankshaft\",\"Price\":4.99,\"Quantity\":10,\"SupplierId\":6},{\"Id\":105,\"Name\":\"Crank pulley\",\"Price\":2.8,\"Quantity\":10,\"SupplierId\":9},{\"Id\":106,\"Name\":\"Crank case\",\"Price\":2.93,\"Quantity\":10,\"SupplierId\":11},{\"Id\":107,\"Name\":\"Camshaft\",\"Price\":100.98,\"Quantity\":10,\"SupplierId\":12},{\"Id\":108,\"Name\":\"Engine\",\"Price\":100.99,\"Quantity\":10,\"SupplierId\":12},{\"Id\":109,\"Name\":\"Window seal\",\"Price\":100.99,\"Quantity\":10,\"SupplierId\":31},{\"Id\":110,\"Name\":\"Windshield\",\"Price\":100.99,\"Quantity\":10,\"SupplierId\":14},{\"Id\":111,\"Name\":\"Window regulator\",\"Price\":100.99,\"Quantity\":10,\"SupplierId\":21},{\"Id\":112,\"Name\":\"Window motor\",\"Price\":123.49,\"Quantity\":10,\"SupplierId\":13},{\"Id\":113,\"Name\":\"Sunroof Glass\",\"Price\":205.24,\"Quantity\":10,\"SupplierId\":22},{\"Id\":114,\"Name\":\"Sunroof Rail\",\"Price\":100.25,\"Quantity\":10,\"SupplierId\":31},{\"Id\":115,\"Name\":\"Sunroof\",\"Price\":103.43,\"Quantity\":10,\"SupplierId\":12},{\"Id\":116,\"Name\":\"Rear Left Quarter Glass\",\"Price\":100.55,\"Quantity\":10,\"SupplierId\":11},{\"Id\":117,\"Name\":\"Rear Right Quarter Glass\",\"Price\":100.66,\"Quantity\":10,\"SupplierId\":12},{\"Id\":118,\"Name\":\"Rear Left Side Door Glass\",\"Price\":100.66,\"Quantity\":10,\"SupplierId\":15},{\"Id\":119,\"Name\":\"Rear Right Side Door Glass\",\"Price\":100.9,\"Quantity\":10,\"SupplierId\":16},{\"Id\":120,\"Name\":\"Front Left Side Door Glass\",\"Price\":100.92,\"Quantity\":10,\"SupplierId\":19},{\"Id\":121,\"Name\":\"Front Right Side Door Glass\",\"Price\":100.91,\"Quantity\":10,\"SupplierId\":16},{\"Id\":122,\"Name\":\"Fuel tank\",\"Price\":100.98,\"Quantity\":10,\"SupplierId\":13},{\"Id\":123,\"Name\":\"Center-locking\",\"Price\":313.99,\"Quantity\":10,\"SupplierId\":12},{\"Id\":124,\"Name\":\"Door lock and power door locks\",\"Price\":120.99,\"Quantity\":10,\"SupplierId\":12},{\"Id\":125,\"Name\":\"Door latch\",\"Price\":2331.99,\"Quantity\":10,\"SupplierId\":13},{\"Id\":126,\"Name\":\"Hinge\",\"Price\":11.99,\"Quantity\":10,\"SupplierId\":14},{\"Id\":127,\"Name\":\"Door water-shield\",\"Price\":123.99,\"Quantity\":10,\"SupplierId\":15},{\"Id\":128,\"Name\":\"Door seal\",\"Price\":444.99,\"Quantity\":10,\"SupplierId\":16},{\"Id\":129,\"Name\":\"Door control module\",\"Price\":421.99,\"Quantity\":10,\"SupplierId\":17},{\"Id\":130,\"Name\":\"Rear Right Side Window motor\",\"Price\":22.99,\"Quantity\":10,\"SupplierId\":19},{\"Id\":131,\"Name\":\"Clutch shoe\",\"Price\":10.49,\"Quantity\":10,\"SupplierId\":2}],\"Sale\":[{\"Id\":1,\"Discount\":30.0,\"CarId\":14,\"CustomerId\":4},{\"Id\":2,\"Discount\":0.0,\"CarId\":26,\"CustomerId\":3},{\"Id\":3,\"Discount\":10.0,\"CarId\":34,\"CustomerId\":1},{\"Id\":4,\"Discount\":0.0,\"CarId\":2,\"CustomerId\":5},{\"Id\":5,\"Discount\":50.0,\"CarId\":13,\"CustomerId\":27},{\"Id\":6,\"Discount\":30.0,\"CarId\":24,\"CustomerId\":9},{\"Id\":7,\"Discount\":15.0,\"CarId\":25,\"CustomerId\":2},{\"Id\":8,\"Discount\":20.0,\"CarId\":17,\"CustomerId\":22},{\"Id\":9,\"Discount\":15.0,\"CarId\":16,\"CustomerId\":21}],\"Supplier\":[{\"Id\":1,\"Name\":\"3M Company\",\"IsImporter\":true},{\"Id\":2,\"Name\":\"VF Corporation\",\"IsImporter\":false},{\"Id\":3,\"Name\":\"Textron Inc\",\"IsImporter\":true},{\"Id\":4,\"Name\":\"Sunoco Inc.\",\"IsImporter\":true},{\"Id\":5,\"Name\":\"Saks Inc\",\"IsImporter\":false},{\"Id\":6,\"Name\":\"Paychex Inc\",\"IsImporter\":true},{\"Id\":7,\"Name\":\"Olin Corp.\",\"IsImporter\":true},{\"Id\":8,\"Name\":\"Nicor Inc\",\"IsImporter\":false},{\"Id\":9,\"Name\":\"Merck & Co., Inc.\",\"IsImporter\":true},{\"Id\":10,\"Name\":\"Level 3 Communications Inc.\",\"IsImporter\":false},{\"Id\":11,\"Name\":\"IDT Corporation\",\"IsImporter\":true},{\"Id\":12,\"Name\":\"GenCorp Inc.\",\"IsImporter\":false},{\"Id\":13,\"Name\":\"Emcor Group Inc.\",\"IsImporter\":true},{\"Id\":14,\"Name\":\"E*Trade Group, Inc.\",\"IsImporter\":true},{\"Id\":15,\"Name\":\"Wyeth\",\"IsImporter\":true},{\"Id\":16,\"Name\":\"E.I. Du Pont de Nemours and Company\",\"IsImporter\":false},{\"Id\":17,\"Name\":\"The Clorox Co.\",\"IsImporter\":false},{\"Id\":18,\"Name\":\"CMGI Inc.\",\"IsImporter\":true},{\"Id\":19,\"Name\":\"CNF Inc.\",\"IsImporter\":true},{\"Id\":20,\"Name\":\"Cintas Corp.\",\"IsImporter\":false},{\"Id\":21,\"Name\":\"Chubb Corp\",\"IsImporter\":true},{\"Id\":22,\"Name\":\"Cintas Corp.\",\"IsImporter\":false},{\"Id\":23,\"Name\":\"Casey\'s General Stores Inc.\",\"IsImporter\":true},{\"Id\":24,\"Name\":\"Caterpillar Inc.\",\"IsImporter\":false},{\"Id\":25,\"Name\":\"Big Lots, Inc.\",\"IsImporter\":true},{\"Id\":26,\"Name\":\"Atmel Corporation\",\"IsImporter\":true},{\"Id\":27,\"Name\":\"Airgas, Inc.\",\"IsImporter\":false},{\"Id\":28,\"Name\":\"Anthem, Inc.\",\"IsImporter\":true},{\"Id\":29,\"Name\":\"Agway Inc.\",\"IsImporter\":false},{\"Id\":30,\"Name\":\"Danaher Corporation\",\"IsImporter\":true},{\"Id\":31,\"Name\":\"Zale\",\"IsImporter\":false}],\"PartCar\":[{\"PartId\":2,\"CarId\":17},{\"PartId\":2,\"CarId\":36},{\"PartId\":4,\"CarId\":3},{\"PartId\":4,\"CarId\":7},{\"PartId\":4,\"CarId\":15},{\"PartId\":5,\"CarId\":15},{\"PartId\":5,\"CarId\":19},{\"PartId\":5,\"CarId\":35},{\"PartId\":5,\"CarId\":36},{\"PartId\":6,\"CarId\":4},{\"PartId\":6,\"CarId\":29},{\"PartId\":7,\"CarId\":12},{\"PartId\":7,\"CarId\":16},{\"PartId\":7,\"CarId\":24},{\"PartId\":8,\"CarId\":3},{\"PartId\":8,\"CarId\":22},{\"PartId\":8,\"CarId\":25},{\"PartId\":8,\"CarId\":28},{\"PartId\":9,\"CarId\":15},{\"PartId\":10,\"CarId\":16},{\"PartId\":10,\"CarId\":18},{\"PartId\":10,\"CarId\":28},{\"PartId\":11,\"CarId\":22},{\"PartId\":12,\"CarId\":27},{\"PartId\":12,\"CarId\":32},{\"PartId\":15,\"CarId\":3},{\"PartId\":15,\"CarId\":10},{\"PartId\":15,\"CarId\":26},{\"PartId\":16,\"CarId\":5},{\"PartId\":17,\"CarId\":19},{\"PartId\":18,\"CarId\":15},{\"PartId\":19,\"CarId\":18},{\"PartId\":20,\"CarId\":6},{\"PartId\":20,\"CarId\":22},{\"PartId\":22,\"CarId\":6},{\"PartId\":22,\"CarId\":28},{\"PartId\":22,\"CarId\":36},{\"PartId\":23,\"CarId\":1},{\"PartId\":23,\"CarId\":25},{\"PartId\":23,\"CarId\":35},{\"PartId\":25,\"CarId\":12},{\"PartId\":25,\"CarId\":17},{\"PartId\":27,\"CarId\":28},{\"PartId\":27,\"CarId\":36},{\"PartId\":28,\"CarId\":6},{\"PartId\":29,\"CarId\":15},{\"PartId\":29,\"CarId\":24},{\"PartId\":29,\"CarId\":36},{\"PartId\":30,\"CarId\":6},{\"PartId\":31,\"CarId\":12},{\"PartId\":32,\"CarId\":1},{\"PartId\":33,\"CarId\":6},{\"PartId\":33,\"CarId\":7},{\"PartId\":34,\"CarId\":17},{\"PartId\":34,\"CarId\":31},{\"PartId\":34,\"CarId\":34},{\"PartId\":35,\"CarId\":3},{\"PartId\":35,\"CarId\":16},{\"PartId\":36,\"CarId\":9},{\"PartId\":36,\"CarId\":15},{\"PartId\":36,\"CarId\":22},{\"PartId\":37,\"CarId\":3},{\"PartId\":37,\"CarId\":6},{\"PartId\":38,\"CarId\":1},{\"PartId\":38,\"CarId\":16},{\"PartId\":39,\"CarId\":16},{\"PartId\":39,\"CarId\":20},{\"PartId\":40,\"CarId\":6},{\"PartId\":40,\"CarId\":14},{\"PartId\":40,\"CarId\":24},{\"PartId\":41,\"CarId\":12},{\"PartId\":42,\"CarId\":29},{\"PartId\":42,\"CarId\":36},{\"PartId\":43,\"CarId\":13},{\"PartId\":44,\"CarId\":7},{\"PartId\":44,\"CarId\":11},{\"PartId\":44,\"CarId\":21},{\"PartId\":44,\"CarId\":34},{\"PartId\":45,\"CarId\":16},{\"PartId\":45,\"CarId\":29},{\"PartId\":46,\"CarId\":1},{\"PartId\":46,\"CarId\":3},{\"PartId\":46,\"CarId\":17},{\"PartId\":46,\"CarId\":18},{\"PartId\":46,\"CarId\":25},{\"PartId\":46,\"CarId\":27},{\"PartId\":47,\"CarId\":17},{\"PartId\":47,\"CarId\":19},{\"PartId\":48,\"CarId\":3},{\"PartId\":48,\"CarId\":12},{\"PartId\":48,\"CarId\":21},{\"PartId\":48,\"CarId\":28},{\"PartId\":48,\"CarId\":31},{\"PartId\":49,\"CarId\":33},{\"PartId\":50,\"CarId\":22},{\"PartId\":52,\"CarId\":28},{\"PartId\":54,\"CarId\":22},{\"PartId\":54,\"CarId\":26},{\"PartId\":57,\"CarId\":2},{\"PartId\":57,\"CarId\":12},{\"PartId\":57,\"CarId\":25},{\"PartId\":57,\"CarId\":34},{\"PartId\":58,\"CarId\":2},{\"PartId\":58,\"CarId\":27},{\"PartId\":59,\"CarId\":17},{\"PartId\":59,\"CarId\":28},{\"PartId\":60,\"CarId\":8},{\"PartId\":61,\"CarId\":3},{\"PartId\":61,\"CarId\":12},{\"PartId\":61,\"CarId\":16},{\"PartId\":61,\"CarId\":31},{\"PartId\":62,\"CarId\":20},{\"PartId\":62,\"CarId\":28},{\"PartId\":63,\"CarId\":2},{\"PartId\":65,\"CarId\":6},{\"PartId\":65,\"CarId\":23},{\"PartId\":65,\"CarId\":31},{\"PartId\":66,\"CarId\":29},{\"PartId\":67,\"CarId\":25},{\"PartId\":67,\"CarId\":34},{\"PartId\":68,\"CarId\":1},{\"PartId\":68,\"CarId\":24},{\"PartId\":68,\"CarId\":32},{\"PartId\":69,\"CarId\":7},{\"PartId\":69,\"CarId\":15},{\"PartId\":70,\"CarId\":25},{\"PartId\":71,\"CarId\":1},{\"PartId\":71,\"CarId\":6},{\"PartId\":71,\"CarId\":25},{\"PartId\":72,\"CarId\":3},{\"PartId\":72,\"CarId\":10},{\"PartId\":72,\"CarId\":20},{\"PartId\":72,\"CarId\":22},{\"PartId\":73,\"CarId\":24},{\"PartId\":74,\"CarId\":9},{\"PartId\":75,\"CarId\":22},{\"PartId\":78,\"CarId\":8},{\"PartId\":78,\"CarId\":31},{\"PartId\":78,\"CarId\":35},{\"PartId\":79,\"CarId\":4},{\"PartId\":79,\"CarId\":10},{\"PartId\":79,\"CarId\":17},{\"PartId\":79,\"CarId\":25},{\"PartId\":82,\"CarId\":2},{\"PartId\":82,\"CarId\":6},{\"PartId\":83,\"CarId\":15},{\"PartId\":83,\"CarId\":16},{\"PartId\":84,\"CarId\":16},{\"PartId\":84,\"CarId\":29},{\"PartId\":85,\"CarId\":9},{\"PartId\":88,\"CarId\":1},{\"PartId\":88,\"CarId\":5},{\"PartId\":88,\"CarId\":22},{\"PartId\":88,\"CarId\":34},{\"PartId\":89,\"CarId\":6},{\"PartId\":89,\"CarId\":13},{\"PartId\":89,\"CarId\":14},{\"PartId\":90,\"CarId\":23},{\"PartId\":90,\"CarId\":30},{\"PartId\":91,\"CarId\":5},{\"PartId\":91,\"CarId\":12},{\"PartId\":91,\"CarId\":17},{\"PartId\":93,\"CarId\":31},{\"PartId\":94,\"CarId\":33},{\"PartId\":94,\"CarId\":34},{\"PartId\":95,\"CarId\":12},{\"PartId\":95,\"CarId\":23},{\"PartId\":96,\"CarId\":3},{\"PartId\":96,\"CarId\":29},{\"PartId\":96,\"CarId\":31},{\"PartId\":97,\"CarId\":17},{\"PartId\":98,\"CarId\":15},{\"PartId\":98,\"CarId\":30},{\"PartId\":99,\"CarId\":12},{\"PartId\":99,\"CarId\":17},{\"PartId\":99,\"CarId\":28},{\"PartId\":99,\"CarId\":34},{\"PartId\":100,\"CarId\":2},{\"PartId\":100,\"CarId\":8},{\"PartId\":100,\"CarId\":34},{\"PartId\":101,\"CarId\":12},{\"PartId\":102,\"CarId\":1},{\"PartId\":102,\"CarId\":24},{\"PartId\":102,\"CarId\":25},{\"PartId\":102,\"CarId\":26},{\"PartId\":103,\"CarId\":11},{\"PartId\":103,\"CarId\":14},{\"PartId\":103,\"CarId\":25},{\"PartId\":103,\"CarId\":32},{\"PartId\":103,\"CarId\":34},{\"PartId\":104,\"CarId\":1},{\"PartId\":104,\"CarId\":7},{\"PartId\":104,\"CarId\":13},{\"PartId\":106,\"CarId\":16},{\"PartId\":107,\"CarId\":4},{\"PartId\":107,\"CarId\":33},{\"PartId\":108,\"CarId\":2},{\"PartId\":108,\"CarId\":25},{\"PartId\":108,\"CarId\":31},{\"PartId\":109,\"CarId\":3},{\"PartId\":111,\"CarId\":31},{\"PartId\":111,\"CarId\":34},{\"PartId\":112,\"CarId\":21},{\"PartId\":112,\"CarId\":28},{\"PartId\":113,\"CarId\":30},{\"PartId\":113,\"CarId\":34},{\"PartId\":114,\"CarId\":1},{\"PartId\":114,\"CarId\":11},{\"PartId\":114,\"CarId\":15},{\"PartId\":114,\"CarId\":22},{\"PartId\":115,\"CarId\":7},{\"PartId\":115,\"CarId\":17},{\"PartId\":115,\"CarId\":22},{\"PartId\":116,\"CarId\":1},{\"PartId\":116,\"CarId\":15},{\"PartId\":117,\"CarId\":31}]}";

            var datasets = JsonConvert.DeserializeObject<Dictionary<string, IEnumerable<JObject>>>(datasetsJson);

            foreach (var dataset in datasets)
            {
                var entityType = GetType(dataset.Key);
                var entities = dataset.Value
                    .Select(j => j.ToObject(entityType))
                    .ToArray();

                context.AddRange(entities);
            }

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
