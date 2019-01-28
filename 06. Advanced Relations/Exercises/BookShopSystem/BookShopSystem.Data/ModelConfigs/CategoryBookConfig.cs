namespace BookShopSystem.Data.ModelConfigs
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class CategoryBookConfig : IEntityTypeConfiguration<CategoryBook>
    {
        public void Configure(EntityTypeBuilder<CategoryBook> builder)
        {
            builder.HasKey(cb => new {cb.BookId, cb.CategoryId});

            builder
                .HasOne(cb => cb.Book)
                .WithMany(b => b.Categories)
                .HasForeignKey(cb => cb.BookId);

            builder
                .HasOne(cb => cb.Category)
                .WithMany(c => c.Books)
                .HasForeignKey(cb => cb.CategoryId);
        }
    }
}