namespace BookShopSystem.Data.ModelConfigs
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class BookRelatedBookConfig : IEntityTypeConfiguration<BookRelatedBook>
    {
        public void Configure(EntityTypeBuilder<BookRelatedBook> builder)
        {
            builder.HasKey(brb => new {brb.BookId, brb.RelatedId});

            builder
                .HasOne(brb => brb.Book)
                .WithMany(b => b.RelatedBooks)
                .HasForeignKey(brb => brb.BookId);

            builder
                .HasOne(brb => brb.RelatedBook)
                .WithMany()
                .HasForeignKey(brb => brb.RelatedId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}