namespace P01_BillsPaymentSystem.Data.ModelConfigs
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class BankAccountConfig : IEntityTypeConfiguration<BankAccount>
    {
        public void Configure(EntityTypeBuilder<BankAccount> builder)
        {
            builder
                .HasOne(b => b.PaymentMethod)
                .WithOne(p => p.BankAccount)
                .HasForeignKey<PaymentMethod>(p => p.BankAccountId);
        }
    }
}