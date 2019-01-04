namespace P02_BookShop
{
    using System;
    using System.Linq;

    public class Book
    {
        private string author;
        private string title;
        private decimal price;

        public Book(string author, string title, decimal price)
        {
            this.Author = author;
            this.Title = title;
            this.Price = price;
        }
        
        public string Author
        {
            get => this.author;
            protected set
            {
                var secondName = value.Split(new[] {" "}, StringSplitOptions.RemoveEmptyEntries).Skip(1).First();
                if (char.IsDigit(secondName[0]))
                {
                    throw new ArgumentException("Author not valid!");
                }

                this.author = value;
            }
        }

        public string Title
        {
            get => this.title;
            protected set
            {
                if (value.Length < 3)
                {
                    throw new ArgumentException("Title not valid!");
                }

                this.title = value;
            }
        }

        public decimal Price
        {
            get => this.price;
            protected set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Price not valid!");
                }

                this.price = value;
            }
        }

        public override string ToString()
        {
            return $"Type: {this.GetType().Name}" + Environment.NewLine +
                   $"Title: {this.Title}" + Environment.NewLine +
                   $"Author: {this.Author}" + Environment.NewLine +
                   $"Price: {this.Price:f2}";
        }
    }
}