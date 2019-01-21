namespace P03_SalesDatabase.Data.EntityConfiguration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;

    public class StoreConfig : IEntityTypeConfiguration<Store>
    {
        public void Configure(EntityTypeBuilder<Store> builder)
        {
            builder.HasKey(s => s.StoreId);

            builder.Property(s => s.StoreId)
                .HasColumnName("StoreID");

            builder.Property(s => s.Name)
                .HasMaxLength(80)
                .IsUnicode();

            builder.HasMany(st => st.Sales)
                .WithOne(sa => sa.Store)
                .HasForeignKey(sa => sa.StoreId);
        }
    }
}