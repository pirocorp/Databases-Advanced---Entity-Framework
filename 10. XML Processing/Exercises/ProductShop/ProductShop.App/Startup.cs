namespace ProductShop.App
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;
    using DataAnnotations = System.ComponentModel.DataAnnotations;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using Data;
    using Dtos.Alternative;
    using Dtos.Export;
    using Dtos.Import;
    using Models;
    using Profiles;


    public class Startup
    {
        public static void Main()
        {
            //ImportUserData();
            //ImportProductData();
            //ImportCategories();
            //GenerateCategoryProducts();
            //ProductsInRange();
            //SoldProducts();
            //CategoriesByProductsCount();
            //UsersAndProducts();
            AlternativeUsersAndProducts();
        }

        private static void AlternativeUsersAndProducts()
        {
            CountDto users;

            using (var context = new ProductShopContext())
            {
                users = new CountDto()
                {
                    Count = context.Users.Count(),
                    Users = context.Users
                        .Where(x => x.SoldProducts.Count >= 1)
                        .OrderByDescending(x => x.SoldProducts.Count)
                        .ThenBy(x => x.LastName)
                        .Select(x => new AlternativeUserDto
                        {
                            FirstName = x.FirstName,
                            LastName = x.LastName,
                            Age = x.Age.Value.ToString(),
                            SoldProducts = new SoldProductsDto
                            {
                                Count = x.SoldProducts.Count,
                                Products = x.SoldProducts.Select(k => new ProductAlternativeDto
                                {
                                    Name = k.Name,
                                    Price = k.Price
                                })
                                .ToArray()
                            }
                        })
                        .ToArray()
                };
            }

            var result = new StringBuilder();
            var xmlNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });

            var serializer = new XmlSerializer(typeof(CountDto), new XmlRootAttribute("users"));
            serializer.Serialize(new StringWriter(result), users, xmlNamespaces);
            File.WriteAllText("../../../Output/users-and-products.xml", result.ToString());
        }

        private static void UsersAndProducts()
        {
            //Not Full But Better solution in my mind
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ProductShopProfile>();
            });

            var mapper = config.CreateMapper();
            UsersAndProductsDto[] usersAndProductsDtos;

            using (var context = new ProductShopContext())
            {
                //If you put OrderBy an thenBy after projectTo you get N + 1 query problem
                usersAndProductsDtos = context.Users
                    .Where(u => u.SoldProducts.Count > 0)
                    .OrderByDescending(u => u.SoldProducts.Count)
                    .ThenBy(u => u.LastName)
                    .ProjectTo<UsersAndProductsDto>(mapper.ConfigurationProvider)
                    .ToArray();
            }

            var usersCountDto = new UsersCountDto()
            {
                Count = usersAndProductsDtos.Length,
                UsersAndProducts = usersAndProductsDtos
            };


            var serializer = new XmlSerializer(typeof(UsersCountDto),
                new XmlRootAttribute("users"));
            var result = new StringBuilder();
            var xmlNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            serializer.Serialize(new StringWriter(result), usersCountDto, xmlNamespaces);
            File.WriteAllText("../../../Output/users-and-products.xml", result.ToString());
        }

        private static void CategoriesByProductsCount()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ProductShopProfile>();
            });

            var mapper = config.CreateMapper();
            CategoryByProductCountDto[] categoryByProductCountDto;

            using (var context = new ProductShopContext())
            {
                categoryByProductCountDto = context.Categories
                    .OrderByDescending(c => c.Products.Count)
                    .ProjectTo<CategoryByProductCountDto>(mapper.ConfigurationProvider)
                    .ToArray();
            }

            var serializer = new XmlSerializer(typeof(CategoryByProductCountDto[]),
                new XmlRootAttribute("categories"));
            var result = new StringBuilder();
            var xmlNamespaces = new XmlSerializerNamespaces(new []{XmlQualifiedName.Empty});
            serializer.Serialize(new StringWriter(result), categoryByProductCountDto, xmlNamespaces);
            File.WriteAllText("../../../Output/categories-by-products.xml", result.ToString());
        }

        private static void SoldProducts()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ProductShopProfile>();
            });

            var mapper = config.CreateMapper();
            UsersSoldProductsDto[] usersSoldProductsDtos;

            using (var context = new ProductShopContext())
            {
                usersSoldProductsDtos = context.Users
                    .Where(u => u.SoldProducts.Count > 0)
                    .ProjectTo<UsersSoldProductsDto>(mapper.ConfigurationProvider)
                    .OrderBy(u => u.LastName)
                    .ThenBy(u => u.FirstName)
                    .ToArray();
            }

            var serializer = new XmlSerializer(typeof(UsersSoldProductsDto[]),
                new XmlRootAttribute("users"));
            var result = new StringBuilder();
            var xmlNamespaces = new XmlSerializerNamespaces(new []{XmlQualifiedName.Empty});
            serializer.Serialize(new StringWriter(result), usersSoldProductsDtos, xmlNamespaces);
            File.WriteAllText("../../../Output/users-sold-products.xml", result.ToString());
        } 

        private static void ProductsInRange()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ProductShopProfile>();
            });

            var mapper = config.CreateMapper();
            ProductInRangeDto[] productInRangeDtos;

            using (var context = new ProductShopContext())
            {
                productInRangeDtos = context.Products
                    .Where(p => p.Price >= 1000 && p.Price <= 2000 && p.BuyerId != null)
                    .OrderBy(p => p.Price)
                    .ProjectTo<ProductInRangeDto>(mapper.ConfigurationProvider)
                    .ToArray();
            }

            var serializer = new XmlSerializer(typeof(ProductInRangeDto[]),
                new XmlRootAttribute("products"));

            var result = new StringBuilder();
            var xmlNamespaces = new XmlSerializerNamespaces(new []{XmlQualifiedName.Empty});
            serializer.Serialize(new StringWriter(result), productInRangeDtos, xmlNamespaces);
            File.WriteAllText("../../../Output/products-in-range.xml", result.ToString());
        }

        private static void GenerateCategoryProducts()
        {
            var random = new Random();
            var categoryProducts = new List<CategoryProduct>();

            for (var productId = 1; productId <= 200; productId++)
            {
                var categoryId = random.Next(1, 12);

                var categoryProduct = new CategoryProduct()
                {
                    ProductId = productId,
                    CategoryId = categoryId
                };

                categoryProducts.Add(categoryProduct);
            }

            using (var context = new ProductShopContext())
            {
                context.CategoryProducts.AddRange(categoryProducts);
                context.SaveChanges();
            }
        }

        private static void ImportCategories()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ProductShopProfile>();
            });

            var mapper = config.CreateMapper();
            var xmlString = File.ReadAllText("../../../Xml/categories.xml");

            var serializer = new XmlSerializer(typeof(CategoryDto[]), 
                new XmlRootAttribute("categories"));
            var categoriesDtos = (CategoryDto[]) serializer.Deserialize(new StringReader(xmlString));

            var categories = new List<Category>();

            foreach (var categoryDto in categoriesDtos)
            {
                if (!IsValid(categoryDto))
                {
                    continue;
                }

                var category = mapper.Map<Category>(categoryDto);
                categories.Add(category);
            }

            using (var context = new ProductShopContext())
            {
                context.Categories.AddRange(categories);
                context.SaveChanges();
            }
        }

        private static void ImportProductData()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ProductShopProfile>();
            });

            var mapper = config.CreateMapper();
            var xmlString = File.ReadAllText("../../../Xml/products.xml");

            var serializer = new XmlSerializer(typeof(ProductDto[]), 
                new XmlRootAttribute("products"));
            var productDtos = (ProductDto[])serializer.Deserialize(new StringReader(xmlString));

            var products = new List<Product>();
            var random = new Random();

            var counter = 1;

            foreach (var productDto in productDtos)
            {
                if (!IsValid(productDto))
                {
                    continue;
                }

                var product = mapper.Map<Product>(productDto);
                
                var buyerId = random.Next(1, 30);
                var sellerId = random.Next(31, 56);

                product.BuyerId = buyerId;
                product.SellerId = sellerId;

                if (counter == 4)
                {
                    product.BuyerId = null;
                    counter = 0;
                }

                products.Add(product);
                counter++;
            }

            using (var context = new ProductShopContext())
            {
                context.Products.AddRange(products);
                context.SaveChanges();
            }
        }

        private static void ImportUserData()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ProductShopProfile>();
            });

            var mapper = config.CreateMapper();
            var xmlString = File.ReadAllText("../../../Xml/users.xml");

            var serializer = new XmlSerializer(typeof(UserDto[]), 
                new XmlRootAttribute("users"));
            var userDtos = (UserDto[]) serializer.Deserialize(new StringReader(xmlString));

            var users = new List<User>();

            foreach (var userDto in userDtos)
            {
                if (!IsValid(userDto))
                {
                    continue;
                }

                var user = mapper.Map<User>(userDto);
                users.Add(user);
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
            var validationResults = new List<DataAnnotations.ValidationResult>();

            return DataAnnotations.Validator.TryValidateObject(obj, validationContext, validationResults, true);
        }
    }
}
