namespace Demo.Data.ModelsConfig
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.ProductId);
        }
    }
}