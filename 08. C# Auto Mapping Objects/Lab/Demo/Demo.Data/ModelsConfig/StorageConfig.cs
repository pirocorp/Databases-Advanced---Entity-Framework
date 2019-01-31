namespace Demo.Data.ModelsConfig
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;

    public class StorageConfig : IEntityTypeConfiguration<Storage>
    {
        public void Configure(EntityTypeBuilder<Storage> builder)
        {
            builder.HasKey(s => s.StorageId);
        }
    }
}