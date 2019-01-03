namespace P03_ShoppingSpree
{
    using System;

    public static class Validator
    {
        public static void ValidateName(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException($"Name cannot be empty");
            }
        }

        public static void ValidateMoney(decimal value)
        {
            if (value < 0)
            {
                throw new ArgumentException($"Money cannot be negative");
            }
        }

        public static void ValidatePrice(decimal value)
        {
            if (value <= 0)
            {
                throw new ArgumentException($"Price cannot be zero or negative");
            }
        }
    }
}