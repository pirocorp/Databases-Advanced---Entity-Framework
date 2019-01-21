namespace P03_SalesDatabase.Data.Models
{
    using System.Collections.Generic;

    public class Product
    {
        private ICollection<Sale> sales;

        public Product()
        {
            this.sales = new HashSet<Sale>();
        }

        public int ProductId { get; set; }

        public string Name { get; set; }

        public decimal Quantity { get; set; }

        public decimal Price { get; set; }

        public ICollection<Sale> Sales
        {
            get => this.sales;
            set => this.sales = value;
        }
    }
}