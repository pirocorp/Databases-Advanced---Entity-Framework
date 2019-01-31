namespace Demo.Data.ModelsConfig
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;

    public class ProductStockConfig : IEntityTypeConfiguration<ProductStock>
    {
        public void Configure(EntityTypeBuilder<ProductStock> builder)
        {
            builder.HasKey(ps => new {ps.ProductId, ps.StorageId});

            builder.HasOne(ps => ps.Product)
                .WithMany(p => p.Storages)
                .HasForeignKey(ps => ps.ProductId);

            builder.HasOne(ps => ps.Storage)
                .WithMany(s => s.Products)
                .HasForeignKey(ps => ps.StorageId);
        }
    }
}