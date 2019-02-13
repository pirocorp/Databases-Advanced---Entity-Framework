namespace CarDealer.App
{
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using Data;
    using Dtos;
    using Dtos.CarsWithPartsDtos;
    using Newtonsoft.Json;
    using Profiles;
    using Formatting = Newtonsoft.Json.Formatting;

    public class ExportData
    {
        private IMapper mapper;

        public ExportData()
        {
            this.InitializeMapper();
        }

        public void SalesWithAppliedDiscount()
        {
            SaleDto[] saleDtos;

            using (var context = new CarDealerContext())
            {
                saleDtos = context.Sales
                    .ProjectTo<SaleDto>(this.mapper.ConfigurationProvider)
                    .ToArray();
            }

            Serialize(saleDtos, new XmlRootAttribute("sales"), "../../../Output/Xml/sales-discounts.xml");
            var jsonObjects = JsonConvert.SerializeObject(saleDtos, new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            });
            File.WriteAllText("../../../Output/Json/sales-discounts.json", jsonObjects);
        }

        public void TotalSalesByCustomer()
        {
            CustomerDto[] customerDtos;

            using (var context = new CarDealerContext())
            {
                customerDtos = context.Customers
                    .Where(c => c.Sales.Count > 0)
                    .ProjectTo<CustomerDto>(this.mapper.ConfigurationProvider)
                    .OrderByDescending(c => c.SpentMoney)
                    .ThenByDescending(c => c.BoughtCars)
                    .ToArray();
            }

            Serialize(customerDtos, new XmlRootAttribute("customers"), "../../../Output/Xml/customers-total-sales.xml");
            var jsonObjects = JsonConvert.SerializeObject(customerDtos, new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            });
            File.WriteAllText("../../../Output/Json/customers-total-sales.json", jsonObjects);
        }

        public void CarsWithTheirListOfParts()
        {
            CarWithTheirPartsDto[] carDtos;

            using (var context = new CarDealerContext())
            {
                carDtos = context.Cars
                    .ProjectTo<CarWithTheirPartsDto>(this.mapper.ConfigurationProvider)
                    .ToArray();
            }

            Serialize(carDtos, new XmlRootAttribute("cars"), "../../../Output/Xml/cars-and-parts.xml");
            var jsonObjects = JsonConvert.SerializeObject(carDtos, new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            });
            File.WriteAllText("../../../Output/Json/cars-and-parts.json", jsonObjects);
        }

        public void LocalSuppliers()
        {
            LocalSupplier[] supplierDtos;

            using (var context = new CarDealerContext())
            {
                supplierDtos = context.Suppliers
                    .Where(s => !s.IsImporter)
                    .ProjectTo<LocalSupplier>(this.mapper.ConfigurationProvider)
                    .ToArray();
            }

            Serialize(supplierDtos, new XmlRootAttribute("suppliers"), "../../../Output/Xml/local-suppliers.xml");
            var jsonObjects = JsonConvert.SerializeObject(supplierDtos, new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            });
            File.WriteAllText("../../../Output/Json/local-suppliers.json", jsonObjects);
        }

        public void CarsFromMakeFerrari()
        {
            CarFromMakeFerrariDto[] carDtos;

            using (var context = new CarDealerContext())
            {
                carDtos = context.Cars
                    .Where(c => c.Make.ToLower() == "ferrari")
                    .OrderBy(c => c.Model)
                    .ThenByDescending(c => c.TravelledDistance)
                    .ProjectTo<CarFromMakeFerrariDto>(this.mapper.ConfigurationProvider)
                    .ToArray();
            }

            Serialize(carDtos, new XmlRootAttribute("cars"), "../../../Output/Xml/ferrari-cars.xml");
            var jsonObjects = JsonConvert.SerializeObject(carDtos, new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            });
            File.WriteAllText("../../../Output/Json/ferrari-cars.json", jsonObjects);
        }

        public void CarsWithDistance()
        {
            CarWithDistanceDto[] carDtos;

            using (var context = new CarDealerContext())
            {
                carDtos = context.Cars
                    .Where(c => c.TravelledDistance > 2000000)
                    .OrderBy(c => c.Make)
                    .ThenBy(c => c.Model)
                    .ProjectTo<CarWithDistanceDto>(this.mapper.ConfigurationProvider)
                    .ToArray();
            }

            Serialize(carDtos, new XmlRootAttribute("cars"), "../../../Output/Xml/cars.xml");
            var jsonObjects = JsonConvert.SerializeObject(carDtos, new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            });
            File.WriteAllText("../../../Output/Json/cars.json", jsonObjects);
        }

        private static void Serialize<T>(T[] model, XmlRootAttribute rootAttribute, string output)
        {
            var serializer = new XmlSerializer(typeof(T[]), rootAttribute);
            var result = new StringBuilder();
            var namespaces = new XmlSerializerNamespaces(new[] {XmlQualifiedName.Empty});

            serializer.Serialize(new StringWriter(result), model, namespaces);
            File.WriteAllText(output, result.ToString());
        }

        private void InitializeMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CarProfile>();
                cfg.AddProfile<CustomerProfile>();
                cfg.AddProfile<PartProfile>();
                cfg.AddProfile<SaleProfile>();
                cfg.AddProfile<SupplierProfile>();
            });

            this.mapper = config.CreateMapper();
        }
    }
}