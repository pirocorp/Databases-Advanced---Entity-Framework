namespace TeamBuilder.Data.Configs
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;

    public class UserTeamConfig : IEntityTypeConfiguration<UserTeam>
    {
        public void Configure(EntityTypeBuilder<UserTeam> builder)
        {
            builder.HasKey(ut => new {ut.UserId, ut.TeamId});

            builder.HasOne(ut => ut.User)
                .WithMany(u => u.Teams)
                .HasForeignKey(ut => ut.UserId);

            builder.HasOne(ut => ut.Team)
                .WithMany(t => t.Users)
                .HasForeignKey(ut => ut.TeamId);
        }
    }
}