namespace P03_MinionNames
{
    using System;
    using System.Data.SqlClient;

    public class StartUp
    {
        public static void Main()
        {
            using (var dbCon = new SqlConnection(Configuration.ConnectionString))
            {
                dbCon.Open();

                Console.Write("Input villain id: ");
                var villainId = int.Parse(Console.ReadLine());
                var villainName = GetVillainName(villainId, dbCon);

                if (villainName == null)
                {
                    Console.WriteLine($"No villain with ID {villainId} exists in the database.");
                    return;
                }

                Console.WriteLine($"Villain: {villainName}");

                var minionsDataReader = GetMinions(villainId, dbCon);

                using (minionsDataReader)
                {
                    if (!minionsDataReader.HasRows)
                    {
                        Console.WriteLine($"(no minions)");
                        return;
                    }

                    var count = 1;
                    while (minionsDataReader.Read())
                    {
                        Console.WriteLine($"{count++}. {minionsDataReader[0]} {minionsDataReader[1]}");
                    }
                }

                dbCon.Close();
            }
        }

        private static string GetVillainName(int villainId, SqlConnection dbCon)
        {
            var sqlCommandString = "SELECT Name FROM Villains WHERE Id = @villainId";
            var sqlCommand = new SqlCommand(sqlCommandString, dbCon);
            sqlCommand.Parameters.AddWithValue("@villainId", villainId);

            var result = sqlCommand.ExecuteScalar()?.ToString();
            return result;
        }

        private static SqlDataReader GetMinions(int villainId, SqlConnection dbCon)
        {
            var sqlCommandString = "SELECT m.Name, m.Age FROM Minions AS m JOIN MinionsVillains as mv ON mv.MinionId = m.Id " + 
                                   "JOIN Villains AS v ON v.Id = mv.VillainId WHERE v.Id = @villainId ORDER BY Name";
            var sqlCommand = new SqlCommand(sqlCommandString, dbCon);
            sqlCommand.Parameters.AddWithValue("@villainId", villainId);

            var result = sqlCommand.ExecuteReader();
            return result;
        }
    }
}