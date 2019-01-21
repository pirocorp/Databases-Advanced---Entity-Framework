namespace P03_SalesDatabase.Data.EntityConfiguration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;

    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.ProductId);

            builder.Property(p => p.ProductId)
                .HasColumnName("ProductID");

            builder.Property(p => p.Name)
                .HasMaxLength(50)
                .IsUnicode();

            builder.Property(p => p.Description)
                .HasMaxLength(250)
                .HasDefaultValue("No description");

            builder.HasMany(p => p.Sales)
                .WithOne(s => s.Product)
                .HasForeignKey(s => s.ProductId);
        }
    }
}