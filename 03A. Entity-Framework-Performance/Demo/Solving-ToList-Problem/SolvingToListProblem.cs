namespace Solving_ToList_Problem
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class SolvingToListProblem
    {
        public static void Main()
        {
            //IncorrectSelectExample();

            //CorrectSelectExample();

            //ShowCustomersPurchasedFromTokyo_Slow();

            //ShowCustomersPurchasedFromTokyo_Fast();
        }

        private static void CorrectSelectExample()
        {
            using (var context = new NorthwindEntities())
            {
                context.Products
                    .Where(p => p.CategoryID == 3)
                    .Select(e => new
                    {
                        e.ProductName
                    })
                    .ToList()
                    .ForEach(i =>
                    {
                        Console.WriteLine(i.ProductName);
                    });
            }
        }

        private static void IncorrectSelectExample()
        {
            using (var context = new NorthwindEntities())
            {
                context.Products
                    .Where(p => p.CategoryID == 3)
                    .ToList()
                    .ForEach(i =>
                    {
                        Console.WriteLine(i.ProductName);
                    });
            }
        }

        private static void ShowCustomersPurchasedFromTokyo_Slow()
        {
            List<Order_Detail> orderItemsFromTokyo;

            using (var context = new NorthwindEntities())
            {
                orderItemsFromTokyo = context.Order_Details.ToList()
                    .Where(od => od.Product.Supplier.City == "Tokyo")
                    .ToList();
            }

            var customerPurchasedFromTokyo = orderItemsFromTokyo
                .Select(oi => oi.Order.Customer)
                .ToList();

            foreach (var customer in customerPurchasedFromTokyo)
            {
                Console.WriteLine(customer.ContactName);
            }
        }

        private static void ShowCustomersPurchasedFromTokyo_Fast()
        {
            List<Customer> customerPurchasedFromTokyo;

            using (var context = new NorthwindEntities())
            {
                customerPurchasedFromTokyo = context.Order_Details
                    .Where(orderItem => orderItem.Product.Supplier.City == "Tokyo")
                    .Select(orderItem => orderItem.Order.Customer)
                    .ToList();
            }

            foreach (var customer in customerPurchasedFromTokyo)
            {
                Console.WriteLine(customer.ContactName);
            }
        }
    }
}