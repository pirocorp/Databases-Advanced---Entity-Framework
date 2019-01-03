namespace P04_PizzaCalories
{
    using System;

    public class StartUp
    {
        public static void Main()
        {
            var inputPizzaArgs = Console.ReadLine().Split(new[] {" "}, StringSplitOptions.RemoveEmptyEntries);
            var pizzaName = inputPizzaArgs[1];

            Pizza pizza = null;

            try
            {
                pizza = new Pizza(pizzaName);
            }
            catch (ArgumentException ae)
            {
                Console.WriteLine(ae.Message);
                return;
            }

            var inputDoughArgs = Console.ReadLine().Split(new[] {" "}, StringSplitOptions.RemoveEmptyEntries);
            var flourType = inputDoughArgs[1];
            var bakingTechnique = inputDoughArgs[2];
            var weightDoughInGrams = decimal.Parse(inputDoughArgs[3]);

            Dough dough = null;

            try
            {
                dough = new Dough(flourType, bakingTechnique, weightDoughInGrams);
            }
            catch (ArgumentException ae)
            {
                Console.WriteLine(ae.Message);
                return;
            }

            pizza.SetDough(dough);

            var input = string.Empty;

            while ((input = Console.ReadLine()) != "END")
            {
                var inputToppingArgs = input.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                var toppingType = inputToppingArgs[1];
                var weightToppingInGrams = decimal.Parse(inputToppingArgs[2]);
                
                try
                {
                    var topping = new Topping(toppingType, weightToppingInGrams);
                    pizza.AddTopping(topping);
                }
                catch (ArgumentException ae)
                {
                    Console.WriteLine(ae.Message);
                    return;
                }
            }

            Console.WriteLine(pizza);
        }
    }
}