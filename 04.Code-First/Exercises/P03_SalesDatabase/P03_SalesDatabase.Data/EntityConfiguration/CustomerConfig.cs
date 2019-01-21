namespace P03_SalesDatabase.Data.EntityConfiguration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class CustomerConfig : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(c => c.CustomerId);

            builder.Property(c => c.CustomerId)
                .HasColumnName("CustomerID");

            builder.Property(c => c.Name)
                .HasMaxLength(100)
                .IsUnicode();

            builder.Property(c => c.Email)
                .HasMaxLength(80)
                .IsUnicode(false);

            builder.HasMany(c => c.Sales)
                .WithOne(s => s.Customer)
                .HasForeignKey(s => s.CustomerId);
        }
    }
}