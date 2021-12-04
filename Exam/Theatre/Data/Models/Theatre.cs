namespace Theatre.Data.Models
{
    using System.Collections.Generic;

    public class Theatre
    {
        public Theatre()
        {
            this.Tickets = new List<Ticket>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public sbyte NumberOfHalls { get; set; }

        public string Director { get; set; }

        public ICollection<Ticket> Tickets { get; set; }
    }
}
