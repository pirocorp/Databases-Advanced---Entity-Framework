namespace _03.SimpleInsertion
{
    using System;
    using System.Data.SqlClient;

    public class Program
    {
        static void Main(string[] args)
        {
            var connectionString = @"Server=PIROMAN\SQLEXPRESS; Database=SoftUni; Trusted_Connection=True";
            var connection = new SqlConnection(connectionString);
            connection.Open();
            using (connection)
            {
                var selectionCommandString = "SELECT * FROM Employees";
                var command = new SqlCommand(selectionCommandString, connection);
                var reader = command.ExecuteReader();
                using (reader)
                {
                    while (reader.Read())
                    {
                        for (var i = 0; i < reader.FieldCount; i++)
                        {
                            Console.Write($"{reader[i]} ");
                        }
                        Console.WriteLine();
                    }
                }
            }
        }
    }
}
