namespace UsersData
{
    using System;
    using System.Linq;
    using Data;
    using Microsoft.EntityFrameworkCore;

    public class Startup
    {
        public static void Main()
        {
            var db = new UsersDataContext();

            var users = db.Users
                .Include(u => u.Friends)
                .ToList();

            foreach (var user in users)
            {
                Console.WriteLine($"User: {user.FirstName}");
                var friends = user.Friends
                    .Select(uf => uf.Friend.FirstName)
                    .ToList();
                Console.WriteLine($"Friends: {string.Join(", ", friends)}");
            }
        }
    }
}
