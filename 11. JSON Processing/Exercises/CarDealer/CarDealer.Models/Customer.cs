namespace CarDealer.Models
{
    using System;
    using System.Collections.Generic;

    public class Customer
    {
        private ICollection<Sale> sales;

        public Customer()
        {
            this.sales = new HashSet<Sale>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime BirthDate { get; set; }

        public bool IsYoungDriver { get; set; }

        public virtual ICollection<Sale> Sales
        {
            get => this.sales;
            set => this.sales = value;
        }
    }
}