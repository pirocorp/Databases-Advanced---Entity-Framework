namespace ProductShop
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml;
    using System.Xml.Serialization;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Data;
    using Dtos.Export;
    using Dtos.Import;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using DataAnnotations = System.ComponentModel.DataAnnotations;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            var context = new ProductShopContext();

            //var xmlString = File.ReadAllText("./Datasets/categories-products.xml");

            var result = GetUsersWithProducts(context);
            Console.WriteLine(result);
        }

        public static string ImportUsers(ProductShopContext context, string inputXml)
        {
            var userDtos = DeserializeXml<UserDto>("Users", inputXml);

            SaveDataToDatabase<UserDto, User>(context, userDtos);

            return $"Successfully imported {context.Users.Count()}";
        }

        public static string ImportProducts(ProductShopContext context, string inputXml)
        {
            var productDtos = DeserializeXml<ProductDto>("Products", inputXml);

            SaveDataToDatabase<ProductDto, Product>(context, productDtos);

            return $"Successfully imported {context.Products.Count()}";
        }

        public static string ImportCategories(ProductShopContext context, string inputXml)
        {
            var categoriesDtos = DeserializeXml<CategoryDto>("Categories", inputXml);

            SaveDataToDatabase<CategoryDto, Category>(context, categoriesDtos);

            return $"Successfully imported {context.Categories.Count()}";
        }

        public static string ImportCategoryProducts(ProductShopContext context, string inputXml)
        {
            var categoryProducts = DeserializeXml<CategoryProductDto>("CategoryProducts", inputXml);

            SaveDataToDatabase<CategoryProductDto, CategoryProduct>(context, categoryProducts);

            return $"Successfully imported {context.CategoryProducts.Count()}";
        }

        public static string GetProductsInRange(ProductShopContext context)
        {
            var mapper = GetMapper();

            var products = context.Products
                .Where(p => p.Price >= 500 && p.Price <= 1000)
                .OrderBy(p => p.Price)
                .Take(10)
                .ProjectTo<ProductInRangeDto>(mapper.ConfigurationProvider)
                .ToList();

            return SerializeXml("Products", products);
        }

        public static string GetSoldProducts(ProductShopContext context)
        {
            var mapper = GetMapper();

            var usersSoldProductsDtos = context.Users
                .Where(u => u.ProductsSold.Count > 0)
                .ProjectTo<UsersSoldProductsDto>(mapper.ConfigurationProvider)
                .OrderBy(u => u.LastName)
                .ThenBy(u => u.FirstName)
                .Take(5)
                .ToArray();

            return SerializeXml("Users", usersSoldProductsDtos);
        }

        public static string GetCategoriesByProductsCount(ProductShopContext context)
        {
            var mapper = GetMapper();

            var categoryByProductCountDto = context.Categories
                .ProjectTo<CategoryByProductCountDto>(mapper.ConfigurationProvider)
                .OrderByDescending(c => c.ProductsCount)
                .ThenBy(c => c.TotalRevenue)
                .ToArray();

            return SerializeXml("Categories", categoryByProductCountDto);
        }

        public static string GetUsersWithProducts(ProductShopContext context)
        {
            var mapper = GetMapper();

            // Works against database, but don't work against fucking in memory adapter in Judge

            //var usersAndProductsDtos = context.Users
            //    .Where(u => u.ProductsSold.Count > 0)
            //    .OrderByDescending(u => u.ProductsSold.Count)
            //    .ProjectTo<UserWithSoldProductsDto>(mapper.ConfigurationProvider)
            //    .Take(10)
            //    .ToArray();

            var usersAndProducts = context.Users
                .Where(u => u.ProductsSold.Count > 0)
                .OrderByDescending(u => u.ProductsSold.Count)
                .Select(u => new
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Age = u.Age,
                    SoldProducts = u.ProductsSold.ToList(),
                })
                .Take(10)
                .ToArray()
                .Select(u => new UserWithSoldProductsDto
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Age = u.Age,
                    SoldProducts = new SoldProductsDto
                    {
                        Count = u.SoldProducts.Count,
                        SoldProducts = u.SoldProducts
                            .OrderByDescending(p => p.Price)
                            .Select(p => new SoldProductDto()
                            {
                                Name = p.Name,
                                Price = p.Price
                            })
                            .ToArray()
                    }
                })
                .ToArray();

            var usersCountDto = new UsersAndProductsDto()
            {
                Count = context.Users.Count(u => u.ProductsSold.Count > 0),
                UsersAndProducts = usersAndProducts
            };

            return SerializeXml("Users", usersCountDto);
        }

        private static string SerializeXml<TDto>(string rootAttribute, IEnumerable<TDto> elements)
        {
            var serializer = new XmlSerializer(typeof(TDto[]),
                new XmlRootAttribute(rootAttribute));

            var xmlNamespaces = new XmlSerializerNamespaces(new []{XmlQualifiedName.Empty});

            StringWriter writer;
            using (writer = new StringWriter())
            {
                serializer.Serialize(writer, elements.ToArray(), xmlNamespaces);
            }

            return writer.ToString();
        }

        private static string SerializeXml<TDto>(string rootAttribute, TDto element)
        {
            var serializer = new XmlSerializer(typeof(TDto),
                new XmlRootAttribute(rootAttribute));

            var xmlNamespaces = new XmlSerializerNamespaces(new []{XmlQualifiedName.Empty});

            StringWriter writer;
            using (writer = new StringWriter())
            {
                serializer.Serialize(writer, element, xmlNamespaces);
            }

            return writer.ToString();
        }

        private static IEnumerable<TDto> DeserializeXml<TDto>(string rootAttribute, string inputXml)
        {
            var serializer = new XmlSerializer(typeof(TDto[]),
                new XmlRootAttribute(rootAttribute));

            var userDtos = (TDto[])serializer.Deserialize(new StringReader(inputXml));
            return userDtos;
        }

        private static void SaveDataToDatabase<TDto, TEntity>(
            ProductShopContext context, 
            IEnumerable<TDto> userDtos) 
            where TEntity : class
        {
            var entities = new List<TEntity>();
            var mapper = GetMapper();

            foreach (var userDto in userDtos)
            {
                if (!IsValid(userDto))
                {
                    continue;
                }

                var entity = mapper.Map<TEntity>(userDto);
                entities.Add(entity);
            }

            context.Set<TEntity>().AddRange(entities);
            context.SaveChanges();
        }

        private static IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ProductShopProfile>();
            });

            return config.CreateMapper();
        }

        private static bool IsValid(object obj)
        {
            var validationContext = new DataAnnotations.ValidationContext(obj);
            var validationResults = new List<DataAnnotations.ValidationResult>();

            return DataAnnotations.Validator.TryValidateObject(obj, validationContext, validationResults, true);
        }
    }
}