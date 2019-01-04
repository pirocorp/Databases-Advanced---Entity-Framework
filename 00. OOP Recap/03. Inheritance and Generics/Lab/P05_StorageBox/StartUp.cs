namespace P05_StorageBox
{
    using System;

    public class StartUp
    {
        public static void Main()
        {
            var box = new Box<string>();

            for (var i = 0; i < 20; i++)
            {
                box.Add($"Item {i}");
            }

            while (box.Count > 0)
            {
                Console.WriteLine(box.Remove());
            }
        }
    }
}
