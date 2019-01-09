namespace P07_PrintAllMinionNames
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public class StartUp
    {
        public static void Main()
        {
            var minionNames = new List<string>();

            using (var dbCon = new SqlConnection(Configuration.ConnectionString))
            {
                dbCon.Open();
                minionNames = GetMinionNames(dbCon);
                dbCon.Close();
            }

            PrintMinions(minionNames);
        }

        private static void PrintMinions(List<string> minionNames)
        {
            for (var i = 0; i < minionNames.Count / 2; i++)
            {
                var lastElementIndex = minionNames.Count - 1 - i;
                Console.WriteLine(minionNames[i]);
                Console.WriteLine(minionNames[lastElementIndex]);
            }

            var oddElements = minionNames.Count % 2;

            if (oddElements == 1)
            {
                var middleElementIndex = minionNames.Count / 2;
                Console.WriteLine(minionNames[middleElementIndex]);
            }
        }

        private static List<string> GetMinionNames(SqlConnection dbCon)
        {
            var getMinionsNames = "SELECT Name FROM Minions";
            var result = new List<string>();

            using (var getMinionsNamesCommand = new SqlCommand(getMinionsNames, dbCon))
            {
                var dataReader = getMinionsNamesCommand.ExecuteReader();
                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        result.Add(dataReader[0].ToString());
                    }
                }
            }

            return result;
        }
    }
}