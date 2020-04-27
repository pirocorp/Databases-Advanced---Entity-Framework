namespace _01._Abstract_Factory
{
    /// <summary>
    /// The 'Client' class 
    /// </summary>
    public class AnimalWorld
    {
        private readonly Herbivore _herbivore;
        private readonly Carnivore _carnivore;

        // Constructor

        public AnimalWorld(ContinentFactory factory)
        {
            this._carnivore = factory.CreateCarnivore();
            this._herbivore = factory.CreateHerbivore();
        }

        public void RunFoodChain()
        {
            this._carnivore.Eat(this._herbivore);
        }
    }
}
