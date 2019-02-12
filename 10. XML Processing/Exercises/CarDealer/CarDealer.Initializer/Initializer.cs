namespace CarDealer.Initializer
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Xml.Serialization;
    using System.Linq;
    using DataAnnotations = System.ComponentModel.DataAnnotations;

    using Microsoft.EntityFrameworkCore;

    using AutoMapper;

    using Data;
    using Dtos;
    using Models;
    using Profiles;

    public class Initializer
    {
        private IMapper mapper;

        public Initializer()
        {
            this.InitializeMapper();
        }

        public void Initialize()
        {
            using (var context = new CarDealerContext())
            {
                context.Database.Migrate();

                if (!context.Suppliers.Any())
                {
                    this.ImportSuppliers();
                }

                if (!context.Parts.Any())
                {
                    this.ImportParts();
                }

                if (!context.Cars.Any())
                {
                    this.ImportCars();
                }

                if (!context.Customers.Any())
                {
                    this.ImportCustomers();
                }

                if (!context.Sales.Any())
                {
                    this.ImportSales();
                }
            }
        }

        private void ImportSales()
        {
            var discounts = this.GetDiscounts();
            List<Car> cars;
            List<Customer> customers;

            using (var context = new CarDealerContext())
            {
                cars = context.Cars.ToList();
                customers = context.Customers.ToList();
            }

            var random = new Random();
            var sales = new List<Sale>(customers.Count);

            foreach (var customer in customers)
            {
                var currentCarRandomIndex = random.Next(0, cars.Count);
                var currentCar = cars[currentCarRandomIndex];
                cars.RemoveAt(currentCarRandomIndex);

                var sale = new Sale()
                {
                    Discount = discounts[random.Next(0, discounts.Length)],
                    CarId = currentCar.Id,
                    CustomerId = customer.Id
                };

                sales.Add(sale);
            }

            using (var context = new CarDealerContext())
            {
                context.Sales.AddRange(sales);
                context.SaveChanges();
            }
        }

        private void ImportCustomers()
        {
            var xmlString = File.ReadAllText("../../../Xml/customers.xml");

            var serializer = new XmlSerializer(typeof(CustomerDto[]), new XmlRootAttribute("customers"));
            var customerDtos = (object[]) serializer.Deserialize(new StringReader(xmlString));
            var customers = this.FilterValidOnlyEntities<Customer>(customerDtos);

            using (var context = new CarDealerContext())
            {
                context.Customers.AddRange(customers);
                context.SaveChanges();
            }
        }

        private void ImportCars()
        {
            var xmlString = File.ReadAllText("../../../Xml/cars.xml");

            var serializer = new XmlSerializer(typeof(CarDto[]), new XmlRootAttribute("cars"));
            var carDtos = (object[]) serializer.Deserialize(new StringReader(xmlString));
            var cars = this.FilterValidOnlyEntities<Car>(carDtos);

            Part[] parts;
            using (var context = new CarDealerContext())
            {
                parts = context.Parts.ToArray();
                context.Cars.AddRange(cars);
                context.SaveChanges();
            }

            var partCars = new List<PartCar>();

            foreach (var car in cars)
            {
                var currentPartsIds = this.GetRandomPartsIds(parts);
                var currentCarId = car.Id;
                var currentPartCars = this.GetPartCars(currentCarId, currentPartsIds);
                partCars.AddRange(currentPartCars);
            }

            using (var context = new CarDealerContext())
            {
                context.PartCars.AddRange(partCars);
                context.SaveChanges();
            }
        }

        private void ImportParts()
        {
            var xmlString = File.ReadAllText("../../../Xml/parts.xml");

            var serializer = new XmlSerializer(typeof(PartDto[]), new XmlRootAttribute("parts"));
            var partDtos = (object[]) serializer.Deserialize(new StringReader(xmlString));
            var parts = this.FilterValidOnlyEntities<Part>(partDtos);

            int[] suppliersIds;
            using (var context = new CarDealerContext())
            {
                suppliersIds = context.Suppliers
                    .Select(s => s.Id)
                    .ToArray();
            }

            var random = new Random();

            foreach (var part in parts)
            {
                part.SupplierId = suppliersIds[random.Next(0, suppliersIds.Length)];
            }

            using (var context = new CarDealerContext())
            {
                context.Parts.AddRange(parts);
                context.SaveChanges();
            }
        }

        private void ImportSuppliers()
        {
            var xmlString = File.ReadAllText("../../../Xml/suppliers.xml");

            var serializer = new XmlSerializer(typeof(SupplierDto[]), new XmlRootAttribute("suppliers"));
            var suppliersDtos = (object[]) serializer.Deserialize(new StringReader(xmlString));
            var suppliers = this.FilterValidOnlyEntities<Supplier>(suppliersDtos);

            using (var context = new CarDealerContext())
            {
                context.Suppliers.AddRange(suppliers);
                context.SaveChanges();
            }
        }

        private TOutput[] FilterValidOnlyEntities<TOutput> (object[] elements)
        {
            var result = new List<TOutput>();

            for (var i = 0; i < elements.Length; i++)
            {
                var currentElement = elements[i];

                if (!IsValid(currentElement))
                {
                    continue;
                }

                var currentEntity = this.mapper.Map<TOutput>(currentElement);

                result.Add(currentEntity);
            }

            return result.ToArray();
        }

        private void InitializeMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<SupplierProfile>();
                cfg.AddProfile<PartProfile>();
                cfg.AddProfile<CarProfile>();
                cfg.AddProfile<CustomerProfile>();
            });

            this.mapper = config.CreateMapper();
        }

        private static bool IsValid(object obj)
        {
            var validationContext = new DataAnnotations.ValidationContext(obj);
            var validationResults = new List<DataAnnotations.ValidationResult>();

            return DataAnnotations.Validator.TryValidateObject(obj, validationContext, validationResults, true);
        }

        private int[] GetRandomPartsIds(Part[] parts)
        {
            var random = new Random();

            var partsCount = 20;

            var partsIds = new int[partsCount];

            for (var i = 0; i < partsIds.Length; i++)
            {
                var currentPartRandomIndex = random.Next(0, parts.Length);
                partsIds[i] = parts[currentPartRandomIndex].Id;
            }

            return partsIds.Distinct().ToArray();
        }

        private List<PartCar> GetPartCars(int currentCarId, int[] currentPartsIds)
        {
            var partCars = new List<PartCar>(currentPartsIds.Length);

            foreach (var currentPartsId in currentPartsIds)
            {
                var currentPartCar = new PartCar()
                {
                    CarId = currentCarId,
                    PartId = currentPartsId
                };

                partCars.Add(currentPartCar);
            }

            return partCars;
        }

        private int[] GetDiscounts()
        {
            return new[] {0, 5, 10, 15, 20, 30, 40, 50};
        }
    }
}