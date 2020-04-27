namespace _03._Factory_Method
{
    using System;

    /// <summary>
    /// FactoryMethodDemo startup class for
    /// Factory Method Design Pattern Demo.
    /// </summary>
    public static class FactoryMethodDemo
    {
        public static void Main()
        {
            // Note: constructors call Factory Method
            var documents = new Document[2];

            documents[0] = new Resume();
            documents[1] = new Report();

            // Display document pages
            foreach (Document document in documents)
            {
                Console.WriteLine("\n" + document.GetType().Name + "--");
                foreach (Page page in document.Pages)
                {
                    Console.WriteLine(" " + page.GetType().Name);
                }
            }
        }
    }
}
