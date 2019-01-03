namespace P03_ShoppingSpree
{
    using System;
    using System.Collections.Generic;

    public class Person
    {
        private string name;
        private decimal money;
        private readonly List<Product> products;

        public Person(string name, decimal money)
        {
            this.Name = name;
            this.Money = money;
            this.products = new List<Product>();
        }

        public string Name
        {
            get => this.name;
            private set
            {
                Validator.ValidateName(value);
                this.name = value;
            }
        }

        public decimal Money
        {
            get => this.money;
            private set
            {
                Validator.ValidateMoney(value);
                this.money = value;
            }
        }

        public void BuyProduct(Product product)
        {
            if (product.Price > this.Money)
            {
                throw new ArgumentException($"{this.Name} can't afford {product.Name}");
            }

            this.products.Add(product);
            this.money -= product.Price;
        }

        public override string ToString()
        {
            var serializedProducts = string.Empty;

            if (this.products.Count > 0)
            {
                serializedProducts = string.Join(", ", this.products);
            }
            else
            {
                serializedProducts = "Nothing bought";
            }

            return $"{this.Name} - {serializedProducts}";
        }
    }
}