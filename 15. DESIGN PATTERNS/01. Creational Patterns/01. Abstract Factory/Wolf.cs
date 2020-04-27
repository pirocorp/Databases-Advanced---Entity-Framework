namespace _01._Abstract_Factory
{
    using System;

    /// <summary>
    /// The 'AnotherConcreteProductB' class
    /// </summary>
    public class Wolf : Carnivore
    {
        public override void Eat(Herbivore herbivore)
        {
            // Eat Bison
            Console.WriteLine(this.GetType().Name +
                              " eats " + herbivore.GetType().Name);
        }
    }
}