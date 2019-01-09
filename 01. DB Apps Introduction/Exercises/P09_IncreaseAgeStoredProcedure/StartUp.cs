namespace P09_IncreaseAgeStoredProcedure
{
    using System;
    using System.Data;
    using System.Data.SqlClient;

    public class StartUp
    {
        public static void Main()
        {
            var minionId = int.Parse(Console.ReadLine());
            using (var dbCon = new SqlConnection(Configuration.ConnectionString))
            {
                dbCon.Open();
                var usp = "usp_GetOlder";
                using (var uspCommand = new SqlCommand(usp, dbCon))
                {
                    uspCommand.CommandType = CommandType.StoredProcedure;
                    uspCommand.Parameters.AddWithValue("@minionId", minionId);
                    uspCommand.ExecuteNonQuery();
                }

                var getMinionData = "SELECT Name, Age FROM Minions WHERE Id = @minionId";
                using (var getMinionDataCommand = new SqlCommand(getMinionData, dbCon))
                {
                    getMinionDataCommand.Parameters.AddWithValue("@minionId", minionId);
                    var dataReader = getMinionDataCommand.ExecuteReader();
                    using (dataReader)
                    {
                        while (dataReader.Read())
                        {
                            Console.WriteLine($"{dataReader[0]} – {dataReader[1]} years old");
                        }
                    }
                }

                dbCon.Close();
            }
        }
    }
}