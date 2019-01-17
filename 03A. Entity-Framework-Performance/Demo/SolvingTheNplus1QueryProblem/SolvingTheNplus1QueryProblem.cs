namespace ResolvingTheNplus1QueryProblem
{
    using System;
    using System.Linq;

    public class SolvingTheNplus1QueryProblem
    {
        public static void Main()
        {
            using (var context = new NorthwindEntities())
            {
                //BestWay(context);

                //PrintCustomersAndRegionsWithQueryProblem(context);

                //PrintCustomersAndRegionsWithoutQueryProblem(context);
            }
        }

        private static void BestWay(NorthwindEntities context)
        {
            var products = context.Products
                .Select(p => new
                {
                    p.ProductName,
                    p.Supplier.CompanyName,
                    p.Category.CategoryName
                })
                .ToList();

            foreach (var product in products)
            {
                Console.WriteLine($"Product: {product.ProductName}; Supplier: {product.CompanyName}; Category: {product.CategoryName}");
            }
        }

        private static void PrintCustomersAndRegionsWithQueryProblem(NorthwindEntities context)
        {
            var products = context.Products
                .ToList();

            foreach (var product in products)
            {
                Console.WriteLine($"Product: {product.ProductName}; Supplier: {product.Supplier.CompanyName}; Category: {product.Category.CategoryName}");
            }
        }

        private static void PrintCustomersAndRegionsWithoutQueryProblem(NorthwindEntities context)
        {
            var products = context.Products
                .Include("Supplier")
                .Include("Category");

            foreach (var product in products)
            {
                Console.WriteLine($"Product: {product.ProductName}; Supplier: {product.Supplier.CompanyName}; Category: {product.Category.CategoryName}");
            }
        }
    }
}