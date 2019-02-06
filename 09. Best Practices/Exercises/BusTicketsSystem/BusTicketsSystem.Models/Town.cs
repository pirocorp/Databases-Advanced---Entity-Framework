namespace BusTicketsSystem.Models
{
    using System.Collections.Generic;

    public class Town
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int CountryId { get; set; }
        public virtual Country Country { get; set; }

        public virtual ICollection<Customer> Customers { get; set; }

        public virtual ICollection<BusStation> BusStations { get; set; }
    }
}