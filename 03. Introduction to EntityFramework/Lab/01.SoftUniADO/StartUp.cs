namespace _01.SoftUniADO
{
    using System;
    using System.Linq;

    public class StartUp
    {
        public static void Main()
        {
            //Window initialization
            Console.WindowHeight = 34;
            Console.BufferHeight = 34;
            Console.WindowWidth = 120;
            Console.BufferWidth = 120;

            //Db init
            using (var db = new SoftUniEntities())
            {
                ListAll(db);
            }
        }

        public static void ShowDetails(Project project)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();

            Console.WriteLine($"ID: {project.ProjectID, 4}| {project.Name}");
            Console.WriteLine($"--------+{new string('-', Console.WindowWidth - 10)}");
            Console.WriteLine(project.Description);
            Console.WriteLine(new string('-', Console.WindowWidth - 1));
            Console.Write($"Start Date: {project.StartDate, 46} |");
            Console.WriteLine($"End Date: {project.EndDate, 48}");
            var delimeter = new string('-', 59);
            Console.WriteLine($"{delimeter}+{delimeter}");
            var employees = project.Employees.ToList();

            foreach (var employee in employees)
            {
                Console.WriteLine($"{employee.FirstName} {employee.LastName}");
            }
        }

        public static void ListAll(SoftUniEntities context)
        {
            const int pageSize = 30;
            var projects = context.Projects.ToList();
            var page = 0;
            var maxPage = (int)Math.Ceiling(projects.Count / (double)pageSize);
            var pointer = 1;

            while (true)
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
                Console.Clear();

                Console.WriteLine($"  ID | Project Name (Page {page + 1} of {maxPage})");
                Console.WriteLine($"-----+{new string('-', Console.WindowWidth - 7)}");

                var cursorIndex = 1;
                foreach (var project in projects.Skip(pageSize * page).Take(pageSize))
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;

                    if (cursorIndex == pointer)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                        Console.ForegroundColor = ConsoleColor.White;
                    }

                    Console.WriteLine($"{project.ProjectID, 5}| {project.Name}");
                    cursorIndex++;
                }

                var keyPressed = Console.ReadKey();

                switch (keyPressed.Key.ToString())
                {
                    case "Enter":
                        var projectIndex = (page * pageSize + pointer) - 1;
                        var currentProject = projects.Skip(projectIndex).First();
                        ShowDetails(currentProject);
                        Console.ReadKey();
                        break;
                    case "UpArrow":
                        if (pointer > 1)
                        {
                            pointer--;
                        }
                        else if (page > 0)
                        {
                            page--;
                            pointer = pageSize;
                        }
                        break;
                    case "DownArrow":
                        var lastPageRows = projects.Count % pageSize;
                        if (page + 1 == maxPage && pointer < lastPageRows)
                        {
                            pointer++;
                        }
                        else if (pointer < pageSize && page + 1 < maxPage)
                        {
                            pointer++;
                        }
                        else if(page + 1 < maxPage)
                        {
                            page++;
                            pointer = 1;
                        }
                        break;
                    case "Escape":
                        return;
                }
            }
        }

        private static void Demo()
        {
            using (var db = new SoftUniEntities())
            {
                //SimpleSelect(db);

                //Projection(db);

                //Update(db);

                //UpdateNavigationProperty(db);

                //SelectFromNavigationProperties(db);
            }
        }

        private static void SelectFromNavigationProperties(SoftUniEntities db)
        {
            var department = db.Departments.First();
            var guys = department.Employees.ToList();

            foreach (var guy in guys)
            {
                Console.WriteLine(guy.FirstName);
            }
        }

        private static void UpdateNavigationProperty(SoftUniEntities db)
        {
            var employee = db.Employees.First();
            Console.WriteLine(employee.Department.DepartmentID);
            employee.Department = db.Departments.First();
            Console.WriteLine(employee.Department.DepartmentID);
            db.SaveChanges();
        }

        private static void Update(SoftUniEntities db)
        {
            var employee = db.Employees.First();
            Console.WriteLine(employee.FirstName);
            employee.FirstName = "Alex";
            db.SaveChanges();
            Console.WriteLine(employee.FirstName);
        }

        private static void Projection(SoftUniEntities db)
        {
            var emplSql = db.Employees
                .Where(e => e.Salary > 50000);

            var employeesSql = db.Employees
                .Where(e => e.Salary > 50000)
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    e.Salary
                });
            var employees = employeesSql.ToList();

            Console.WriteLine(emplSql);
            Console.WriteLine();

            Console.WriteLine(employeesSql);
            Console.WriteLine();

            foreach (var employee in employees)
            {
                Console.WriteLine($"{employee.FirstName} {employee.LastName} {employee.Salary}");
            }
        }

        private static void SimpleSelect(SoftUniEntities db)
        {
            var employee = db.Employees.Find(1);
            Console.WriteLine(employee.FirstName + ' ' + employee.LastName);
        }
    }
}