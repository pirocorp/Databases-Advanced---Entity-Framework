namespace BusTicketsSystem.Models
{
    using System.Collections.Generic;

    public class BusStation
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int TownId { get; set; }
        public virtual Town Town { get; set; }

        public virtual ICollection<Trip> ExpectedArrivals { get; set; }

        public virtual ICollection<Trip> ExpectedDeparture { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }

        public virtual ICollection<ArrivedTrip> ActualArrivals { get; set; }

        public virtual ICollection<ArrivedTrip> ActualDeparture { get; set; }
    }
}