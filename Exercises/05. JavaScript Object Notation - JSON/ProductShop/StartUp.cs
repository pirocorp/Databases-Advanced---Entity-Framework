namespace ProductShop
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml;
    using AutoMapper;
    using Data;
    using Models;
    using Newtonsoft.Json;

    using DataAnnotations = System.ComponentModel.DataAnnotations;
    using Formatting = Newtonsoft.Json.Formatting;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ProductShopProfile>();
            });

            var mapper = config.CreateMapper();

            var context = new ProductShopContext();
            //var jsonString = File.ReadAllText("./Datasets/categories-products.json");

            var result = GetUsersWithProducts(context);
            Console.WriteLine(result);
        }

        public static string GetUsersWithProducts(ProductShopContext context)
        {
            var users = context.Users
                .Where(u => u.ProductsSold.Any(p => p.BuyerId != null))
                .OrderByDescending(u => u.ProductsSold.Count(p => p.BuyerId != null))
                .Select(u => new
                {
                    firstName = u.FirstName,
                    lastName = u.LastName,
                    age = u.Age,
                    soldProducts = u.ProductsSold.ToList(),
                })
                .ToList()
                .Select(u => new
                {
                    u.firstName,
                    u.lastName,
                    u.age,
                    soldProducts = new
                    {
                        count = u.soldProducts.Count(p => p.BuyerId != null),
                        products = u.soldProducts
                            .Where(p => p.BuyerId != null)
                            .Select(p => new
                            {
                                name = p.Name,
                                price = p.Price
                            })
                            .ToList()
                    }
                })
                .ToArray();

            var result = new
            {
                usersCount = users.Length,
                users = users
            };

            var jsonObjects = JsonConvert.SerializeObject(result,
                new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented,
                    NullValueHandling = NullValueHandling.Ignore
                });

            return jsonObjects;
        }

        public static string GetCategoriesByProductsCount(ProductShopContext context)
        {
            var categories = context.Categories
                .OrderByDescending(c => c.CategoryProducts.Count)
                .Select(c => new
                {
                    category = c.Name,
                    productsCount = c.CategoryProducts.Count,
                    averagePrice = c.CategoryProducts
                        .Select(p => p.Product.Price)
                        .Sum() / c.CategoryProducts.Count,
                    totalRevenue = c.CategoryProducts
                        .Select(p => p.Product.Price)
                        .Sum()
                })
                .ToArray()
                .Select(c => new
                {
                    c.category,
                    c.productsCount,
                    averagePrice = $"{c.averagePrice:F2}",
                    totalRevenue = $"{c.totalRevenue:F2}"
                });

            var jsonObjects = JsonConvert.SerializeObject(categories,
                new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented,
                    NullValueHandling = NullValueHandling.Include
                });

            return jsonObjects;
        }

        public static string GetSoldProducts(ProductShopContext context)
        {
            var users = context.Users
                .Where(u => u.ProductsSold.Any(p => p.BuyerId != null))
                .OrderBy(u => u.LastName)
                .ThenBy(u => u.FirstName)
                .Select(u => new
                {
                    firstName = u.FirstName,
                    lastName = u.LastName,
                    soldProducts = u.ProductsSold
                        .Where(p => p.BuyerId != null)
                        .Select(p => new
                        {
                            name = p.Name,
                            price = p.Price,
                            buyerFirstName = p.Buyer.FirstName,
                            buyerLastName = p.Buyer.LastName
                        })
                        .ToArray()
                })
                .ToArray();

            var jsonObjects = JsonConvert.SerializeObject(users,
                new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented,
                    NullValueHandling = NullValueHandling.Include
                });

            return jsonObjects;
        }

        public static string GetProductsInRange(ProductShopContext context)
        {
            var products = context.Products
                .Where(x => x.Price >= 500 && x.Price <= 1000)
                .OrderBy(x => x.Price)
                .Select(s => new
                {
                    name = s.Name,
                    price = s.Price,
                    seller = $"{s.Seller.FirstName} {s.Seller.LastName}"
                })
                .ToArray();

            return JsonConvert.SerializeObject(products, Newtonsoft.Json.Formatting.Indented);
        }

        public static string ImportCategoryProducts(ProductShopContext context, string jsonString)
        {
            var deserializeObjects = JsonConvert
                .DeserializeObject<CategoryProduct[]>(jsonString);

            var categoryProducts = new List<CategoryProduct>();

            foreach (var categoryProduct in deserializeObjects)
            {
                if (!IsValid(categoryProduct))
                {
                    continue;
                }

                categoryProducts.Add(categoryProduct);
            }

            context.CategoryProducts.AddRange(categoryProducts);
            context.SaveChanges();

            return $"Successfully imported {context.CategoryProducts.Count()}";
        }

        public static string ImportCategories(ProductShopContext context, string jsonString)
        {
            var deserializeObjects = JsonConvert
                .DeserializeObject<Category[]>(jsonString);

            var categories = new List<Category>();

            foreach (var category in deserializeObjects)
            {
                if (!IsValid(category))
                {
                    continue;
                }

                categories.Add(category);
            }

            context.Categories.AddRange(categories);
            context.SaveChanges();

            return $"Successfully imported {context.Categories.Count()}";
        }

        public static string ImportProducts(ProductShopContext context, string jsonString)
        {
            var deserializedProducts = JsonConvert
                .DeserializeObject<Product[]>(jsonString);

            var products = new List<Product>();
            var random = new Random();

            foreach (var product in deserializedProducts)
            {
                if (!IsValid(product))
                {
                    continue;
                }

                var sellerId = random.Next(1, 35);
                var buyerId = random.Next(35, 57);

                product.BuyerId = buyerId;
                product.SellerId = sellerId;

                var rnd = random.Next(1, 4);

                if (rnd == 3)
                {
                    product.BuyerId = null;
                }

                products.Add(product);
            }

            context.Products.AddRange(products);
            context.SaveChanges();

            return $"Successfully imported {context.Products.Count()}";
        }

        public static string ImportUsers(ProductShopContext context, string jsonString)
        {
            var deserializedUsers = JsonConvert
                .DeserializeObject<User[]>(jsonString);

            var users = new List<User>();

            foreach (var deserializedUser in deserializedUsers)
            {
                if (IsValid(deserializedUser))
                {
                    users.Add(deserializedUser);
                }
            }

            context.Users.AddRange(users);
            context.SaveChanges();

            return $"Successfully imported {context.Users.Count()}";
        }

        private static bool IsValid(object obj)
        {
            var validationContext = new DataAnnotations.ValidationContext(obj);
            var result = new List<DataAnnotations.ValidationResult>();

            return DataAnnotations.Validator.TryValidateObject(obj, validationContext, result, true);
        }
    }
}