namespace FastFood.Data.Configs
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;

    public class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasOne(e => e.Employee)
                .WithMany(e => e.Orders)
                .HasForeignKey(e => e.EmployeeId);
        }
    }
}