namespace Theatre.Data.Models
{
    using System;
    using System.Collections.Generic;
    using Enums;

    public class Play
    {
        public Play()
        {
            this.Casts = new List<Cast>();
            this.Tickets = new List<Ticket>();
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public TimeSpan Duration { get; set; }

        public float Rating { get; set; }

        public Genre Genre { get; set; }

        public string Description { get; set; }

        public string Screenwriter { get; set; }

        public ICollection<Cast> Casts { get; set; }

        public ICollection<Ticket> Tickets { get; set; }
    }
}
