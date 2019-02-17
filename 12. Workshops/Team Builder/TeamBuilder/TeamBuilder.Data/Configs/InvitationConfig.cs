namespace TeamBuilder.Data.Configs
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;

    public class InvitationConfig : IEntityTypeConfiguration<Invitation>
    {
        public void Configure(EntityTypeBuilder<Invitation> builder)
        {
            builder.HasKey(i => i.Id);

            builder.HasOne(i => i.InvitedUser)
                .WithMany(u => u.Invitations)
                .HasForeignKey(i => i.InvitedUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(i => i.Team)
                .WithMany(t => t.Invitations)
                .HasForeignKey(i => i.TeamId);
        }
    }
}