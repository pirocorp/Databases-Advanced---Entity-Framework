namespace CarDealer.Models
{
    using System.Collections.Generic;

    public class Part
    {
        private ICollection<PartCar> cars;

        public Part()
        {
            this.cars = new HashSet<PartCar>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public int SupplierId { get; set; }
        public virtual  Supplier Supplier { get; set; }

        public virtual ICollection<PartCar> Cars
        {
            get => this.cars;
            set => this.cars = value;
        }
    }
}