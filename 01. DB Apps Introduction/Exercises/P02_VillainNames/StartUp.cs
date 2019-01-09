namespace P02_VillainNames
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

                var sqlCommandString =
                    "SELECT v.Name, COUNT(m.Id) AS [Count] FROM Villains AS v JOIN MinionsVillains AS mv ON mv.VillainId = v.Id JOIN Minions AS m ON m.Id = mv.MinionId GROUP BY v.Id, v.Name HAVING COUNT(m.Id) >= 3 ORDER BY [Count] DESC";

                using (var sqlCommand = new SqlCommand(sqlCommandString, dbCon))
                {
                    var dataReader = sqlCommand.ExecuteReader();

                    using (dataReader)
                    {
                        const int leftAlign = -15;
                        Console.WriteLine($"{"Villain name", leftAlign}| Minions count");
                        Console.WriteLine($"{new string('-', 15)}+{new string('-', 15)}");
                        while (dataReader.Read())
                        {
                            Console.WriteLine($"{dataReader[0], leftAlign}| {dataReader[1]}");
                        }
                    }
                }

                dbCon.Close();
            }
        }
    }
}