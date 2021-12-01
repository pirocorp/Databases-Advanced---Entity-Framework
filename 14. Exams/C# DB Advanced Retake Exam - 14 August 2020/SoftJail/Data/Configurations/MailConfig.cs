namespace SoftJail.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class MailConfig : IEntityTypeConfiguration<Mail>
    {
        public void Configure(EntityTypeBuilder<Mail> builder)
        {
            builder
                .HasKey(m => m.Id);

            builder
                .Property(m => m.Description)
                .IsRequired();

            builder
                .Property(m => m.Sender )
                .IsRequired();

            builder
                .Property(m => m.Address )
                .IsRequired();

            builder
                .HasOne(m => m.Prisoner)
                .WithMany(p => p.Mails)
                .HasForeignKey(m => m.PrisonerId);
        }
    }
}
