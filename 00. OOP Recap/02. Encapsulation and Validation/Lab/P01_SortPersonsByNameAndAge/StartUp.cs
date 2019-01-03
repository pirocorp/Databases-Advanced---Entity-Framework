namespace P01_SortPersonsByNameAndAge
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class StartUp
    {
        public static void Main()
        {
            //Problem 1
            //var lines = int.Parse(Console.ReadLine());
            //var persons = new List<Person>();
            //for (int i = 0; i < lines; i++)
            //{
            //    var cmdArgs = Console.ReadLine().Split();
            //    var person = new Person(cmdArgs[0], cmdArgs[1], int.Parse(cmdArgs[2]));
            //    persons.Add(person);
            //}
            //
            //persons.OrderBy(p => p.FirstName)
            //    .ThenBy(p => p.Age)
            //    .ToList()
            //    .ForEach(p => Console.WriteLine(p.ToString()));

            //Problem 2
            //var lines = int.Parse(Console.ReadLine());
            //var persons = new List<Person>();
            //
            //for (int i = 0; i < lines; i++)
            //{
            //    var cmdArgs = Console.ReadLine().Split();
            //    var person = new Person(cmdArgs[0],
            //        cmdArgs[1],
            //        int.Parse(cmdArgs[2]),
            //        decimal.Parse(cmdArgs[3]));
            //
            //    persons.Add(person);
            //}
            //
            //var bonus = decimal.Parse(Console.ReadLine());
            //persons.ForEach(p => p.IncreaseSalary(bonus));
            //persons.ForEach(p => Console.WriteLine(p.ToString()));

            //Problem 3
            //var lines = int.Parse(Console.ReadLine());
            //var persons = new List<Person>();
            //
            //for (int i = 0; i < lines; i++)
            //{
            //    var cmdArgs = Console.ReadLine().Split();
            //
            //    try
            //    {
            //        var person = new Person(cmdArgs[0],
            //            cmdArgs[1],
            //            int.Parse(cmdArgs[2]),
            //            decimal.Parse(cmdArgs[3]));
            //
            //        persons.Add(person);
            //    }
            //    catch (ArgumentException ae)
            //    {
            //        Console.WriteLine(ae.Message);
            //    }
            //
            //}
            //
            //var bonus = decimal.Parse(Console.ReadLine());
            //persons.ForEach(p => p.IncreaseSalary(bonus));
            //persons.ForEach(p => Console.WriteLine(p.ToString()));

            //Problem 4
            var lines = int.Parse(Console.ReadLine());
            var team = new Team("Test Team");

            for (var i = 0; i < lines; i++)
            {
                var cmdArgs = Console.ReadLine().Split();

                try
                {
                    var person = new Person(cmdArgs[0],
                        cmdArgs[1],
                        int.Parse(cmdArgs[2]),
                        decimal.Parse(cmdArgs[3]));

                    team.AddPlayer(person);
                }
                catch (ArgumentException ae)
                {
                    Console.WriteLine(ae.Message);
                }

            }

            Console.WriteLine($"First team have {team.FirstTeam.Count} players");
            Console.WriteLine($"Reserve team have {team.ReserveTeam.Count} players");
        }
    }
}