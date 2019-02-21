namespace FastFood.Data.Configs
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;

    public class PositionConfig : IEntityTypeConfiguration<Position>
    {
        public void Configure(EntityTypeBuilder<Position> builder)
        {
            builder.HasKey(e => e.Id);

            //If you just want to enforce uniqueness of a column then you want a
            //unique index rather than an alternate key. In EF, alternate keys provide
            //greater functionality than unique indexes because they can be used as the
            //target of a foreign key.
            builder.HasIndex(e => e.Name)
                .IsUnique();
        }
    }
}