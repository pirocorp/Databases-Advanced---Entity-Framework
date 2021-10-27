namespace CarDealer
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using AutoMapper;
    using Data;
    using DTO;
    using Models;
    using Newtonsoft.Json;

    public static class StartUp
    {
        public static void Main(string[] args)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CarDealerProfile>();
            });

            var mapper = config.CreateMapper();
            var context = new CarDealerContext();
            var jsonString = File.ReadAllText("./Datasets/sales.json");

            var result = GetTotalSalesByCustomer(context);
            Console.WriteLine(result);
        }

        public static string ImportSuppliers(CarDealerContext context, string inputJson)
        {
            var suppliers = JsonConvert
                .DeserializeObject<Supplier[]>(inputJson);

            context.Suppliers.AddRange(suppliers);
            context.SaveChanges();

            return $"Successfully imported {context.Suppliers.Count()}.";
        }

        public static string ImportParts(CarDealerContext context, string inputJson)
        {
            var parts = JsonConvert
                .DeserializeObject<Part[]>(inputJson);

            foreach (var part in parts)
            {
                if (context.Suppliers.Any(s => s.Id == part.SupplierId))
                {
                    context.Parts.Add(part);
                    context.SaveChanges();
                }
            }

            return $"Successfully imported {context.Parts.Count()}.";
        }

        public static string ImportCars(CarDealerContext context, string inputJson)
        {
            var cars  = JsonConvert
                .DeserializeObject<CarDTO[]>(inputJson);

            foreach (var carDto in cars)
            {
                var car = new Car()
                {
                    Model = carDto.Model,
                    Make = carDto.Make,
                    TravelledDistance = carDto.TravelledDistance
                };

                var partCars = new List<PartCar>();

                foreach (var id in carDto.PartsId.Distinct())
                {
                    var partCar = new PartCar()
                    {
                        Car = car,
                        PartId = id
                    };

                    partCars.Add(partCar);
                }

                context.Cars.Add(car);
                context.PartCars.AddRange(partCars);
                context.SaveChanges();
            }

            return $"Successfully imported {context.Cars.Count()}.";
        }

        public static string ImportCustomers(CarDealerContext context, string inputJson)
        {
            var customers = JsonConvert
                .DeserializeObject<Customer[]>(inputJson);

            context.Customers.AddRange(customers);
            context.SaveChanges();

            return $"Successfully imported {context.Customers.Count()}.";
        }

        public static string ImportSales(CarDealerContext context, string inputJson)
        {
            var sales = JsonConvert
                .DeserializeObject<Sale[]>(inputJson);

            context.Sales.AddRange(sales);
            context.SaveChanges();

            return $"Successfully imported {context.Sales.Count()}.";
        }

        public static string GetOrderedCustomers(CarDealerContext context)
        {
            var customers = context.Customers
                .OrderBy(c => c.BirthDate)
                .ThenBy(c => c.IsYoungDriver)
                .Select(c => new
                {
                    c.Name,
                    BirthDate = c.BirthDate.ToString("dd/MM/yyyy"),
                    c.IsYoungDriver
                })
                .ToArray();

            return JsonConvert.SerializeObject(
                customers, 
                new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented,
                    NullValueHandling = NullValueHandling.Ignore
                });
        }

        public static string GetCarsFromMakeToyota(CarDealerContext context)
        {
            var cars = context.Cars
                .Where(c => c.Make == "Toyota")
                .OrderBy(c => c.Model)
                .ThenByDescending(c => c.TravelledDistance)
                .Select(c => new
                {
                    c.Id,
                    c.Make,
                    c.Model,
                    c.TravelledDistance
                })
                .ToList();

            return JsonConvert.SerializeObject(
                cars, 
                new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented,
                    NullValueHandling = NullValueHandling.Ignore
                });
        }

        public static string GetLocalSuppliers(CarDealerContext context)
        {
            var suppliers = context.Suppliers
                .Where(s => !s.IsImporter)
                .Select(s => new
                {
                    s.Id,
                    s.Name,
                    PartsCount = s.Parts.Count,
                })
                .ToList();

            return JsonConvert.SerializeObject(
                suppliers, 
                new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented,
                    NullValueHandling = NullValueHandling.Ignore
                });
        }

        public static string GetCarsWithTheirListOfParts(CarDealerContext context)
        {
            var cars = context.Cars
                .Select(c => new
                {
                    car = new
                    {
                        c.Make,
                        c.Model,
                        c.TravelledDistance,
                    },
                    parts = c.PartCars
                        .Select(p => new
                        {
                            p.Part.Name,
                            Price = p.Part.Price.ToString("F2")
                        })
                })
                .ToList();

            return JsonConvert.SerializeObject(
                cars, 
                new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented,
                    NullValueHandling = NullValueHandling.Ignore
                });
        }

        public static string GetTotalSalesByCustomer(CarDealerContext context)
        {
            var customers = context.Customers
                .Where(c => c.Sales.Count > 0)
                .Select(c => new
                {
                    fullName = c.Name,
                    boughtCars = c.Sales.Count,
                    spentMoney = c.Sales
                        .Sum(s => (s.Car.PartCars.Select(x => x.Part.Price).Sum()))
                })
                .OrderByDescending(c => c.spentMoney)
                .ThenByDescending(c => c.boughtCars)
                .ToList();

            return JsonConvert.SerializeObject(
                customers, 
                new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented,
                    NullValueHandling = NullValueHandling.Ignore
                });
        }

        public static string GetSalesWithAppliedDiscount(CarDealerContext context)
        {
            //var customers = context.Customers
            //    .Where(c => c.Sales.Count > 0)
            //    .Select(c => new
            //    {
            //        fullName = c.Name,
            //        boughtCars = c.Sales.Count,
            //        spentMoney = c.Sales
            //            .Sum(s => (s.Car.PartCars.Select(x => x.Part.Price).Sum()) * ((100 - s.Discount) / 100))
            //    })
            //    .OrderByDescending(c => c.spentMoney)
            //    .ThenByDescending(c => c.boughtCars)
            //    .ToList();

            var sales = context.Sales
                .Take(10)
                .Select(s => new
                {
                    car = new
                    {
                        s.Car.Make,
                        s.Car.Model,
                        s.Car.TravelledDistance
                    },
                    customerName = s.Customer.Name,
                    Discount = s.Discount.ToString("F2"),
                    price = s.Car.PartCars.Select(pc => pc.Part.Price).Sum().ToString("F2"),
                    priceWithDiscount = 
                        (s.Car.PartCars.Select(pc => pc.Part.Price).Sum() 
                        * ((100 - s.Discount) / 100))
                        .ToString("F2")
                })
                .ToList();

            return JsonConvert.SerializeObject(
                sales, 
                new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented,
                    NullValueHandling = NullValueHandling.Ignore
                });
        }
    }
}