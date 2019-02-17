namespace TeamBuilder.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Enums;

    public class User
    {
        private ICollection<Team> createdTeams;
        private ICollection<Event> createdEvents;
        private ICollection<Invitation> invitations;
        private ICollection<UserTeam> teams;

        public User()
        {
            this.createdTeams = new HashSet<Team>();
            this.createdEvents = new HashSet<Event>();
            this.invitations = new HashSet<Invitation>();
            this.teams = new HashSet<UserTeam>();
        }

        public int Id { get; set; }

        [MinLength(3)]
        public string Username { get; set; }

        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        [MinLength(6)]
        public string Password { get; set; }

        public Gender Gender { get; set; }

        public int Age { get; set; }

        public bool IsDeleted { get; set; }

        public virtual ICollection<Team> CreatedTeams
        {
            get => this.createdTeams;
            set => this.createdTeams = value;
        }

        public virtual ICollection<Event> CreatedEvents
        {
            get => this.createdEvents;
            set => this.createdEvents = value;
        }

        public virtual ICollection<Invitation> Invitations
        {
            get => this.invitations;
            set => this.invitations = value;
        }

        public virtual ICollection<UserTeam> Teams
        {
            get => this.teams;
            set => this.teams = value;
        }
    }
}