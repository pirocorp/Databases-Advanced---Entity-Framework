namespace TeamBuilder.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Team
    {
        private ICollection<Invitation> invitations;
        private ICollection<UserTeam> users;
        private ICollection<TeamEvent> events;

        public Team()
        {
            this.invitations = new HashSet<Invitation>();
            this.users = new HashSet<UserTeam>();
            this.events = new HashSet<TeamEvent>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        [MinLength(3)]
        public string Acronym { get; set; }

        public int CreatorId { get; set; }
        public virtual User Creator { get; set; }

        public virtual ICollection<Invitation> Invitations
        {
            get => this.invitations;
            set => this.invitations = value;
        }

        public virtual ICollection<UserTeam> Users
        {
            get => this.users;
            set => this.users = value;
        }

        public virtual ICollection<TeamEvent> Events
        {
            get => this.events;
            set => this.events = value;
        }
    }
}