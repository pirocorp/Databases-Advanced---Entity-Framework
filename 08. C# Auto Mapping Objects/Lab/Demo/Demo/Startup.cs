namespace Demo
{
    using System;
    using System.Linq;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Microsoft.EntityFrameworkCore;

    using Data;
    using DTOs;
    using DTOs.Profiles;
    using Mappings;
    using Models;

    public class Startup
    {
        public static void Main()
        {
            //ManualMappingInline();
            //ManualMapping();
            //AutoMappingInline();
            AutoMapping();
        }

        private static void AutoMapping()
        {
            //Using profile for our dto configuration
            Mapper.Initialize(cfg => cfg.AddProfile<ProductProfile>());

            using (var context = new DemoContext())
            {
                var products = context.Products
                    .ProjectTo<ProductDto>()
                    .ToArray();
            }
        }

        private static void AutoMappingInline()
        {
            //Properties will be mapped by name
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Product, ProductDto>()
                    .ForMember(dto => dto.InStorageCount, 
                        opt => opt.MapFrom(src => src.Storages.Sum(p => p.Quantity)));
            });

            Product product = null;
            using (var context = new DemoContext())
            {
                product = context.Products
                    .Include(p => p.Storages)
                    .First();
            }

            var resultDto = Mapper.Map<ProductDto>(product);

            //Flattening Complex Properties
            //Mapper.Initialize(cfg =>
            //    cfg.CreateMap<Event, CalendarEventViewModel>()
            //        .ForMember(dest => dest.Date,
            //            opt => opt.MapFrom(src => src.Date.Date))
            //        .ForMember(dest => dest.Hour,
            //            opt => opt.MapFrom(src => src.Date.Hour))
            //        .ForMember(dest => dest.Minute,
            //            opt => opt.MapFrom(src => src.Date.Minute)));

            //Using AutoMapper to map an entire DB collection:
            using (var context = new DemoContext())
            {
                var products = context.Products
                    .ProjectTo<ProductDto>()
                    .ToArray();
            }
        }

        private static void ManualMapping()
        {
            Product entity = null;
            using (var context = new DemoContext())
            {
                entity = context.Products
                    .Include(p => p.Storages)
                    .First();
            }
            //Using methods from Mappings\ProductMapping

            //Calling static method form ProductMapping which converts Product to ProductDto
            var result = ProductMapping.MapToProductDto(entity);

            //Calling Extension Method for Product class from ProductMapping class (Like Linq)
            var result2 = entity.ToProductDto();

            //Using Expression
            using (var context = new DemoContext())
            {
                var dtos = context.Products
                    .Select(ProductMapping.ToProductDtoExpression())
                    .ToArray();
            }
        }

        private static void ManualMappingInline()
        {
            Product entity = null;
            using (var context = new DemoContext())
            {
                entity = context.Products
                    .Include(p => p.Storages)
                    .First();
            }

            var productDto = new ProductDto()
            {
                Name = entity.Name,
                InStorageCount = entity.Storages
                    .Sum(ps => ps.Quantity)
            };

            Console.WriteLine($"{productDto.Name}: {productDto.InStorageCount}");
        }
    }
}