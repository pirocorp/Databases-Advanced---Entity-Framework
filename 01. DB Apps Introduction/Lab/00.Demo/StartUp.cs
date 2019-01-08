namespace _00.Demo
{
    using System;
    using System.Data.SqlClient;

    public class StartUp
    {
        public static void Main()
        {
            //SqlClient and ADO.NET Connected Model
            //Retrieving data in connected model
            //Open a connection(SqlConnection)
            //Execute command(SqlCommand)
            //Process the result set of the query byusing a reader (SqlDataReader)
            //Close the reader
            //Close the connection
            var connectionString = @"Server=PIROMAN\SQLEXPRESS;" +
                                   @"Database=SoftUni;" +
                                   @"Integrated Security=true";

            var dbCon = new SqlConnection(connectionString);
            dbCon.Open();
            using (dbCon)
            {
                //ExecuteScalar Example
                var sqlCommandString = "SELECT COUNT(*) FROM Employees";
                var sqlCommand = new SqlCommand(sqlCommandString, dbCon);
                var employeesCount = (int) sqlCommand.ExecuteScalar();
                Console.WriteLine($"Employees count: {employeesCount}");
                Console.WriteLine();

                //ExecuteReader Example
                sqlCommandString = "Select * FROM Employees";
                sqlCommand = new SqlCommand(sqlCommandString, dbCon);
                var reader = sqlCommand.ExecuteReader();
                using (reader)
                {
                    var count = 1;
                    while (reader.Read())
                    {
                        var firstName = reader["FirstName"].ToString();
                        var lastName = reader["LastName"].ToString();
                        var salary = decimal.Parse(reader["Salary"].ToString());
                        Console.WriteLine($"{count++}.{firstName} {lastName} - {salary:F2}");
                    }
                }

                //Using parameterized Commands to avoid SQL Injection
                InsertProject("Test Project", "Test Description for test project", DateTime.Now, dbCon);
            }
        }

        private static void InsertProject(string name, string description, DateTime startDate, SqlConnection dbCon)
        {
            var cmd = new SqlCommand(
                "INSERT INTO Projects " +
                "(Name, Description, StartDate, EndDate) VALUES " +
                "(@name, @desc, @start, NULL)", dbCon);

            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@desc", description);
            cmd.Parameters.AddWithValue("@start", startDate);

            cmd.ExecuteNonQuery();
        }
    }
}
 