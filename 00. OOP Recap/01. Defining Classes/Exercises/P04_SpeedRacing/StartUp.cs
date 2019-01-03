namespace P04_SpeedRacing
{
    using System;
    using System.Collections.Generic;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            var cars = new Dictionary<string, Car>();
            var n = int.Parse(Console.ReadLine());

            for (var i = 0; i < n; i++)
            {
                var inputArgs = Console.ReadLine().Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
                var model = inputArgs[0];
                var fuelAmount = decimal.Parse(inputArgs[1]);
                var consumption = decimal.Parse(inputArgs[2]);

                var currentCar = new Car(model, fuelAmount, consumption);
                cars.Add(model, currentCar);
            }

            var input = string.Empty;

            while ((input = Console.ReadLine()) != "End")
            {
                var inputArgs = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                ;
                var model = inputArgs[1];
                var distance = decimal.Parse(inputArgs[2]);

                try
                {
                    cars[model].Drive(distance);
                }
                catch (ArgumentException ae)
                {
                    Console.WriteLine(ae.Message);
                }
            }

            foreach (var car in cars.Values)
            {
                Console.WriteLine(car);
            }
        }
    }
}
