namespace ProductShop.Models
{
    using System.Collections.Generic;

    public class Category
    {
        private ICollection<CategoryProduct> products;

        public Category()
        {
            this.products = new HashSet<CategoryProduct>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<CategoryProduct> Products
        {
            get => this.products;
            set => this.products = value;
        }
    }
}