namespace CarDealer
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml;
    using System.Xml.Serialization;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Data;
    using Dtos.Export;
    using Dtos.Import;
    using Models;
    using DataAnnotations = System.ComponentModel.DataAnnotations;
    using PartDto = Dtos.Import.PartDto;

    public class StartUp
    {
        public static void Main()
        {
            var context = new CarDealerContext();

            //var xmlString = File.ReadAllText("./Datasets/sales.xml");

            var result = GetTotalSalesByCustomer(context);
            Console.WriteLine(result);
        }

        public static string ImportSuppliers(CarDealerContext context, string inputXml)
        {
            var supplierDtos = DeserializeXml<SupplierDto>("Suppliers", inputXml);

            SaveDataToDatabase<SupplierDto, Supplier>(context, supplierDtos);

            return $"Successfully imported {context.Suppliers.Count()}";
        }

        public static string ImportParts(CarDealerContext context, string inputXml)
        {
            var partDtos = DeserializeXml<PartDto>("Parts", inputXml)
                .Where(d => context.Suppliers.Any(s => s.Id == d.SupplierId))
                .ToArray();

            SaveDataToDatabase<PartDto, Part>(context, partDtos);

            return $"Successfully imported {context.Parts.Count()}";
        }

        public static string ImportCars(CarDealerContext context, string inputXml)
        {
            var carsDtos = DeserializeXml<CarDto>("Cars", inputXml);

            var cars = new List<Car>();
            var carParts = new List<PartCar>();

            foreach (var carDto in carsDtos)
            {
                var mapper = GetMapper();
                var car = mapper.Map<Car>(carDto);
                cars.Add(car);

                carDto.Parts
                    .Select(p => p.Id)
                    .Distinct()
                    .Where(d => context.Parts.Any(p => p.Id == d))
                    .ToList()
                    .ForEach(d => carParts.Add(new PartCar()
                    {
                        Car = car,
                        PartId = d
                    }));
            }

            context.Cars.AddRange(cars);
            context.PartCars.AddRange(carParts);
            context.SaveChanges();

            return $"Successfully imported {context.Cars.Count()}";
        }

        public static string ImportCustomers(CarDealerContext context, string inputXml)
        {
            var customersDtos = DeserializeXml<CustomerDto>("Customers", inputXml);

            SaveDataToDatabase<CustomerDto, Customer>(context, customersDtos);

            return $"Successfully imported {context.Customers.Count()}";
        }

        public static string ImportSales(CarDealerContext context, string inputXml)
        {
            var salesDtos = DeserializeXml<SaleDto>("Sales", inputXml)
                .Where(saleDto
                    => context.Cars.Any(car => car.Id == saleDto.CarId))
                .ToList();

            SaveDataToDatabase<SaleDto, Sale>(context, salesDtos);

            return $"Successfully imported {context.Sales.Count()}";
        }

        public static string GetCarsWithDistance(CarDealerContext context)
        {
            var mapper = GetMapper();

            var cars = context.Cars
                .Where(c => c.TravelledDistance > 2_000_000)
                .OrderBy(c => c.Make)
                .ThenBy(c => c.Model)
                .Take(10)
                .ProjectTo<CarWithTheirPartsDto>(mapper.ConfigurationProvider)
                .ToArray();

            return SerializeXml("cars", cars);
        }

        public static string GetCarsFromMakeBmw(CarDealerContext context)
        {
            var mapper = GetMapper();

            var cars = context.Cars
                .Where(c => c.Make == "BMW")
                .OrderBy(c => c.Model)
                .ThenByDescending(c => c.TravelledDistance)
                .ProjectTo<BmwCarDto>(mapper.ConfigurationProvider)
                .ToList();

            return SerializeXml("cars", cars);
        }

        public static string GetLocalSuppliers(CarDealerContext context)
        {
            var mapper = GetMapper();

            var suppliers = context.Suppliers
                .Where(c => !c.IsImporter)
                .ProjectTo<LocalSupplierDto>(mapper.ConfigurationProvider)
                .ToList();

            return SerializeXml("suppliers", suppliers);
        }

        public static string GetCarsWithTheirListOfParts(CarDealerContext context)
        {
            var mapper = GetMapper();

            var cars = context.Cars
                .OrderByDescending(c => c.TravelledDistance)
                .ThenBy(c => c.Model)
                .Take(5)
                .ProjectTo<CarWithPartsDto>(mapper.ConfigurationProvider)
                .ToList();

            return SerializeXml("cars", cars);
        }

        public static string GetTotalSalesByCustomer(CarDealerContext context)
        {
            var mapper = GetMapper();

            var customers = context.Customers
                .Where(c => c.Sales.Count > 0)
                .ProjectTo<TotalSalesByCustomerDto>(mapper.ConfigurationProvider)
                .ToArray()
                .OrderByDescending(c => c.SpendMoney)
                .ToArray();

            return SerializeXml("customers", customers);
        }

        public static string GetSalesWithAppliedDiscount(CarDealerContext context)
        {
            var mapper = GetMapper();

            var sales = context.Sales
                .ProjectTo<SaleWithDiscountDto>(mapper.ConfigurationProvider)
                .ToArray();

            return SerializeXml("sales", sales);
        }

        private static string SerializeXml<TDto>(string rootAttribute, IEnumerable<TDto> elements)
        {
            var serializer = new XmlSerializer(typeof(TDto[]),
                new XmlRootAttribute(rootAttribute));

            var xmlNamespaces = new XmlSerializerNamespaces(new []{XmlQualifiedName.Empty});

            StringWriter writer;
            using (writer = new StringWriter())
            {
                serializer.Serialize(writer, elements.ToArray(), xmlNamespaces);
            }

            return writer.ToString();
        }

        private static string SerializeXml<TDto>(string rootAttribute, TDto element)
        {
            var serializer = new XmlSerializer(typeof(TDto),
                new XmlRootAttribute(rootAttribute));

            var xmlNamespaces = new XmlSerializerNamespaces(new []{XmlQualifiedName.Empty});

            StringWriter writer;
            using (writer = new StringWriter())
            {
                serializer.Serialize(writer, element, xmlNamespaces);
            }

            return writer.ToString();
        }

        private static IEnumerable<TDto> DeserializeXml<TDto>(string rootAttribute, string inputXml)
        {
            var serializer = new XmlSerializer(typeof(TDto[]),
                new XmlRootAttribute(rootAttribute));

            var userDtos = (TDto[])serializer.Deserialize(new StringReader(inputXml));
            return userDtos;
        }

        private static void SaveDataToDatabase<TDto, TEntity>(
            CarDealerContext context, 
            IEnumerable<TDto> userDtos) 
            where TEntity : class
        {
            var entities = new List<TEntity>();
            var mapper = GetMapper();

            foreach (var userDto in userDtos)
            {
                if (!IsValid(userDto))
                {
                    continue;
                }

                var entity = mapper.Map<TEntity>(userDto);
                entities.Add(entity);
            }

            context.Set<TEntity>().AddRange(entities);
            context.SaveChanges();
        }

        private static IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CarDealerProfile>();
            });

            return config.CreateMapper();
        }

        private static bool IsValid(object obj)
        {
            var validationContext = new DataAnnotations.ValidationContext(obj);
            var validationResults = new List<DataAnnotations.ValidationResult>();

            return DataAnnotations.Validator.TryValidateObject(obj, validationContext, validationResults, true);
        }
    }
}