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
    using Profiles;

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

            Serialize(saleDtos, new XmlRootAttribute("sales"), "../../../Output/sales-discounts.xml");
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

            Serialize(customerDtos, new XmlRootAttribute("customers"), "../../../Output/customers-total-sales.xml");
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

            Serialize(carDtos, new XmlRootAttribute("cars"), "../../../Output/cars-and-parts.xml");
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

            Serialize(supplierDtos, new XmlRootAttribute("suppliers"), "../../../Output/local-suppliers.xml");
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

            Serialize(carDtos, new XmlRootAttribute("cars"), "../../../Output/ferrari-cars.xml");
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

            Serialize(carDtos, new XmlRootAttribute("cars"), "../../../Output/cars.xml");
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