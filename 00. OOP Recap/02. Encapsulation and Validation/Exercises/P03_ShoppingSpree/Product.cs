namespace P03_ShoppingSpree
{
    public class Product
    {
        private string name;
        private decimal price;

        public Product(string name, decimal price)
        {
            this.Name = name;
            this.Price = price;
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

        public decimal Price
        {
            get => this.price;
            private set
            {
                Validator.ValidatePrice(value);
                this.price = value;
            }
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}