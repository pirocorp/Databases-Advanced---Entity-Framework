namespace _01.SoftUniADO
{
    using System;
    using System.Linq;

    public class StartUp
    {
        public static void Main()
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