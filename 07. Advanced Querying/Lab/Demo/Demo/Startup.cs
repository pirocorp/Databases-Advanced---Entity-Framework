namespace Demo
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using Z.EntityFramework.Plus;

    public class Startup
    {
        public static void Main()
        {
            //ExecutingNativeSqlQueries();
            //NativeSqlQueriesWithParameters();
            //DetachingObjects();
            //AttachingObjects();
            //ExecutingStoredProcedure();
            //BulkDelete();
            //BulkUpdate();
            //BulkUpdateAge();
            //BulkUpdateDepartmentId();
            //ExplicitLoading();
            //EagerLoading();
            //LazyLoading();

        }

        private static void LazyLoading()
        {
            //Lazy Loading delays loading of data until it is used
            //EF Core enables lazy - loading for any navigation property that can be overridden
            //Offers better performance in certain cases
            //Less RAM usage
            //Smaller result sets returned
            using (var context = new DemoContext())
            {
                var empl = context.Employees.Find(1);

                Console.WriteLine(empl.Department.Name);

                var department = context.Departments.Find(4);
                Console.WriteLine(department.Employees.Count);
            }
        }

        private static void EagerLoading()
        {
            //Eager loading loads all related records of an entity at once
            //Performed with the Include method
            //Install-Package Microsoft.EntityFrameworkCore.Proxies

            using (var context = new DemoContext())
            {
                var empl = context.Employees
                    .Include(e => e.Department)
                    .Take(10);

                foreach (var employee in empl)
                {
                    Console.WriteLine(employee.Department.Name);
                }

                var development = context.Departments
                    .Include(d => d.Employees)
                    .FirstOrDefault(d => d.DepartmentId == 4);

                Console.WriteLine(development?.Employees.Count);
            }
        }

        private static void ExplicitLoading()
        {   //Explicit loading loads all records when they’re needed

            using (var context = new DemoContext())
            {
                var employee = context.Employees.First();

                context.Entry(employee)
                    .Reference(e => e.Department)
                    .Load();

                Console.WriteLine(employee.Department.Name);

                var department = context.Departments.Find(4);

                // Performed with the Collection().Load() method
                context.Entry(department)
                    .Collection(d => d.Employees)
                    .Load();

                Console.WriteLine($"{department.Name}: {department.Employees.Count}");
            }
        }

        private static void BulkUpdateDepartmentId()
        {
            using (var context = new DemoContext())
            {
                context.Employees
                    .Update(u => new Employee() {DepartmentId = 1});
            }
        }

        private static void BulkUpdateAge()
        {
            using (var context = new DemoContext())
            {
                context.Employees
                    .Where(e => e.Age == null)
                    .Update(u => new Employee() {Age = 0});
            }
        }

        private static void BulkUpdate()
        {
            var query = "SELECT * FROM Employees WHERE FirstName = {0}";

            var parameter = "Plamen";
            var replaced = "Viktor";

            using (var context = new DemoContext())
            {
                var employeesCount = context.Employees
                    .FromSql(query, parameter)
                    .ToArray()
                    .Length;

                Console.WriteLine($"{parameter} Count: {employeesCount}");

                context.Employees
                    .Where(e => e.FirstName == parameter)
                    .Update(u => new Employee() { FirstName = replaced });


                employeesCount = context.Employees
                    .FromSql(query, parameter)
                    .ToArray()
                    .Length;

                var plamenCount = context.Employees
                    .FromSql(query, replaced)
                    .ToArray()
                    .Length;

                Console.WriteLine($"{parameter} Count: {employeesCount}, {replaced} Count: {plamenCount}");
            }
        }

        private static void BulkDelete()
        {
            var query = "SELECT * FROM Employees WHERE FirstName = 'A'";

            using (var context = new DemoContext())
            {
                var employeesCount = context.Employees
                    .FromSql(query)
                    .ToArray()
                    .Length;

                Console.WriteLine(employeesCount);

                context.Employees
                    .Where(u => u.FirstName == "A")
                    .Delete();

                employeesCount = context.Employees
                    .FromSql(query)
                    .ToArray()
                    .Length;

                Console.WriteLine(employeesCount);
            }
        }

        private static void ExecutingStoredProcedure()
        {
            var ageParameter = new SqlParameter("@age", 5);
            var query = "EXEC UpdateAge @age";

            PrintEmployeeById(1);

            using (var demoContext = new DemoContext())
            {
                demoContext.Database.ExecuteSqlCommand(query, ageParameter);
            }

            PrintEmployeeById(1);
        }

        private static void AttachingObjects()
        {
            //Here employee is detached because it's out of the context scope
            var employee = GetEmployeeById(3);
            UpdateName(employee, "Pesho");
            PrintEmployeeById(3);
        }

        private static void PrintEmployeeById(int id)
        {
            Employee employee;
            employee = GetEmployeeById(id);
            Console.WriteLine($"{employee.FirstName} {employee.LastName} : {employee.Age} - {employee.JobTitle}");
        }

        private static void UpdateName(Employee employee, string newName)
        {
            using (var demoContext = new DemoContext())
            {
                var entry = demoContext.Entry(employee);
                entry.State = EntityState.Modified; //<-- Attaching object to context
                employee.FirstName = newName;
                demoContext.SaveChanges();
            }
        }


        private static void DetachingObjects()
        {
            //Here employee is detached because it's out of the context scope
            var employee = GetEmployeeById(100);
        }

        private static Employee GetEmployeeById(int id)
        {
            using (var demoContext = new DemoContext())
            {
                return demoContext.Employees
                    .First(p => p.EmployeeId == id);
            }

            //After object is returned its been detached because using block
            //Dispose demoContext and when context is disposed all objects that 
            //are tracked are detached.
        }


        private static void NativeSqlQueriesWithParameters()
        {
            var context = new DemoContext();

            var nativeSQLQuery =
                "SELECT *" +
                " FROM dbo.Employees WHERE FirstName = {0}";
            var employees = context.Employees.FromSql(nativeSQLQuery, "A");

            foreach (var employee in employees)
            {
                Console.WriteLine($"{employee.FirstName} {employee.LastName} - {employee.JobTitle}");
            }

        }

        private static void ExecutingNativeSqlQueries()
        {
            //Limitations:
            //JOIN statements don’t get mapped to the entity class
            //Required columns must always be selected
            //Target table must be the same as the DbSet

            using (var db = new DemoContext())
            {
                var query = "SELECT * FROM Employees";

                var employees = db.Employees
                    .FromSql(query)
                    .Take(1000)
                    .ToArray();

            }
        }

        private static void SeedEmployees()
        {
            var random = new Random();
            var employees = new List<Employee>();

            for (var i = 0; i < 10000; i++)
            {
                var currentEmployee = new Employee()
                {
                    FirstName = GenerateRandomString(random),
                    LastName = GenerateRandomString(random),
                    JobTitle = GenerateRandomString(random)
                };

                employees.Add(currentEmployee);
            }

            using (var db = new DemoContext())
            {
                db.Employees.AddRange(employees);
                db.SaveChanges();
            }
        }

        private static string GenerateRandomString(Random random)
        {
            var nameLength = random.Next(1, 30);
            var nameAsChars = new char[nameLength];
            var firstLetter = (char)('A' + random.Next(0, 26));
            nameAsChars[0] = firstLetter;

            for (var i = 1; i < nameLength; i++)
            {
                nameAsChars[i] = (char)('a' + random.Next(0, 26));
            }

            return new string(nameAsChars);
        }
    }
}