using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLInjection
{
    using System.Data;
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
                Selecting("Guy", connection);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("First query passed");
                Console.ForegroundColor = ConsoleColor.White;
                Selecting("' OR 1=1 --", connection);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Second query passed");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        static void Selecting(string name, SqlConnection connection)
        {
            var selectionCommandString = $"SELECT * FROM Employees WHERE FirstName = @name";
            var command = new SqlCommand(selectionCommandString, connection);
            var parameter = new SqlParameter("@name", SqlDbType.VarChar, 50) { Value = name };
            command.Parameters.Add(parameter);
            //OR Another way to add the parameter is 
            //command.Parameters.AddWithValue("@name", nameOfFail);
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
