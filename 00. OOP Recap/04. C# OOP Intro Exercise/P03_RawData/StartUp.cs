namespace P03_RawData
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class StartUp
    {
        public static void Main()
        {
            var n = int.Parse(Console.ReadLine());
            var cars = new List<Car>();

            for (var i = 0; i < n; i++)
            {
                var car = Car.Parse(Console.ReadLine());
                cars.Add(car);
            }

            Car[] result = null;
            var command = Console.ReadLine();

            if (command == "fragile")
            {
                result = cars
                    .Where(x => x.Cargo.Type == "fragile" && 
                                x.Tires.Any(t => t.Pressure < 1))
                    .ToArray();
            }
            else if (command == "flammable")
            {
                result = cars
                    .Where(x => x.Cargo.Type == "flammable" &&
                                         x.Engine.Power > 250)
                    .ToArray();
            }

            foreach (var car in result)
            {
                Console.WriteLine(car.Model);
            }
        }
    }
}
