namespace _01._Abstract_Factory
{
    /// <summary>
    /// AbstractFactoryDemo startup class for
    /// Abstract Factory Design Pattern Demo.
    /// </summary>
    public static class AbstractFactoryDemo
    {
        public static void Main()
        {
            // Create and run the African animal world
            ContinentFactory africa = new AfricaFactory();
            var world = new AnimalWorld(africa);
            world.RunFoodChain();

            // Create and run the American animal world
            ContinentFactory america = new AmericaFactory();
            world = new AnimalWorld(america);
            world.RunFoodChain();
        }
    }
}
