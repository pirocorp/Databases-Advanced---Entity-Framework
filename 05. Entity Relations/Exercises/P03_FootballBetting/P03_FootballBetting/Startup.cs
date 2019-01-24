using P03_FootballBetting.Data;

namespace P03_FootballBetting
{
    using Data.Models;

    public class Startup
    {
        public static void Main()
        {
            using (var db = new FootballBettingContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
            }
        }
    }
}
