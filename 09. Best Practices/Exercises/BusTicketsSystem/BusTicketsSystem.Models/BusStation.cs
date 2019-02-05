namespace BusTicketsSystem.Models
{
    using System.Collections.Generic;

    public class BusStation
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int TownId { get; set; }
        public Town Town { get; set; }

        public ICollection<Trip> TripsStartsFromHere { get; set; }

        public ICollection<Trip> TripsToHere { get; set; }

        public ICollection<Review> Reviews { get; set; }
    }
}