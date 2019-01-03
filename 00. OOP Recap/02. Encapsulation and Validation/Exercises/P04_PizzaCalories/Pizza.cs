namespace P04_PizzaCalories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Pizza
    {
        private string name;
        private Dough dough;
        private readonly List<Topping> toppings;

        public Pizza(string name)
        {
            this.Name = name;
            this.toppings = new List<Topping>();
        }
        
        public string Name
        {
            get => this.name;
            private set
            {
                if (string.IsNullOrEmpty(value) && value.Length <= 15)
                {
                    throw new AggregateException($"Pizza name should be between 1 and 15 symbols.");
                }

                this.name = value;
            }
        }

        public int ToppingsCount => this.toppings.Count;

        public decimal TotalCalories => this.dough.TotalCalories + this.toppings.Sum(x => x.TotalCalories);

        public void AddTopping(Topping topping)
        {
            if (this.ToppingsCount == 10)
            {
                throw new ArgumentException($"Number of toppings should be in range [0..10].");
            }

            this.toppings.Add(topping);
        }

        public void SetDough(Dough inputDough)
        {
            this.dough = inputDough;
        }

        public override string ToString()
        {
            return $"{this.Name} - {this.TotalCalories:F2} Calories.";
        }
    }
}