namespace P02_DateModifier
{
    using System;

    public class StartUp
    {
        public static void Main()
        {
            var firstDate = Console.ReadLine();
            var secondDate = Console.ReadLine();

            var dateModifier = new DateModifier();
            Console.WriteLine(dateModifier.DayDifference(firstDate, secondDate));
        }
    }
}
