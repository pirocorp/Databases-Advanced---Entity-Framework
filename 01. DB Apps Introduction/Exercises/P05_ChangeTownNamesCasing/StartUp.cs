namespace P05_ChangeTownNamesCasing
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public class StartUp
    {
        public static void Main()
        {
            var country = Console.ReadLine();

            using (var dbCon = new SqlConnection(Configuration.ConnectionString))
            {
                dbCon.Open();

                var townsIdsCommandString ="SELECT t.Id FROM Towns AS t JOIN Countries AS c ON c.Id = t.CountryCode WHERE c.Name = @country";
                using (var townsIdsCommand = new SqlCommand(townsIdsCommandString, dbCon))
                {
                    townsIdsCommand.Parameters.AddWithValue("@country", country);
                    var townsIds = new List<int>();
                    var dataReader = townsIdsCommand.ExecuteReader();
                    using (dataReader)
                    {
                        if (!dataReader.HasRows)
                        {
                            Console.WriteLine($"No town names were affected.");
                            return;
                        }

                        while (dataReader.Read())
                        {
                            townsIds.Add(int.Parse(dataReader[0].ToString()));
                        }
                    }

                    var upperCaseTownNames = $"UPDATE Towns SET Name = UPPER(Name) WHERE Id IN ({string.Join(", ", townsIds)})";
                    using (var upperCaseTownNamesCommand = new SqlCommand(upperCaseTownNames, dbCon))
                    {
                        upperCaseTownNamesCommand.ExecuteNonQuery();
                    }

                    var townNames = new List<string>();
                    var getTownsNames = $"SELECT Name FROM Towns WHERE Id IN ({string.Join(", ", townsIds)})";
                    using (var getTownsNamesCommand = new SqlCommand(getTownsNames, dbCon))
                    {
                        var townNamesDataReader = getTownsNamesCommand.ExecuteReader();
                        using (townNamesDataReader)
                        {
                            while (townNamesDataReader.Read())
                            {
                                townNames.Add(townNamesDataReader[0].ToString());
                            }
                        }
                    }

                    Console.WriteLine($"{townNames.Count} town names were affected.");
                    Console.WriteLine($"[{string.Join(", ", townNames)}]");
                }

                dbCon.Close();
            }
        }
    }
}