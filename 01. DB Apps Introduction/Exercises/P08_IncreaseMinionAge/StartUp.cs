namespace P08_IncreaseMinionAge
{
    using System;
    using System.Data.SqlClient;
    using System.Linq;

    public class StartUp
    {
        public static void Main()
        {
            var minionIds = Console.ReadLine()
                .Split(new[] {" "}, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            using (var dbCon = new SqlConnection(Configuration.ConnectionString))
            {
                dbCon.Open();

                var updateMinions = $"UPDATE Minions SET Name = UPPER(LEFT(Name, 1)) + LOWER(RIGHT(Name, LEN(Name) - 1)), Age += 1 WHERE Id IN ({string.Join(", ", minionIds)})";
                using (var updateMinionsCommand = new SqlCommand(updateMinions, dbCon))
                {
                    updateMinionsCommand.ExecuteNonQuery();
                }

                PrintMinions(dbCon);
                dbCon.Close();
            }
        }

        private static void PrintMinions(SqlConnection dbCon)
        {
            var print = "SELECT Name, Age FROM Minions";
            using (var printCommand = new SqlCommand(print, dbCon))
            {
                var dataReader = printCommand.ExecuteReader();
                while (dataReader.Read())
                {
                    Console.WriteLine($"{dataReader[0]} {dataReader[1]}");
                }
            }
        }
    }
}