namespace P01_BillsPaymentSystem.Data.ModelConfigs
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .HasMany(u => u.PaymentMethods)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId);
        }
    }
}