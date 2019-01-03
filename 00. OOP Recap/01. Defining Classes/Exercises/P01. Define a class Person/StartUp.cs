﻿namespace P01._Define_a_class_Person
{
    using System;
    using System.Reflection;

    public class StartUp
    {
        public static void Main()
        {
            //Problem 1
            //var personType = typeof(Person);
            //var properties = personType.GetProperties
            //    (BindingFlags.Public | BindingFlags.Instance);

            //Console.WriteLine(properties.Length);

            //var person1 = new Person { Name = "Pesho", Age = 20};
            //var person2 = new Person { Name = "Gosho", Age = 18 };
            //var person3 = new Person();
            //person3.Name = "Stamat";
            //person3.Age = 43;

            //Problem 2
            var personType = typeof(Person);
            var emptyCtor = personType.GetConstructor(new Type[] { });
            var ageCtor = personType.GetConstructor(new[] { typeof(int) });
            var nameAgeCtor = personType.GetConstructor(new[] { typeof(string), typeof(int) });
            var swapped = false;
            if (nameAgeCtor == null)
            {
                nameAgeCtor = personType.GetConstructor(new[] { typeof(int), typeof(string) });
                swapped = true;
            }

            var name = Console.ReadLine();
            var age = int.Parse(Console.ReadLine());

            var basePerson = (Person)emptyCtor.Invoke(new object[] { });
            var personWithAge = (Person)ageCtor.Invoke(new object[] { age });
            var personWithAgeAndName = swapped ? (Person)nameAgeCtor.Invoke(new object[] { age, name }) : (Person)nameAgeCtor.Invoke(new object[] { name, age });

            Console.WriteLine("{0} {1}", basePerson.Name, basePerson.Age);
            Console.WriteLine("{0} {1}", personWithAge.Name, personWithAge.Age);
            Console.WriteLine("{0} {1}", personWithAgeAndName.Name, personWithAgeAndName.Age);
        }
    }
}
