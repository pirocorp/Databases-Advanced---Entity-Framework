namespace Forum.Console
{
    using System;

    public class CategoryImporter : IImporter
    {
        public string Message => "Start importing categories";

        public int Order => 1;

        public void Import()
        {
            Console.WriteLine("Importing");
        }
    }
}