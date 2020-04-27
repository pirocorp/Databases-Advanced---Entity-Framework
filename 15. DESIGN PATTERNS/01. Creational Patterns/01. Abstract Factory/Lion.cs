namespace _01._Abstract_Factory
{
    using System;

    /// <summary>
    /// The 'ConcreteProductB' class
    /// </summary>
    public class Lion : Carnivore
    {
        public override void Eat(Herbivore herbivore)
        {
            // Eat Wildebeest
            Console.WriteLine(this.GetType().Name +
                              " eats " + herbivore.GetType().Name);
        }
    }
}