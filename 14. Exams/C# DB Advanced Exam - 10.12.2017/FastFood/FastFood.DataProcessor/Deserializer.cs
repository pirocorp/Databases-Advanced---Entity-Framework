namespace FastFood.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using Data;
    using Dto.Import;
    using Models;
    using Models.Enums;
    using Newtonsoft.Json;

    public static class Deserializer
	{
		private const string FAILURE_MESSAGE = "Invalid data format.";
		private const string SUCCESS_MESSAGE = "Record {0} successfully imported.";

		public static string ImportEmployees(FastFoodDbContext context, string jsonString)
        {
            var deserializedEmployees = JsonConvert.DeserializeObject<EmployeeDto[]>(jsonString);

            var sb = new StringBuilder();
            var employees = new List<Employee>();

            foreach (var employeeDto in deserializedEmployees)
            {
                if (!IsValid(employeeDto))
                {
                    sb.AppendLine(FAILURE_MESSAGE);
                    continue;
                }

                var position = GetPosition(context, employeeDto.Position);
                var employee = new Employee
                {
                    Name = employeeDto.Name,
                    Age = employeeDto.Age,
                    PositionId = position.Id
                };
                
                employees.Add(employee);
                sb.AppendLine(string.Format(SUCCESS_MESSAGE, employee.Name));
            }

            context.AddRange(employees);
            context.SaveChanges();
            return sb.ToString().Trim();
        }

        public static string ImportItems(FastFoodDbContext context, string jsonString)
		{
            var deserializedItems = JsonConvert.DeserializeObject<ItemDto[]>(jsonString);

            var sb = new StringBuilder();
            var items = new List<Item>();

            foreach (var itemDto in deserializedItems)
            {
                if (!IsValid(itemDto))
                {
                    sb.AppendLine(FAILURE_MESSAGE);
                    continue;
                }

                //Ignoring duplicated entities
                if (context.Items.Any(i => i.Name == itemDto.Name) ||
                    items.Any(i => i.Name == itemDto.Name))
                {
                    sb.AppendLine(FAILURE_MESSAGE);
                    continue;
                }

                var category = GetCategory(context, itemDto.Category);
                var item = new Item
                {
                    Name = itemDto.Name,
                    Price = itemDto.Price,
                    CategoryId = category.Id
                };

                items.Add(item);
                sb.AppendLine(string.Format(SUCCESS_MESSAGE, item.Name));
            }

            context.AddRange(items);
            context.SaveChanges();
            return sb.ToString().Trim();
        }

        public static string ImportOrders(FastFoodDbContext context, string xmlString)
        {
            var serializer = new XmlSerializer(typeof(OrderDto[]), new XmlRootAttribute("Orders"));
            var deserializedOrders = (OrderDto[]) serializer.Deserialize(new StringReader(xmlString));

            var sb = new StringBuilder();
            var orders = new List<Order>();

            foreach (var orderDto in deserializedOrders)
            {
                if (!IsValid(orderDto))
                {
                    sb.AppendLine(FAILURE_MESSAGE);
                    continue;
                }

                var employee = context.Employees.FirstOrDefault(x => x.Name == orderDto.Employee);

                if (employee == null)
                {
                    sb.AppendLine(FAILURE_MESSAGE);
                    continue;
                }

                var areValidItems = AreValidItems(context, orderDto.Items);

                if (!areValidItems)
                {
                    sb.AppendLine(FAILURE_MESSAGE);
                    continue;
                }

                var date = DateTime.ParseExact(orderDto.DateTime, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
                var orderType = Enum.Parse<OrderType>(orderDto.Type);

                var order = new Order()
                {
                    Customer = orderDto.Customer,
                    Employee = employee,
                    DateTime = date,
                    Type = orderType
                };

                var orderItems = new List<OrderItem>();

                foreach (var itemDto in orderDto.Items)
                {
                    var item = context.Items.First(x => x.Name == itemDto.Name);

                    var orderItem = new OrderItem
                    {
                        Order = order,
                        Item = item,
                        Quantity = itemDto.Quantity
                    };

                    orderItems.Add(orderItem);
                }

                order.OrderItems = orderItems;
                sb.AppendLine($"Order for {orderDto.Customer} on {date :dd/MM/yyyy HH:mm} added");
                orders.Add(order);
            }

            context.Orders.AddRange(orders);
            context.SaveChanges();
            return sb.ToString().Trim();
        }

        private static bool IsValid(object obj)
        {
            var validationContext = new ValidationContext(obj);
            var validationResults = new List<ValidationResult>();

            return Validator.TryValidateObject(obj, validationContext, validationResults, true);
        }

        private static Position GetPosition(FastFoodDbContext context, string positionName)
        {
            var position = context.Positions.FirstOrDefault(x => x.Name == positionName);

            if (position == null)
            {
                position = new Position
                {
                    Name = positionName
                };

                context.Positions.Add(position);
                context.SaveChanges();
            }

            return position;
        }

        private static Category GetCategory(FastFoodDbContext context, string categoryName)
        {
            var category = context.Categories.FirstOrDefault(x => x.Name == categoryName);

            if (category == null)
            {
                category = new Category
                {
                    Name = categoryName
                };

                context.Categories.Add(category);
                context.SaveChanges();
            }

            return category;
        }

        private static bool AreValidItems(FastFoodDbContext context, OrderItemDto[] orderDtoItems)
        {
            foreach (var item in orderDtoItems)
            {
                var itemExists = context.Items.Any(x => x.Name == item.Name);

                if (!itemExists ||
                    !IsValid(item))
                {
                    return false;
                }
            }

            return true;
        }
    }
}