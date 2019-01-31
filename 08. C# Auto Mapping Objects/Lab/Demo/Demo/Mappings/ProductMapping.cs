namespace Demo.Mappings
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using DTOs;
    using Models;

    public static class ProductMapping
    {
        public static ProductDto MapToProductDto(Product product)
        {
            return new ProductDto()
            {
                Name = product.Name,
                InStorageCount = product.Storages
                    .Sum(ps => ps.Quantity)
            };
        }

        public static ProductDto ToProductDto(this Product product)
        {
            return new ProductDto()
            {
                Name = product.Name,
                InStorageCount = product.Storages
                    .Sum(ps => ps.Quantity)
            };
        }

        public static Expression<Func<Product, ProductDto>> ToProductDtoExpression()
        {
            return product => new ProductDto()
            {
                Name = product.Name,
                InStorageCount = product.Storages
                    .Sum(ps => ps.Quantity)
            };
        }
    }
}