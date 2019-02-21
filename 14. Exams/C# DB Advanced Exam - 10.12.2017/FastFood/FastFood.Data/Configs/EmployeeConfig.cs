namespace FastFood.Data.Configs
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;

    public class EmployeeConfig : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasOne(e => e.Position)
                .WithMany(p => p.Employees)
                .HasForeignKey(e => e.PositionId);
        }
    }
}