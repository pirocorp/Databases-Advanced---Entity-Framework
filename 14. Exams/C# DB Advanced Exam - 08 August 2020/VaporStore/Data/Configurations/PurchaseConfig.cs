namespace VaporStore.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class PurchaseConfig : IEntityTypeConfiguration<Purchase>
    {
        public void Configure(EntityTypeBuilder<Purchase> builder)
        {
            builder.HasKey(p => p.Id);

            builder
                .Property(p => p.ProductKey)
                .IsRequired();

            builder
                .HasOne(p => p.Card)
                .WithMany(c => c.Purchases)
                .HasForeignKey(p => p.CardId);

            builder
                .HasOne(p => p.Game)
                .WithMany(g => g.Purchases)
                .HasForeignKey(p => p.GameId);
        }
    }
}
