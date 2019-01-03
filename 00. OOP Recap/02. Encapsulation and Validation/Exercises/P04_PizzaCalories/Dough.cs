namespace P04_PizzaCalories
{
    using System;

    public class Dough
    {
        private const decimal BASE_CALORIES_PER_GRAM = 2;

        private decimal weightInGrams;
        private string flourType;
        private string bakingTechnique;

        public Dough(string flourType, string bakingTechnique, decimal weightInGrams)
        {
            this.FlourType = flourType;
            this.BakingTechnique = bakingTechnique;
            this.WeightInGrams = weightInGrams;
        }

        public decimal TotalCalories => this.WeightInGrams * this.RealCaloriesPerGram;

        private decimal RealCaloriesPerGram =>
            BASE_CALORIES_PER_GRAM * this.FlourTypeModifier() * this.BakingTechniqueModifier();

        private string FlourType
        {
            get => this.flourType;
            set
            {
                if (value != "White" && value != "Wholegrain")
                {
                    throw new ArgumentException($"Invalid type of dough.");
                }

                this.flourType = value;
            }
        }

        private string BakingTechnique
        {
            get => this.bakingTechnique;
            set
            {
                if (value != "Crispy" && value != "Chewy" && value != "Homemade")
                {
                    throw new ArgumentException($"Invalid baking technique.");
                }

                this.bakingTechnique = value;
            }
        }

        private decimal WeightInGrams
        {
            get => this.weightInGrams;
            set
            {
                if (value < 1 || value > 200)
                {
                    throw new ArgumentException($"Dough weight should be in the range [1..200].");
                }

                this.weightInGrams = value;
            }
        }

        private decimal FlourTypeModifier()
        {
            var result = 0.0M;

            switch (this.FlourType)
            {
                case "White":
                    result = 1.5M;
                    break;
                case "Wholegrain":
                    result = 1.0M;
                    break;
            }

            return result;
        }
        
        private decimal BakingTechniqueModifier()
        {
            var result = 0.0M;

            switch (this.BakingTechnique)
            {
                case "Crispy":
                    result = 0.9M;
                    break;
                case "Chewy":
                    result = 1.1M;
                    break;
                case "Homemade":
                    result = 1.0M;
                    break;
            }

            return result;
        }
    }
}