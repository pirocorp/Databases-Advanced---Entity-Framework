namespace BusTicketsSystem.Models
{
    using System.Collections.Generic;

    public class Country
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<BusCompany> BusCompanies { get; set; }

        public ICollection<Town> Towns { get; set; }
    }
}