namespace BusTicketsSystem.Models
{
    using System.Collections.Generic;

    public class Town
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int CountryId { get; set; }
        public Country Country { get; set; }

        public ICollection<Customer> Customers { get; set; }

        public ICollection<BusStation> BusStations { get; set; }
    }
}