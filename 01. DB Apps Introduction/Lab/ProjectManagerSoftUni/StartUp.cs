namespace ProjectManagerSoftUni
{
    using System.Data.SqlClient;

    public class StartUp
    {
        public static void Main()
        {
            var connectionString = @"Server=PIROMAN\SQLEXPRESS;" +
                                   @"Database=SoftUni;" +
                                   @"Integrated Security=true";

            var engine = new Engine(connectionString);
            engine.Run();
        }
    }
}
