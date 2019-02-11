namespace ProductShop.Models
{
    using System.Collections.Generic;

    public class User
    {
        private ICollection<Product> boughtProducts;
        private ICollection<Product> soldProducts;

        public User()
        {
            this.boughtProducts = new HashSet<Product>();
            this.soldProducts = new HashSet<Product>();
        }

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int? Age { get; set; }

        public virtual ICollection<Product> BoughtProducts
        {
            get => this.boughtProducts;
            set => this.boughtProducts = value;
        }

        public virtual ICollection<Product> SoldProducts
        {
            get => this.soldProducts;
            set => this.soldProducts = value;
        }
    }
}