namespace P07_Animals.Animals
{
    public class Tomcats : Cat
    {
        public Tomcats(string name, int age) 
            : base(name, age, "Male")
        {
        }

        public override string ProduceSound()
        {
            return $"MEOW";
        }
    }
}