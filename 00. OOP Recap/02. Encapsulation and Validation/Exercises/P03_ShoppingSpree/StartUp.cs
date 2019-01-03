namespace P03_ShoppingSpree
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Reflection;

    public class StartUp
    {
        public static void Main()
        {
            var persons = new Dictionary<string, Person>();
            var products =  new Dictionary<string, Product>();

            var personsInput = Console.ReadLine().Split(new[] {";"}, StringSplitOptions.RemoveEmptyEntries);
            var productsInput = Console.ReadLine().Split(new[] {";"}, StringSplitOptions.RemoveEmptyEntries);

            ReadInput(personsInput, persons);
            ReadInput(productsInput, products);

            var input = string.Empty;

            while ((input = Console.ReadLine()) != "END")
            {
                var tokens = input.Split(new[] {" "}, StringSplitOptions.RemoveEmptyEntries);
                var personName = tokens[0];

                if (!persons.ContainsKey(personName))
                {
                    continue;
                }

                var productName = tokens[1];
                var person = persons[personName];
                var product = products[productName];

                try
                {
                    person.BuyProduct(product);
                    Console.WriteLine($"{person.Name} bought {product.Name}");
                }
                catch (ArgumentException ae)
                {
                    Console.WriteLine(ae.Message);
                }
            }

            foreach (var person in persons.Values)
            {
                Console.WriteLine(person);
            }
        }

        private static void ReadInput<T>(string[] personsInput, Dictionary<string, T> persons)
        {
            foreach (var input in personsInput)
            {
                var personArgs = input.Split(new[] {"="}, StringSplitOptions.None);
                var personName = personArgs[0];
                var personMoney = decimal.Parse(personArgs[1]);

                try
                {
                    var person =(T)Activator.CreateInstance(typeof(T), personName, personMoney);
                    persons.Add(personName, person);

                }
                catch (TargetInvocationException tie)
                {
                    Console.WriteLine(tie.InnerException.Message);
                }
            }
        }
    }
}