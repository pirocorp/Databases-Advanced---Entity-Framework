namespace P06_RemoveVillain
{
    using System;
    using System.Data.SqlClient;

    public class StartUp
    {
        public static void Main()
        {
            var villainId = int.Parse(Console.ReadLine());
            using (var dbCon = new SqlConnection(Configuration.ConnectionString))
            {
                dbCon.Open();
                var transaction = dbCon.BeginTransaction();
                try
                {
                    var villainName = GetVillainName(villainId, dbCon, transaction);
                    if (villainName == null)
                    {
                        Console.WriteLine($"No such villain was found.");
                        return;
                    }

                    var minionsReleased = ReleaseMinions(villainId, dbCon, transaction);
                    DeleteVillain(villainId, dbCon, transaction);
                    transaction.Commit();
                    Console.WriteLine($"{villainName} was deleted.");
                    Console.WriteLine($"{minionsReleased} minions were released.");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);

                    try
                    {
                        transaction.Rollback();
                    }
                    catch (Exception exRollback)
                    {
                        Console.WriteLine(exRollback.Message);
                    }
                }

                dbCon.Close();
            }
        }

        private static void DeleteVillain(int villainId, SqlConnection dbCon, SqlTransaction transaction)
        {
            var deleteVillain = "DELETE Villains WHERE Id = @villainId";
            using (var deleteVillainCommand = new SqlCommand(deleteVillain, dbCon, transaction))
            {
                deleteVillainCommand.Parameters.AddWithValue("@villainId", villainId);
                deleteVillainCommand.ExecuteNonQuery();
            }
        }

        private static int ReleaseMinions(int villainId, SqlConnection dbCon, SqlTransaction transaction)
        {
            var releaseMinions = "DELETE MinionsVillains WHERE VillainId = @villainId";
            using (var releaseMinionsCommand = new SqlCommand(releaseMinions, dbCon, transaction))
            {
                releaseMinionsCommand.Parameters.AddWithValue("@villainId", villainId);
                var affectedRows = releaseMinionsCommand.ExecuteNonQuery();
                return affectedRows;
            }
        }

        private static string GetVillainName(int villainId, SqlConnection dbCon, SqlTransaction transaction)
        {
            var getName = "SELECT Name FROM Villains WHERE Id = @villainId";
            using (var getNameCommand = new SqlCommand(getName, dbCon, transaction))
            {
                getNameCommand.Parameters.AddWithValue("@villainId", villainId);
                var result = getNameCommand.ExecuteScalar()?.ToString();
                return result;
            }
        }
    }
}