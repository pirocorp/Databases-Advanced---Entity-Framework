using P01_StudentSystem.Data;

namespace P01_StudentSystem
{
    public class Startup
    {
        public static void Main()
        {
            using (var context = new StudentSystemContext())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }
        }
    }
}