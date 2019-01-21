namespace P03_SalesDatabase.Data.EntityConfiguration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class SaleConfig : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.HasKey(s => s.SaleId);

            builder.Property(s => s.SaleId)
                .HasColumnName("SaleID");

            builder.Property(s => s.CustomerId)
                .HasColumnName("CustomerID");

            builder.Property(s => s.ProductId)
                .HasColumnName("ProductID");

            builder.Property(s => s.StoreId)
                .HasColumnName("StoreID");
        }
    }
}