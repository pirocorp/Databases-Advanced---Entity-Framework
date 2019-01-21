namespace P03_SalesDatabase.Data.Models
{
    using System.Collections.Generic;

    public class Store
    {
        private ICollection<Sale> sales;

        public Store()
        {
            this.sales = new HashSet<Sale>();
        }

        public int StoreId { get; set; }

        public string Name { get; set; }

        public ICollection<Sale> Sales
        {
            get => this.sales;
            set => this.sales = value;
        }
    }
}