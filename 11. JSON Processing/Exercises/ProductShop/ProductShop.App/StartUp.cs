namespace ProductShop.App
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using DataAnnotations = System.ComponentModel.DataAnnotations;

    using AutoMapper;
    using Newtonsoft.Json;

    using Data;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ProductShopProfile>();
            });
            var mapper = config.CreateMapper();

            //ImportUsers();
            //ImportProducts();
            //ImportCategories();
            //ImportCategoryProducts();
            //ProductsInRange();
            //SuccessfullySoldProducts();
            //CategoriesByProductsCount();
            UsersAndProducts();
        }

        private static void UsersAndProducts()
        {
            string jsonObjects;
            using (var context = new ProductShopContext())
            {
                var users = context.Users
                    .Where(u => u.ProductsSold.Any(p => p.BuyerId != null))
                    .OrderByDescending(u => u.ProductsSold.Count)
                    .ThenBy(u => u.LastName)
                    .Select(u => new
                    {
                        firstName = u.FirstName,
                        lastName = u.LastName,
                        age = u.Age,
                        soldProducts = new
                        {
                            count = u.ProductsSold.Count,
                            products = u.ProductsSold
                                .Where(p => p.BuyerId != null)
                                .Select(p => new
                                {
                                    name = p.Name,
                                    price = p.Price
                                })
                                .ToArray()//If missing <-- This leads to N+1 query problem
                        }
                    })
                    .ToArray();

                var result = new
                {
                    usersCount = users.Length,
                    users = users
                };

                jsonObjects = JsonConvert.SerializeObject(result,
                    new JsonSerializerSettings
                    {
                        Formatting = Formatting.Indented,
                        NullValueHandling = NullValueHandling.Ignore
                    });
            }

            File.WriteAllText("../../../Output/users-and-products.json", jsonObjects);
        }

        private static void CategoriesByProductsCount()
        {
            string jsonObjects;
            using (var context = new ProductShopContext())
            {
                var categories = context.Categories
                    .OrderByDescending(c => c.CategoryProducts.Count)
                    //.Include(c => c.CategoryProducts) // If we need DefaultIfEmpty
                    //.ThenInclude(cp => cp.Product) // we can use Include and ThenInclude
                    //.ToArray() //<-- this terminates the query //It Solves N + 1 Query problem for the price of more data transfer
                    .Select(c => new
                    {
                        category = c.Name,
                        productsCount = c.CategoryProducts.Count,
                        averagePrice = c.CategoryProducts
                            .Select(p => p.Product.Price)
                            //.DefaultIfEmpty(0) <-- this leads to N + 1 query problem
                            //.Average() <-- this leads to N + 1 query problem
                            .Sum() / c.CategoryProducts.Count,
                        totalRevenue = c.CategoryProducts
                            .Select(p => p.Product.Price)
                            //.DefaultIfEmpty(0) <-- this leads to N + 1 query problem
                            .Sum() 
                    })
                    .ToArray();

                jsonObjects = JsonConvert.SerializeObject(categories,
                    new JsonSerializerSettings
                    {
                        Formatting = Formatting.Indented,
                        NullValueHandling = NullValueHandling.Ignore
                    });
            }

            File.WriteAllText("../../../Output/categories-by-products.json", jsonObjects);
        }

        private static void SuccessfullySoldProducts()
        {
            string jsonObjects;
            using (var context = new ProductShopContext())
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

                jsonObjects = JsonConvert.SerializeObject(users, 
                    new JsonSerializerSettings
                    {
                        Formatting = Formatting.Indented,
                        NullValueHandling = NullValueHandling.Ignore
                    });
            }

            File.WriteAllText("../../../Output/users-sold-products.json", jsonObjects);
        }

        private static void ProductsInRange()
        {
            string jsonObjects;
            using (var context = new ProductShopContext())
            {
                var products = context.Products
                    .Where(x => x.Price >= 500 && x.Price <= 1000)
                    .OrderBy(x => x.Price)
                    .Select(s => new
                    {
                        name = s.Name,
                        price = s.Price,
                        seller = $"{s.Seller.FirstName} {s.Seller.LastName}".Trim()
                    })
                    .ToArray();

                jsonObjects = JsonConvert.SerializeObject(products, Formatting.Indented);
            }

            File.WriteAllText("../../../Output/products-in-range.json", jsonObjects);
        }

        private static void ImportCategoryProducts()
        {
            var categoryProducts = new List<CategoryProduct>();
            var random = new Random();

            for (var productId = 1; productId <= 200; productId++)
            {
                var categoryId = random.Next(1, 12);

                var generatedCategoryId = random.Next(1, 12);

                while (generatedCategoryId == categoryId)
                {
                    generatedCategoryId = random.Next(1, 12);
                }

                var anotherCategoryId = generatedCategoryId;

                var categoryProduct = new CategoryProduct
                {
                    CategoryId = categoryId,
                    ProductId = productId,
                };

                var anotherCategoryProduct = new CategoryProduct()
                {
                    CategoryId = anotherCategoryId,
                    ProductId = productId
                };

                categoryProducts.Add(categoryProduct);
                categoryProducts.Add(anotherCategoryProduct);
            }

            using (var context = new ProductShopContext())
            {
                context.CategoryProducts.AddRange(categoryProducts);
                context.SaveChanges();
            }
        }

        private static void ImportCategories()
        {
            var jsonString = File.ReadAllText("../../../Json/categories.json");

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

            using (var context = new ProductShopContext())
            {
                context.Categories.AddRange(categories);
                context.SaveChanges();
            }
        }

        private static void ImportProducts()
        {
            var jsonString = File.ReadAllText("../../../Json/products.json");

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

            using (var context = new ProductShopContext())
            {
                context.Products.AddRange(products);
                context.SaveChanges();
            }
        }

        private static void ImportUsers()
        {
            var jsonString = File.ReadAllText("../../../Json/users.json");

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

            using (var context = new ProductShopContext())
            {
                context.Users.AddRange(users);
                context.SaveChanges();
            }
        }

        private static bool IsValid(object obj)
        {
            var validationContext = new DataAnnotations.ValidationContext(obj);
            var result = new List<DataAnnotations.ValidationResult>();

            return DataAnnotations.Validator.TryValidateObject(obj, validationContext, result, true);
        }
    }
}