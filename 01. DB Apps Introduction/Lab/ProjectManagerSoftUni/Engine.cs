namespace ProjectManagerSoftUni
{
    using System;
    using System.Data.SqlClient;

    public class Engine
    {
        private readonly string connectionString;
        private bool isQuit;
        private SqlConnection dbCon;

        public Engine(string connectionString)
        {
            this.connectionString = connectionString;
            this.isQuit = false;
        }

        public void Run()
        {
            while (!this.isQuit)
            {
                this.InterpretCommand();
            }
        }

        private void InterpretCommand()
        {
            this.PrintMenu();
            var input = this.ReadNumberFromConsole();

            switch (input)
            {
                case 1:
                    this.ListAllProjects();
                    break;
                case 2:
                    this.ViewDetails();
                    break;
                case 3:
                    this.SearchForProject();
                    break;
                case 4:
                    this.AssignEmployee();
                    break;
                case 5:
                    this.ReleaseEmployee();
                    break;
                case 6:
                    this.CreateNewProject();
                    break;
                case 7:
                    this.EditProject();
                    break;
                case 8:
                    this.SearchForEmployee();
                    break;
                case 0:
                    this.Quit();
                    break;
                default:
                    Console.WriteLine($"Invalid Command");
                    break;
            }

            Console.ReadKey();
        }

        private void SearchForEmployee()
        {
            Console.Write($"Enter employee name: ");
            var employeeName = this.ReadStringFromConsole();

            this.CreateNewDatabaseConnection();
            this.dbCon.Open();

            using (this.dbCon)
            {
                var sqlCommandString = "SELECT EmployeeID, FirstName, LastName FROM Employees WHERE FirstName LIKE @employeeName OR LastName LIKE @employeeName";
                var sqlCommand = new SqlCommand(sqlCommandString, this.dbCon);
                sqlCommand.Parameters.AddWithValue("@employeeName", $"%{employeeName}%");
                var dataReader = sqlCommand.ExecuteReader();

                using (dataReader)
                {
                    Console.WriteLine($"Employee ID | Employee Name");
                    Console.WriteLine($"{new string('-', 12)}+{new string('-', 40)}");
                    while (dataReader.Read())
                    {
                        var employeeId = dataReader["EmployeeID"].ToString();
                        var firstName = dataReader["FirstName"].ToString();
                        var lastName = dataReader["LastName"].ToString();
                        Console.WriteLine($"{employeeId,-12}| {firstName} {lastName}");
                    }
                }
            }
        }

        private void EditProject()
        {
            Console.Write($"Enter Project ID: ");
            var projectId = this.ReadNumberFromConsole();

            this.CreateNewDatabaseConnection();
            this.dbCon.Open();

            using (this.dbCon)
            {
                var sqlCommandString = "SELECT COUNT(*) FROM Projects WHERE ProjectID = @projectId";
                var sqlCommand = new SqlCommand(sqlCommandString, this.dbCon);
                sqlCommand.Parameters.AddWithValue("@projectId", projectId);
                var result = int.Parse(sqlCommand.ExecuteScalar().ToString());

                if (result == 0)
                {
                    Console.WriteLine("Project not found.");
                    return;
                }
            }

            Console.WriteLine();
            this.ViewDetails(projectId);
            Console.Write($"Enter Project New Name: ");
            var projectName = this.ReadStringFromConsole();
            Console.Write($"Enter Project New Description: ");
            var description = this.ReadStringFromConsole();
            
            this.CreateNewDatabaseConnection();
            this.dbCon.Open();

            using (this.dbCon)
            {
                var sqlCommandString = "UPDATE Projects SET Name = @projectName, Description = @description WHERE ProjectID = @projectId";
                var transaction = this.dbCon.BeginTransaction();
                var sqlCommand = new SqlCommand(sqlCommandString, this.dbCon, transaction);
                sqlCommand.Parameters.AddWithValue("@projectId", projectId);
                sqlCommand.Parameters.AddWithValue("@projectName", projectName);
                sqlCommand.Parameters.AddWithValue("@description", description);

                try
                {
                    sqlCommand.ExecuteNonQuery();
                    transaction.Commit();
                    Console.WriteLine("Project Has Been Updated.");
                    this.ViewDetails(projectId);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                    try
                    {
                        transaction.Rollback();
                    }
                    catch (InvalidOperationException exRollback)
                    {
                        Console.WriteLine(exRollback.Message);
                    }
                }
            }
        }

        private void CreateNewProject()
        {
            Console.Write($"Enter Project Name: ");
            var projectName = this.ReadStringFromConsole();
            Console.Write($"Enter Project Description: ");
            var description = this.ReadStringFromConsole();

            this.CreateNewDatabaseConnection();
            this.dbCon.Open();

            using (this.dbCon)
            {
                var sqlCommandString = "INSERT INTO Projects " +
                                       "(Name, Description, StartDate, EndDate) VALUES " +
                                       "(@name, @desc, @start, NULL)";

                var transaction = this.dbCon.BeginTransaction();
                var sqlCommand = new SqlCommand(sqlCommandString, this.dbCon, transaction);
                sqlCommand.Parameters.AddWithValue("@name", projectName);
                sqlCommand.Parameters.AddWithValue("@desc", description);
                sqlCommand.Parameters.AddWithValue("@start", DateTime.Now);

                try
                {
                    sqlCommand.ExecuteNonQuery();
                    transaction.Commit();
                    Console.WriteLine("Project has been inserted.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                    try
                    {
                        transaction.Rollback();
                    }
                    catch (InvalidOperationException exRollback)
                    {
                        Console.WriteLine(exRollback.Message);
                    }
                }
            }

            Console.WriteLine();
        }

        private void ReleaseEmployee()
        {
            Console.Write($"Enter Employee Id: ");
            var employeeId = this.ReadNumberFromConsole();
            Console.Write($"Enter Project Id: ");
            var projectId = this.ReadNumberFromConsole();

            this.CreateNewDatabaseConnection();
            this.dbCon.Open();

            using (this.dbCon)
            {
                var sqlCommandString = "SELECT COUNT(*) FROM EmployeesProjects WHERE EmployeeID = @employeeId AND ProjectID = @projectId";
                var sqlCommand = new SqlCommand(sqlCommandString, this.dbCon);
                sqlCommand.Parameters.AddWithValue("@employeeId", employeeId);
                sqlCommand.Parameters.AddWithValue("@projectId", projectId);

                var result = int.Parse(sqlCommand.ExecuteScalar().ToString());
                Console.WriteLine();

                if (result == 0)
                {
                    Console.WriteLine($"No such employee for the given project.");
                    Console.WriteLine();
                    return;
                }

                var transaction = this.dbCon.BeginTransaction();
                sqlCommandString = "DELETE EmployeesProjects WHERE EmployeeID = @employeeId AND ProjectID = @projectId";
                sqlCommand = new SqlCommand(sqlCommandString, this.dbCon, transaction);
                sqlCommand.Parameters.AddWithValue("@employeeId", employeeId);
                sqlCommand.Parameters.AddWithValue("@projectId", projectId);

                try
                {
                    sqlCommand.ExecuteNonQuery();
                    transaction.Commit();
                    Console.WriteLine("Employee has been deleted from this project.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                    try
                    {
                        transaction.Rollback();
                    }
                    catch (InvalidOperationException exRollback)
                    {
                        Console.WriteLine(exRollback.Message);
                    }
                }
            }

            Console.WriteLine();
        }

        private void AssignEmployee()
        {
            Console.Write($"Enter Employee Id: ");
            var employeeId = this.ReadNumberFromConsole();
            Console.Write($"Enter Project Id: ");
            var projectId = this.ReadNumberFromConsole();

            this.CreateNewDatabaseConnection();
            this.dbCon.Open();

            using (this.dbCon)
            {
                var sqlCommandString = "SELECT COUNT(*) FROM EmployeesProjects WHERE EmployeeID = @employeeId AND ProjectID = @projectId";
                var sqlCommand = new SqlCommand(sqlCommandString, this.dbCon);
                sqlCommand.Parameters.AddWithValue("@employeeId", employeeId);
                sqlCommand.Parameters.AddWithValue("@projectId", projectId);

                var result = int.Parse(sqlCommand.ExecuteScalar().ToString());
                Console.WriteLine();

                if (result > 0)
                {
                    Console.WriteLine($"Employee is all ready assigned to this project.");
                    Console.WriteLine();
                    return;
                }

                var transaction = this.dbCon.BeginTransaction();
                sqlCommandString = "INSERT INTO EmployeesProjects VALUES (@employeeId, @projectId)";
                sqlCommand = new SqlCommand(sqlCommandString, this.dbCon, transaction);
                sqlCommand.Parameters.AddWithValue("@employeeId", employeeId);
                sqlCommand.Parameters.AddWithValue("@projectId", projectId);

                try
                {
                    sqlCommand.ExecuteNonQuery();
                    transaction.Commit();
                    Console.WriteLine("Employee has been assigned to this project.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                    try
                    {
                        transaction.Rollback();
                    }
                    catch (InvalidOperationException exRollback)
                    {
                        Console.WriteLine(exRollback.Message);
                    }
                }
            }
        }

        private void SearchForProject()
        {
            Console.Write($"Enter Search Criteria: ");
            var userInput = this.ReadStringFromConsole();
            this.CreateNewDatabaseConnection();
            this.dbCon.Open();

            using (this.dbCon)
            {
                var sqlCommandString = $"SELECT * FROM Projects WHERE Name LIKE @userInput";
                var sqlCommand = new SqlCommand(sqlCommandString, this.dbCon);
                sqlCommand.Parameters.AddWithValue("@userInput", $"%{userInput}%");
                var dataReader = sqlCommand.ExecuteReader();
                Console.WriteLine();
                Console.WriteLine($"{"Project ID",-12}| Name");
                Console.WriteLine(new string('-', 12) + "+" + new string('-', 40));

                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        var projectId = dataReader["ProjectID"];
                        var name = dataReader["Name"];

                        Console.WriteLine($"{projectId,-12}| {name}");
                    }
                }
            }

            Console.WriteLine();
        }

        private void ViewDetails(int projectId = -1)
        {
            if (projectId == -1)
            {
                Console.Write($"Enter ProjectId: ");
                projectId = this.ReadNumberFromConsole();
            }

            this.CreateNewDatabaseConnection();
            this.dbCon.Open();

            using (this.dbCon)
            {
                var sqlCommandString = "SELECT * FROM Projects WHERE ProjectID = @projectId";
                var sqlCommand = new SqlCommand(sqlCommandString, this.dbCon);
                sqlCommand.Parameters.AddWithValue("@projectId", projectId);
                var dataReader = sqlCommand.ExecuteReader();

                Console.WriteLine();
                using (dataReader)
                {
                    while (dataReader.Read())
                    {
                        var currentProjectId = dataReader["ProjectID"].ToString();
                        var currentProjectName = dataReader["Name"].ToString();
                        var currentProjectDescription = dataReader["Description"].ToString();
                        var currentStartDate = dataReader["StartDate"].ToString();
                        var currentEndDate = dataReader["EndDate"].ToString();
                        Console.WriteLine(new string('-', 71));
                        Console.WriteLine($"ID: {currentProjectId}");
                        Console.WriteLine(new string('-', 71));
                        Console.WriteLine($"Name: {currentProjectName}");
                        Console.WriteLine(new string('-', 71));
                        Console.WriteLine($"Description:");
                        Console.WriteLine(currentProjectDescription);
                        Console.WriteLine(new string('-', 71));
                        Console.WriteLine($"{$"Start Date: {currentStartDate}", -35}| End Date: {currentEndDate}");
                        Console.WriteLine(new string('-', 71));
                    }
                }

                sqlCommandString = "SELECT e.EmployeeID, e.FirstName, e.LastName FROM Employees AS e JOIN EmployeesProjects AS ep ON ep.EmployeeID = e.EmployeeID WHERE ep.ProjectID = @projectId";
                sqlCommand = new SqlCommand(sqlCommandString, this.dbCon);
                sqlCommand.Parameters.AddWithValue("@projectId", projectId);
                dataReader = sqlCommand.ExecuteReader();

                using (dataReader)
                {
                    Console.WriteLine($"Employee ID | Employee Name");
                    Console.WriteLine($"{new string('-', 12)}+{new string('-', 40)}");
                    while (dataReader.Read())
                    {
                        var employeeId = dataReader[0].ToString();
                        var employeeFirstname = dataReader[1].ToString();
                        var employeeLastname = dataReader[2].ToString();
                        Console.WriteLine($"{employeeId, -12}| {employeeFirstname} {employeeLastname}");
                    }
                }
            }

            Console.WriteLine();
        }

        private void ListAllProjects()
        {
            this.CreateNewDatabaseConnection();
            this.dbCon.Open();

            using (this.dbCon)
            {
                var sqlCommandString = "SELECT ProjectID, [Name] FROM Projects";
                var sqlCommand = new SqlCommand(sqlCommandString, this.dbCon);
                var dataReader = sqlCommand.ExecuteReader();

                Console.WriteLine();
                Console.WriteLine($"{"Project ID", -12}| Name");
                Console.WriteLine(new string('-', 12)+ "+" + new string('-', 40));
                while (dataReader.Read())
                {
                    var projectId = dataReader["ProjectID"];
                    var name = dataReader["Name"];

                    Console.WriteLine($"{projectId, -12}| {name}");
                }

                Console.WriteLine();
            }
        }

        private void Quit()
        {
            this.isQuit = true;
        }

        private void PrintMenu()
        {
            const int leftAlign = -30;
            Console.WriteLine($"{"Command", leftAlign}| Description");
            Console.WriteLine($"{new string('-', 30)}+{new string('-', 40)}");
            Console.WriteLine($"{"1.All Projects", leftAlign}| List all projects");
            Console.WriteLine($"{"2.View Details", leftAlign}| View Details for specific project");
            Console.WriteLine($"{"3.Search By Project", leftAlign}| Search for project by name");
            Console.WriteLine($"{"4.Assign Employee", leftAlign}| Assign given employee for given project");
            Console.WriteLine($"{"5.Release Employee",leftAlign}| Release given employee from given project");
            Console.WriteLine($"{"6.Create Project", leftAlign}| Create new project");
            Console.WriteLine($"{"7.Edit Project", leftAlign}| Edit existing project details");
            Console.WriteLine($"{"8.Search By Employee", leftAlign}| Search by employee");
            Console.WriteLine($"{"0.Exit", leftAlign}| Closes the application");
            Console.Write($"Enter your command: ");
        }

        private void CreateNewDatabaseConnection()
        {
            this.dbCon = new SqlConnection(this.connectionString);
        }

        private int ReadNumberFromConsole()
        {
            var isParsed = false;
            var employeeId = -1;

            while (!(isParsed = int.TryParse(Console.ReadLine(), out employeeId)))
            {
            }

            return employeeId;
        }

        private string ReadStringFromConsole()
        {
            var userInput = string.Empty;

            while ((userInput = Console.ReadLine()).Length == 0)
            {
            }

            return userInput;
        }
    }
}