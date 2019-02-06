namespace BusTicketsSystem.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;

    public class BankAccountConfig : IEntityTypeConfiguration<BankAccount>
    {
        public void Configure(EntityTypeBuilder<BankAccount> builder)
        {
            builder.HasKey(e => new {e.Id});

            builder.HasOne(e => e.Customer)
                .WithMany(c => c.BankAccounts)
                .HasForeignKey(e => e.CustomerId);

            builder.HasIndex(e => e.AccountNumber)
                .IsUnique();

            builder.Property(e => e.AccountNumber)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.Balance)
                .HasDefaultValue(0);
        }
    }
}