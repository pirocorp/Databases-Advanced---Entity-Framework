namespace CarDealer.App
{
    using System.IO;
    using System.Linq;

    using Microsoft.EntityFrameworkCore;

    using Newtonsoft.Json;

    using Data;

    public class OnlyJsonExport
    {
        private JsonSerializerSettings jsonSerializerSettings;

        public OnlyJsonExport()
        {
            this.InitializeJsonSerializerSettings();
        }

        public void SalesWithAppliedDiscount()
        {
            string jsonObjects;
            using (var context = new CarDealerContext())
            {
                var sales = context.Sales
                    .Select(s => new
                    {
                        car = new
                        {
                            s.Car.Make,
                            s.Car.Model,
                            s.Car.TravelledDistance
                        },
                        customerName = s.Customer.Name,
                        s.Discount,
                        price = s.Car.Parts.Select(p => p.Part.Price).Sum(), 
                        priceWithDiscount = s.Car.Parts.Select(p => p.Part.Price).Sum() - s.Car.Parts.Select(p => p.Part.Price).Sum() * (s.Discount / 100M)
                    })
                    .ToArray();

                jsonObjects = JsonConvert.SerializeObject(sales, this.jsonSerializerSettings);
            }

            File.WriteAllText("../../../Output/Json/sales-discounts.json", jsonObjects);
        }

        public void TotalSalesByCustomer()
        {
            string jsonObjects;
            using (var context = new CarDealerContext())
            {
                var customers = context.Customers
                    .Where(c => c.Sales.Count > 0)
                    .Select(c => new
                    {
                        fullName = c.Name,
                        boughtCars = c.Sales.Count,
                        spentMoney = c.Sales
                            .Select(s => s.Car.Parts.Select(p => p.Part.Price).Sum() - (s.Car.Parts.Select(p => p.Part.Price).Sum() * (s.Discount / 100M)))
                            .ToArray() //<-- if this is changed to Sum() leads to N + 1 query problem
                    })
                    .ToArray()
                    .Select(x => new
                    {
                        x.fullName,
                        x.boughtCars,
                        spentMoney = x.spentMoney.Sum() //<-- so i aggregate array on application level
                    })
                    .ToArray();

                jsonObjects = JsonConvert.SerializeObject(customers, this.jsonSerializerSettings);
            }

            File.WriteAllText("../../../Output/Json/customers-total-sales.json", jsonObjects);
        }

        public void CarsWithTheirListOfParts()
        {
            string jsonObjects;
            using (var context = new CarDealerContext())
            {
                var cars = context.Cars
                    .Select(c => new
                    {
                        car = new
                        {
                            c.Make,
                            c.Model,
                            c.TravelledDistance
                        },
                        parts = c.Parts
                            .Select(p => new
                            {
                                p.Part.Name,
                                p.Part.Price
                            })
                            .ToArray()
                    })
                    .ToArray();

                jsonObjects = JsonConvert.SerializeObject(cars, this.jsonSerializerSettings);
            }

            File.WriteAllText("../../../Output/Json/cars-and-parts.json", jsonObjects);
        }

        public void LocalSuppliers()
        {
            string jsonObjects;
            using (var context = new CarDealerContext())
            {
                var suppliers = context.Suppliers
                    .Where(s => !s.IsImporter)
                    .Select(s => new
                    {
                        s.Id,
                        s.Name,
                        PartsCount = s.Parts.Count
                    })
                    .ToArray();

                jsonObjects = JsonConvert.SerializeObject(suppliers, this.jsonSerializerSettings);
            }

            File.WriteAllText("../../../Output/Json/local-suppliers.json", jsonObjects);
        }

        public void CarsFromMakeToyota()
        {
            string jsonObjects;
            using (var context = new CarDealerContext())
            {
                var cars = context.Cars
                    .Where(c => c.Make == "Toyota")
                    .OrderBy(c => c.Model)
                    .ThenByDescending(c => c.TravelledDistance)
                    .Select(x => new
                    {
                        x.Id,
                        x.Make,
                        x.Model,
                        x.TravelledDistance
                    })
                    .ToArray();

                jsonObjects = JsonConvert.SerializeObject(cars, this.jsonSerializerSettings);
            }

            File.WriteAllText("../../../Output/Json/toyota-cars.json", jsonObjects);
        }

        public void OrderedCustomers()
        {
            string jsonObjects;
            using (var context = new CarDealerContext())
            {
                var customers = context.Customers
                    .OrderBy(c => c.BirthDate)
                    .ThenBy(c => c.IsYoungDriver)
                    .Include(c => c.Sales)
                    .ToArray();

                jsonObjects = JsonConvert.SerializeObject(customers, this.jsonSerializerSettings);
            }

            File.WriteAllText("../../../Output/Json/ordered-customers.json", jsonObjects);
        }

        private void InitializeJsonSerializerSettings()
        {
            this.jsonSerializerSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                //ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
        }
    }
}