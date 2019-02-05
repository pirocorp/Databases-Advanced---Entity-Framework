namespace BusTicketsSystem.Models
{
    using System.Collections.Generic;

    public class BusCompany
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double Rating { get; set; }

        public int CountryId { get; set; }
        public Country Country { get; set; }

        public ICollection<Trip> Trips { get; set; }
    }
}