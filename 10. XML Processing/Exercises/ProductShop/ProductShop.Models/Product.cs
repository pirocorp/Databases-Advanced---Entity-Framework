namespace ProductShop.Models
{
    using System.Collections.Generic;

    public class Product
    {
        private ICollection<CategoryProduct> categories;

        public Product()
        {
            this.categories = new HashSet<CategoryProduct>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int? BuyerId { get; set; }
        public virtual User Buyer { get; set; }

        public int SellerId { get; set; }
        public virtual User Seller { get; set; }

        public virtual ICollection<CategoryProduct> Categories
        {
            get => this.categories;
            set => this.categories = value;
        }
    }
}