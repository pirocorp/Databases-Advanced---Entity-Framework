namespace Forum.Console
{
    using System;

    public class PostImporter : IImporter
    {
        public string Message => "Start importing posts";

        public int Order => 2;

        public void Import()
        {
            Console.WriteLine("Importing");
        }
    }
}