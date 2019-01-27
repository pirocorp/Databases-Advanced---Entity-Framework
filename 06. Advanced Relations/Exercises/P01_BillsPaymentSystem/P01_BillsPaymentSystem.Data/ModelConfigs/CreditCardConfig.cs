namespace P01_BillsPaymentSystem.Data.ModelConfigs
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;

    public class CreditCardConfig : IEntityTypeConfiguration<CreditCard>
    {
        public void Configure(EntityTypeBuilder<CreditCard> builder)
        {
            builder
                .HasOne(c => c.PaymentMethod)
                .WithOne(p => p.CreditCard)
                .HasForeignKey<PaymentMethod>(p => p.CreditCardId);
        }
    }
}