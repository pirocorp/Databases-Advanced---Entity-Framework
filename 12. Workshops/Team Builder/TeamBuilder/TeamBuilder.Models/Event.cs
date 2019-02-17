namespace TeamBuilder.Models
{
    using System;
    using System.Collections.Generic;

    public class Event
    {
        private ICollection<TeamEvent> teams;

        public Event()
        {
            this.teams = new HashSet<TeamEvent>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int CreatorId { get; set; }
        public virtual User Creator { get; set; }

        public virtual ICollection<TeamEvent> Teams
        {
            get => this.teams;
            set => this.teams = value;
        }
    }
}