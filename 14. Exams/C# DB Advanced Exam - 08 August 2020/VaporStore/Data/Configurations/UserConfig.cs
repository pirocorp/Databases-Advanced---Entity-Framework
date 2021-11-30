namespace VaporStore.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    using static Common.ValidationConstants.User;

    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder
                .Property(u => u.Username)
                .HasMaxLength(UsernameMaxLength)
                .IsRequired();

            builder
                .Property(u => u.FullName)
                .IsRequired();

            builder
                .Property(u => u.Email)
                .IsRequired();
        }
    }
}
