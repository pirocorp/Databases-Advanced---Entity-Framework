namespace BusTicketsSystem.Models
{
    using System.Collections.Generic;

    public class BusStation
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int TownId { get; set; }
        public virtual Town Town { get; set; }

        public virtual ICollection<Trip> Arrivals { get; set; }

        public virtual ICollection<Trip> Departure { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }
    }
}