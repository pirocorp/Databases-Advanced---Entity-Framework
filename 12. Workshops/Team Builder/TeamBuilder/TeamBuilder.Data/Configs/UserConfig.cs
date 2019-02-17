namespace TeamBuilder.Data.Configs
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;

    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Username)
                .HasMaxLength(25)
                .IsRequired();

            builder.HasIndex(u => u.Username)
                .IsUnique();

            builder.Property(u => u.FirstName)
                .HasMaxLength(25);

            builder.Property(u => u.LastName)
                .HasMaxLength(25);

            builder.Property(u => u.Password)
                .HasMaxLength(30)
                .IsRequired();
        }
    }
}