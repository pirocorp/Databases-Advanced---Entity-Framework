namespace P01._Define_a_class_Person
{
    using System;
    using System.Reflection;

    public class StartUp
    {
        public static void Main()
        {
            var personType = typeof(Person);
            var properties = personType.GetProperties
                (BindingFlags.Public | BindingFlags.Instance);

            Console.WriteLine(properties.Length);

            var person1 = new Person { Name = "Pesho", Age = 20};
            var person2 = new Person { Name = "Gosho", Age = 18 };
            var person3 = new Person();
            person3.Name = "Stamat";
            person3.Age = 43;
        }
    }
}
