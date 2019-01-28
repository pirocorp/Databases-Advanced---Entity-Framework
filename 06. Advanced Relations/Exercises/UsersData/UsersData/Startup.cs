namespace UsersData
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class Startup
    {
        public static void Main()
        {
            var db = new UsersDataContext();

            var inputString = Console.ReadLine();
            var tagString = Transform(inputString);
            var tag = new Tag(){Text = tagString};
            db.Tags.Add(tag);
            db.SaveChanges();

            var lastTag = db.Tags
                .OrderByDescending(t => t.TagId)
                .FirstOrDefault();

            Console.WriteLine($"{lastTag.Text} was added to database");
        }

        private static string Transform(string inputString)
        {
            var noWhiteSpacesCharacters = new List<char>();

            if (inputString[0] != '#')
            {
                inputString = $"#{inputString}";
            }

            foreach (var ch in inputString)
            {
                if (!char.IsWhiteSpace(ch))
                {
                    noWhiteSpacesCharacters.Add(ch);
                }
            }

            var result = new string(noWhiteSpacesCharacters.Take(20).ToArray());
            return result;
        }
    }
}
