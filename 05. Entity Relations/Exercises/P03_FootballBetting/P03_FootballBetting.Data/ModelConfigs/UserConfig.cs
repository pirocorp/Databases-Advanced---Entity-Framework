namespace P03_FootballBetting.Data.ModelConfigs
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.UserId);

            builder
                .Property(u => u.Name)
                .IsUnicode()
                .HasMaxLength(100);

            builder
                .Property(u => u.Username)
                .IsRequired()
                .HasMaxLength(30);

            builder
                .Property(u => u.Password)
                .IsRequired()
                .HasMaxLength(30);

            builder
                .Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(20);
        }
    }
}