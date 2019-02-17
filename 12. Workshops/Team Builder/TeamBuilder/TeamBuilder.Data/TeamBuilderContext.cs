namespace TeamBuilder.Data
{
    using Microsoft.EntityFrameworkCore;

    using Configs;
    using Models;

    public class TeamBuilderContext : DbContext
    {
        public TeamBuilderContext()
        {
        }

        public TeamBuilderContext(DbContextOptions options) 
            : base(options)
        {
        }

        public DbSet<Event> Events { get; set; }

        public DbSet<Invitation> Invitations { get; set; }

        public DbSet<Team> Teams { get; set; }

        public DbSet<TeamEvent> TeamEvents { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<UserTeam> UserTeams { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=TeamBuilder;Integrated Security=True;");
        //    }
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EventConfig());
            modelBuilder.ApplyConfiguration(new InvitationConfig());
            modelBuilder.ApplyConfiguration(new TeamConfig());
            modelBuilder.ApplyConfiguration(new TeamEventConfig());
            modelBuilder.ApplyConfiguration(new UserConfig());
            modelBuilder.ApplyConfiguration(new UserTeamConfig());
        }
    }
}