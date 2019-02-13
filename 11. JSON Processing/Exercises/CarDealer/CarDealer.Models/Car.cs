namespace CarDealer.Models
{
    using System.Collections.Generic;

    public class Car
    {
        private ICollection<Sale> sales;
        private ICollection<PartCar> parts;

        public Car()
        {
            this.sales = new HashSet<Sale>();
            this.parts = new HashSet<PartCar>();
        }

        public int Id { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public long TravelledDistance { get; set; }

        public virtual ICollection<Sale> Sales
        {
            get => this.sales;
            set => this.sales = value;
        }

        public virtual ICollection<PartCar> Parts
        {
            get => this.parts;
            set => this.parts = value;
        }
    }
}