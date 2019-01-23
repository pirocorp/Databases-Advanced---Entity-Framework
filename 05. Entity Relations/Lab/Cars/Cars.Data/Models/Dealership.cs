namespace Cars.Data.Models
{
    using System.Collections.Generic;

    public class Dealership
    {
        public Dealership()
        {
            this.CarDealerships = new HashSet<CarDealership>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<CarDealership> CarDealerships { get; set; }
    }
}