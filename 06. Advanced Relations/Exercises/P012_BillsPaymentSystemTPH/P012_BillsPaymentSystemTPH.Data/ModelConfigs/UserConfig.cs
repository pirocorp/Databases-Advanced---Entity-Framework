namespace P012_BillsPaymentSystemTPH.Data.ModelConfigs
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
                .WithOne(pm => pm.User)
                .HasForeignKey(pm => pm.UserId);
        }
    }
}