namespace P07_Animals
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Animals;

    public class StartUp
    {
        public static void Main()
        {
            var animals = new List<Animal>();
            var animalType = string.Empty;

            while ((animalType = Console.ReadLine()) != "Beast!")
            {
                var inputArgs = Console.ReadLine().Split(new[] {" "}, StringSplitOptions.RemoveEmptyEntries);
                var animalName = inputArgs[0];
                var age = int.Parse(inputArgs[1]);
                string gender = null;

                if (animalType != "Kittens" || animalType != "Tomcats")
                {
                    gender = inputArgs[2];
                }

                var type = Assembly.GetExecutingAssembly()
                    .GetTypes()
                    .Where(x => x.Namespace.Contains("Animals.Animals"))
                    .FirstOrDefault(x => x.Name == animalType);

                Animal currentAnimal = null;

                if (animalType != "Kittens" || animalType != "Tomcats")
                {
                    try
                    {
                        currentAnimal = (Animal) Activator.CreateInstance(type, animalName, age, gender);
                    }
                    catch (TargetInvocationException tie)
                    {
                        Console.WriteLine(tie.InnerException.Message);
                    }
                }
                else
                {
                    try
                    {
                        currentAnimal = (Animal) Activator.CreateInstance(type, animalName, age);
                    }
                    catch (TargetInvocationException tie)
                    {
                        Console.WriteLine(tie.InnerException.Message);
                    }
                }

                if (currentAnimal != null)
                {
                    animals.Add(currentAnimal);
                }
            }

            foreach (var animal in animals)
            {
                Console.WriteLine(animal.GetType().Name);
                Console.WriteLine(animal.ToString());
                Console.WriteLine(animal.ProduceSound());
            }
        }
    }
}
