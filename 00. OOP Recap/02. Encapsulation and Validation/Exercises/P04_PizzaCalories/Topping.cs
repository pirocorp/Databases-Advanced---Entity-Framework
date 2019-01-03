namespace P04_PizzaCalories
{
    using System;
    using System.Linq;

    public class Topping
    {
        private const decimal BASE_CALORIES_PER_GRAM = 2;

        private readonly string[] allowedToppings = {"Meat", "Veggies", "Cheese", "Sauce"};
        private string toppingType;
        private decimal weightInGrams;

        public Topping(string toppingType, decimal weightInGrams)
        {
            this.ToppingType = toppingType;
            this.WeightInGrams = weightInGrams;
        }

        public decimal TotalCalories => this.WeightInGrams * this.RealCaloriesPerGram;

        private string ToppingType
        {
            get => this.toppingType;
            set
            {
                if (this.allowedToppings.All(x => x != value))
                {
                    throw new ArgumentException($"Cannot place {value} on top of your pizza.");
                }

                this.toppingType = value;
            }
        }

        private decimal WeightInGrams
        {
            get => this.weightInGrams;
            set
            {
                if (value < 1 || value > 50)
                {
                    throw new ArgumentException($"{this.ToppingType} weight should be in the range [1..50].");
                }

                this.weightInGrams = value;
            }
        }

        private decimal RealCaloriesPerGram => BASE_CALORIES_PER_GRAM * this.ToppingModifier();

        private decimal ToppingModifier()
        {
            var result = 0.0M;

            switch (this.ToppingType)
            {
                case "Meat":
                    result =1.2M;
                    break;
                case "Veggies":
                    result = 0.8M;
                    break;
                case "Cheese":
                    result = 1.1M;
                    break;
                case "Sauce":
                    result = 0.9M;
                    break;
            }

            return result;
        }
    }
}