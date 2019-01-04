namespace P04_RandomList
{
    using System;

    public class StartUp
    {
        public static void Main()
        {
            var randomList = new RandomList();

            for (var i = 0; i < 10; i++)
            {
                randomList.Add($"Item {i}");
            }

            var randomElement = randomList.RandomString();
            Console.WriteLine(randomElement);
            Console.WriteLine(string.Join(", ", randomList));
        }
    }
}
