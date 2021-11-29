namespace TeisterMask.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    using static Common.ValidationConstants.Employee;

    public class EmployeeConfig : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder
                .HasKey(e => e.Id);

            builder
                .Property(e => e.Username)
                .HasMaxLength(UsernameMaxLength)
                .IsRequired();

            builder
                .Property(e => e.Email)
                .IsRequired();

            builder
                .Property(e => e.Phone)
                .IsRequired();
        }
    }
}
