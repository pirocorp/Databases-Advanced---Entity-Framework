namespace _06._Flyweight
{
    public static class FlyweightDemo
    {
        public static void Main()
        {
            // Build a document with text
            var document = "AAZZBBZB";
            var chars = document.ToCharArray();

            var factory = new CharacterFactory();

            // extrinsic state
            var pointSize = 10;

            // For each character use a flyweight object
            foreach (var c in chars)
            {
                pointSize++;
                var character = factory.GetCharacter(c);
                character.Display(pointSize);
            }
        }
    }
}
