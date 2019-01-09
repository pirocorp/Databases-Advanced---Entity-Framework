namespace P04_AddMinion
{
    using System;
    using System.Data.SqlClient;

    public class StartUp
    {
        public static void Main()
        {
            //Minion: Bob 14 Berlin
            //Villain: Gru
            var minionTokens = Console.ReadLine().Split(new []{' '}, StringSplitOptions.RemoveEmptyEntries);
            var minionName = minionTokens[1];
            var minionAge = int.Parse(minionTokens[2]);
            var minionTown = minionTokens[3];

            var villainTokens = Console.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var villainName = villainTokens[1];

            using (var dbCon = new SqlConnection(Configuration.ConnectionString))
            {
                dbCon.Open();
                var transaction = dbCon.BeginTransaction();

                try
                {
                    var townId = EnsureTownAndReturnTownId(minionTown, dbCon, transaction);
                    var villainId = EnsureVillainAndReturnVillainId(villainName, dbCon, transaction);

                    AddMinionToDatabase(minionName, minionAge, townId, dbCon, transaction);
                    var minionId = GetMinionId(minionName, dbCon, transaction);
                    var result = InsertMinionToMaster(minionId, villainId, dbCon, transaction);
                    transaction.Commit();
                    Console.WriteLine($"{result} added {minionName} to be minion of {villainName}.");
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

        private static string InsertMinionToMaster(int minionId, int villainId, SqlConnection dbCon, SqlTransaction transaction)
        {
            var minionToMasterCount = "SELECT COUNT(*) FROM MinionsVillains WHERE MinionId = @minionId AND VillainId = @villainId;";
            using (var minionToMasterCountCommand = new SqlCommand(minionToMasterCount, dbCon, transaction))
            {
                minionToMasterCountCommand.Parameters.AddWithValue("@minionId", minionId);
                minionToMasterCountCommand.Parameters.AddWithValue("@villainId", villainId);
                var count = int.Parse(minionToMasterCountCommand.ExecuteScalar().ToString());

                if (count > 0)
                {
                    return "Already";
                }
            }

            var insertMinion = "INSERT INTO MinionsVillains VALUES (@minionId, @villainId)";
            using (var insertMinionCommand = new SqlCommand(insertMinion, dbCon, transaction))
            {
                insertMinionCommand.Parameters.AddWithValue("@minionId", minionId);
                insertMinionCommand.Parameters.AddWithValue("@villainId", villainId);
                insertMinionCommand.ExecuteNonQuery();
                return "Successfully";
            }
        }

        private static int GetMinionId(string minionName, SqlConnection dbCon, SqlTransaction transaction)
        {
            var getMinionId = "SELECT TOP 1 Id FROM Minions WHERE Name = @minionName ORDER BY Id DESC";
            using (var getMinionIdCommand = new SqlCommand(getMinionId, dbCon, transaction))
            {
                getMinionIdCommand.Parameters.AddWithValue("@minionName", minionName);
                var result = int.Parse(getMinionIdCommand.ExecuteScalar().ToString());
                return result;
            }
        }

        private static void AddMinionToDatabase(string minionName, int minionAge, int townId, SqlConnection dbCon, SqlTransaction transaction)
        {
            var addMinion = "INSERT INTO Minions VALUES (@minionName, @minionAge, @townId)";
            using (var addMinionCommand = new SqlCommand(addMinion, dbCon, transaction))
            {
                addMinionCommand.Parameters.AddWithValue("@minionName", minionName);
                addMinionCommand.Parameters.AddWithValue("@minionAge", minionAge);
                addMinionCommand.Parameters.AddWithValue("@townId", townId);
                addMinionCommand.ExecuteNonQuery();
            }
        }

        private static int EnsureVillainAndReturnVillainId(string villainName, SqlConnection dbCon, SqlTransaction transaction)
        {
            var villainCount = "SELECT COUNT(*) FROM Villains WHERE Name = @villainName";
            using (var villainCountCommand = new SqlCommand(villainCount, dbCon, transaction))
            {
                villainCountCommand.Parameters.AddWithValue("@villainName", villainName);
                var result = int.Parse(villainCountCommand.ExecuteScalar().ToString());
                if (result == 0)
                {
                    var evilnessId = GetEvilnessId("Evil", dbCon, transaction);
                    var insertVillain = "INSERT INTO Villains VALUES (@villainName, @evilnessId)";
                    using (var insertVillainCommand = new SqlCommand(insertVillain, dbCon, transaction))
                    {
                        insertVillainCommand.Parameters.AddWithValue("@villainName", villainName);
                        insertVillainCommand.Parameters.AddWithValue("@evilnessId", evilnessId);
                        insertVillainCommand.ExecuteNonQuery();
                        Console.WriteLine($"Villain {villainName} was added to the database.");
                    }
                }
            }

            var getVillain = "SELECT TOP 1 Id FROM Villains WHERE Name = @villainName";
            using (var getVillainCommand = new SqlCommand(getVillain, dbCon, transaction))
            {
                getVillainCommand.Parameters.AddWithValue("@villainName", villainName);
                var result = int.Parse(getVillainCommand.ExecuteScalar().ToString());
                return result;
            }
        }

        private static int EnsureTownAndReturnTownId(string minionTown, SqlConnection dbCon, SqlTransaction transaction)
        {
            var sqlTownCount = "SELECT COUNT(*) FROM Towns WHERE Name = @minionTown";
            using (var sqlTownCountCommand = new SqlCommand(sqlTownCount, dbCon, transaction))
            {
                sqlTownCountCommand.Parameters.AddWithValue("@minionTown", minionTown);
                var result = int.Parse(sqlTownCountCommand.ExecuteScalar().ToString());
                if (result == 0)
                {
                    var sqlInsertTown = "INSERT INTO Towns VALUES(@minionTown, 6)";
                    using (var sqlInsertTownCommand = new SqlCommand(sqlInsertTown, dbCon, transaction))
                    {
                        sqlInsertTownCommand.Parameters.AddWithValue("@minionTown", minionTown);
                        sqlInsertTownCommand.ExecuteNonQuery();
                        Console.WriteLine($"Town {minionTown} was added to the database.");
                    }
                }
            }

            var getTownId = "SELECT Id FROM Towns WHERE Name = @minionTown";
            using (var getTownIdCommand = new SqlCommand(getTownId, dbCon, transaction))
            {
                getTownIdCommand.Parameters.AddWithValue("@minionTown", minionTown);
                var result = int.Parse(getTownIdCommand.ExecuteScalar().ToString());
                return result;
            }
        }

        private static int GetEvilnessId(string evilnessFactor, SqlConnection dbCon, SqlTransaction transaction)
        {
            var getEvilnessId = "SELECT Id FROM EvilnessFactors WHERE Name = @evilnessFactor";
            using (var getEvilnessIdCommand = new SqlCommand(getEvilnessId, dbCon, transaction))
            {
                getEvilnessIdCommand.Parameters.AddWithValue("@evilnessFactor", evilnessFactor);
                var result = int.Parse(getEvilnessIdCommand.ExecuteScalar().ToString());
                return result;
            }
        }
    }
}