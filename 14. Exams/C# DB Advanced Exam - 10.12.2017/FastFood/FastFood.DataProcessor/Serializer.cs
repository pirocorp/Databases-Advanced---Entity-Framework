namespace FastFood.DataProcessor
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;
    using Data;
    using Dto.Export;
    using Models.Enums;
    using Newtonsoft.Json;

    public class Serializer
	{
		public static string ExportOrdersByEmployee(FastFoodDbContext context, string employeeName, string orderType)
        {
            var type = Enum.Parse<OrderType>(orderType);

            var employee = context.Employees
                .ToArray() //Only for judge
                .Where(e => e.Name == employeeName)
                .Select(x => new
                {
                    Name = x.Name,
                    Orders = x.Orders
                        .Where(o => o.Type == type)
                        .Select(o => new
                        {
                            Customer = o.Customer,
                            Items = o.OrderItems.Select(i => new
                            {
                                Name = i.Item.Name,
                                Price = i.Item.Price,
                                Quantity = i.Quantity
                            })
                            .ToArray(),
                            TotalPrice = o.OrderItems.Sum(oi => oi.Item.Price * oi.Quantity)
                        })
                        .OrderByDescending(o => o.TotalPrice)
                        .ThenByDescending(o => o.Items.Length)
                        .ToArray(),
                    TotalMade = x.Orders
                        .Where(o => o.Type == type)
                        .Sum(o => o.TotalPrice)
                })
                .FirstOrDefault();

            var result = JsonConvert.SerializeObject(employee);

            return result; //.Replace('"', '\'');
        }

		public static string ExportCategoryStatistics(FastFoodDbContext context, string categoriesString)
        {
            var categoriesArray = categoriesString.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries);

            var categories = context.Categories
                .Where(c => categoriesArray.Contains(c.Name))
                .Select(c => new CategoryDto
                {
                    Name = c.Name,
                    MostPopularItem = c.Items
                        .Select(z => new MostPopularItemDto
                        {
                            Name = z.Name,
                            TimesSold = z.OrderItems.Sum(x => x.Quantity),
                            TotalMade = z.OrderItems.Sum(x => x.Quantity * x.Item.Price)
                        })
                        .OrderByDescending(x => x.TotalMade)
                        .ThenByDescending(x => x.TimesSold)
                        .FirstOrDefault()
                })
                .OrderByDescending(c => c.MostPopularItem.TotalMade)
                .ThenByDescending(c => c.MostPopularItem.TimesSold)
                .ToArray();

            var result = new StringBuilder();
            var serializer = new XmlSerializer(typeof(CategoryDto[]), new XmlRootAttribute("Categories"));
            var xmlNamespaces = new XmlSerializerNamespaces(new[] {XmlQualifiedName.Empty});
            serializer.Serialize(new StringWriter(result), categories, xmlNamespaces);

            return result.ToString().Trim();
        }
	}
}