namespace VaporStore.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    using static Common.ValidationConstants.Card;

    public class CardConfig : IEntityTypeConfiguration<Card>
    {
        public void Configure(EntityTypeBuilder<Card> builder)
        {
            builder.HasKey(c => c.Id);

            builder
                .Property(c => c.Number)
                .IsRequired();

            builder
                .Property(c => c.Cvc)
                .HasMaxLength(CvcLength)
                .IsRequired();

            builder
                .HasOne(c => c.User)
                .WithMany(u => u.Cards)
                .HasForeignKey(c => c.UserId);
        }
    }
}
