﻿namespace P04_AnimalFarm
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;

    public class StartUp
    {
        public static void Main()
        {
            var chickenType = typeof(Chicken);
            var fields = chickenType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            var methods = chickenType.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance);
            Debug.Assert(fields.Count(f => f.IsPrivate) == 2);
            Debug.Assert(methods.Count(m => m.IsPrivate) == 1);

            var name = Console.ReadLine();
            var age = int.Parse(Console.ReadLine());
            Chicken chicken = null;

            try
            {
                chicken = new Chicken(name, age);
            }
            catch (ArgumentException ae)
            {
                Console.WriteLine(ae.Message);
                return;
            }

            Console.WriteLine(
                "Chicken {0} (age {1}) can produce {2} eggs per day.",
                chicken.Name,
                chicken.Age,
                chicken.GetProductPerDay());
        }
    }
}
