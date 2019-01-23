namespace Cars.App
{
    using System;
    using System.Linq;

    using Microsoft.EntityFrameworkCore;

    using Data;
    using Data.Models;

    public class Startup
    {
        public static void Main()
        {
            var context = new CarsDbContext();

            //ResetDatabase(context);
            var cars = context.Cars
                .Include(c => c.Engine)
                .Include(c => c.Make)
                .Include(c => c.LicensePlate)
                .Include(c => c.CarDealerships)
                .ThenInclude(cd => cd.Dealership)
                .ToArray();

            var plates = context.LicensePlates.ToArray();

            var rnd = new Random();

            for (var i = 0; i < 3; i++)
            {

                var rndIndex = rnd.Next(3);
                cars[i].LicensePlate = plates[rndIndex];
            }

            context.SaveChanges();

            foreach (var car in cars)
            {
                
                Console.WriteLine($"{car.Make.Name} {car.Model}");
                Console.WriteLine($"--Fuel: {car.Engine.FuelType}");
                Console.WriteLine($"--Transmission: {car.Transmission}");
                Console.WriteLine($"--Plate number: {car.LicensePlate?.Number??"No Plate"}");
                Console.WriteLine($"--Dealerships:");
                Console.WriteLine(string.Join(Environment.NewLine, 
                    car.CarDealerships.Select(x => $"----{x.Dealership.Name}")));
            }

            Console.WriteLine();
        }

        private static void ResetDatabase(CarsDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            Seed(context);
        }

        private static void Seed(CarsDbContext context)
        {
            var makes = new[]
            {
                new Make() {Name = "Ford"},
                new Make() {Name = "Mercedes"},
                new Make() {Name = "Audi"},
                new Make() {Name = "BMW"},
                new Make() {Name = "Toyota"},
                new Make() {Name = "Lexus"},
                new Make() {Name = "Opel"},
            };

            var engines = new []
            {
                new Engine(){Capacity = 1.6, Pistons = 4, FuelType = FuelType.Petrol, Horsepower = 90},
                new Engine(){Capacity = 2.5, Pistons = 4, FuelType = FuelType.Diesel, Horsepower = 190},
                new Engine(){Capacity = 4.5, Pistons = 6, FuelType = FuelType.Petrol, Horsepower = 290},
            };

            var cars = new[]
            {
                new Car()
                {
                    Engine = engines[2],
                    Make = makes[1],
                    Doors = 4,
                    Model = "AMG S500",
                    ProductionYear = new DateTime(2019, 1, 1),
                    Transmission = Transmission.Automatic,
                },

                new Car()
                {
                    Engine = engines[1],
                    Make = makes[6],
                    Doors = 5,
                    Model = "Astra",
                    ProductionYear = new DateTime(2018, 1, 1),
                    Transmission = Transmission.Manual,
                },

                new Car()
                {
                    Engine = engines[0],
                    Make = makes[5],
                    Doors = 4,
                    Model = "IS 600H",
                    ProductionYear = new DateTime(2019, 1, 1),
                    Transmission = Transmission.Automatic,
                },
            };
            context.Cars.AddRange(cars);

            var dealerships = new[]
            {
                new Dealership()
                {
                    Name = "SoftUni - Autos",
                },

                new Dealership()
                {
                    Name = "Fast and Furious Autos",
                },
            };
            context.Dealerships.AddRange(dealerships);
            
            var carDealerships = new[]
            {
                new CarDealership()
                {
                    Car = cars[0],
                    Dealership = dealerships[0]
                },

                new CarDealership()
                {
                    Car = cars[1],
                    Dealership = dealerships[1]
                },

                new CarDealership()
                {
                    Car = cars[0],
                    Dealership = dealerships[1]
                },

                new CarDealership()
                {
                    Car = cars[2],
                    Dealership = dealerships[0]
                },
            };
            context.CarsDealerships.AddRange(carDealerships);

            var licensePlates = new[]
            {
                new LicensePlate()
                {
                    Number = "CB1234AX"
                },

                new LicensePlate()
                {
                    Number = "B1224AX"
                },

                new LicensePlate()
                {
                    Number = "H1178AH"
                },
            };
            context.LicensePlates.AddRange(licensePlates);

            context.SaveChanges();
        }
    }
}