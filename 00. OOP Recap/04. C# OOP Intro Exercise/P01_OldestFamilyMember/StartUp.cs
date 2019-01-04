namespace P01_OldestFamilyMember
{
    using System;

    public class StartUp
    {
        public static void Main()
        {
            var oldestMemberMethod = typeof(Family).GetMethod("GetOldestMember");
            var addMemberMethod = typeof(Family).GetMethod("AddMember");
            if (oldestMemberMethod == null || addMemberMethod == null)
            {
                throw new Exception();
            }

            var family = new Family();
            var n = int.Parse(Console.ReadLine());

            for (var i = 0; i < n; i++)
            {
                var personArgs = Console.ReadLine().Split(new[] {" "}, StringSplitOptions.RemoveEmptyEntries);
                var personName = personArgs[0];
                var personAge = int.Parse(personArgs[1]);
                var person = new Person(personName, personAge);
                family.AddMember(person);
            }

            Console.WriteLine(family.GetOldestMember());
        }
    }
}
