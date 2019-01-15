namespace EF_Kenov
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;

    public class StartUp
    {
        public static void Main()
        {
            //ExpressionVsFunc();
            //ExpressionExample();
            //Projection();
            //Joins();
            //NativeSqlExecution();

            using (var db = new SoftUniDbEntities())
            {
                //DifferentJoins(db);
                //SimpleGrouping(db);
                //Grouping(db);
                //Update(db);
                //DetachingAttachingExamples(db);
            }
        }

        private static void DetachingAttachingExamples(SoftUniDbEntities db)
        {
            var emp = db.Employees.FirstOrDefault();
            //this change will enter database
            emp.FirstName = "Vankata";

            var dbEntry = db.Entry(emp);
            dbEntry.State = EntityState.Detached;
            //this change wont enter database
            emp.FirstName = "Peshaka";

            var emply = new Employee()
            {
                EmployeeID = 5,
                JobTitle = "Pes"
            };

            //all changes in this object will be added to database
            //if there is no such primary key new object will be 
            //inserted into database
            //if there is allreay such monitored object 
            //it will be replaced with this one
            //and object will be monitored by the EF
            db.Employees.Attach(emply);

            db.SaveChanges();
        }

        private static void Update(SoftUniDbEntities db)
        {
            //Can update original objects only
            var empl = db.Employees.FirstOrDefault();
            empl.FirstName = "Ivan";
            db.SaveChanges();
        }

        private static void Grouping(SoftUniDbEntities db)
        {
            var emplGroupsSql = db.Employees
                .GroupBy(e => new {e.Department.Name, TownName = e.Address.TownID});

            var emplGroup = emplGroupsSql.ToList();

            Console.WriteLine(emplGroupsSql);
        }

        private static void SimpleGrouping(SoftUniDbEntities db)
        {
            var emplGroupList = db.Employees
                .GroupBy(e => e.Department.Name)
                .ToList();

            foreach (var empl in emplGroupList)
            {
                Console.WriteLine(empl.Key);

                foreach (var em in empl)
                {
                    Console.WriteLine(em.FirstName);
                }
            }
        }

        private static void DifferentJoins(SoftUniDbEntities db)
        {
            var result = db.Towns
                .Join(
                    db.Addresses,
                    t => t.TownID, a => a.TownID,
                    (t, a) => new
                    {
                        TownName = t.Name,
                        AdressTexts = a.AddressText
                    });

            var anotherWay = db.Towns
                .Select(t => new
                {
                    TownName = t.Name,
                    AdressesTexts = t.Addresses.Select(a => a.AddressText).ToList()
                });

            Console.WriteLine(result);
            Console.WriteLine();
            Console.WriteLine(anotherWay);

            var emplJoin = db.Employees
                .Select(e => new
                {
                    FullName = e.FirstName + " " + e.LastName,
                    Town = e.Address.Town.Name,
                    Address = e.Address.AddressText,
                    Projects = e.Projects.Select(pr => pr.Name),
                    Departments = e.Department.Name
                });

            Console.WriteLine();
            Console.WriteLine(emplJoin);
        }

        private static void NativeSqlExecution()
        {
            using (var db = new SoftUniDbEntities())
            {
                var projects = db.Database.SqlQuery<Project>("SELECT * FROM Projects");

                foreach (var project in projects)
                {
                    Console.WriteLine(project.Name);
                }
            }
        }

        private static void Joins()
        {
            using (var db = new SoftUniDbEntities())
            {
                //Wrong Approach For each town we make additional query for addresses
                WrongApproach(db);

                //Better approach
                BetterApproach(db);
            }
        }

        private static void WrongApproach(SoftUniDbEntities db)
        {
            var sql = db
                .Towns
                .Where(t => t.Addresses.Any());

            var towns = sql
                .ToList();

            Console.WriteLine(towns.Count);

            foreach (var town in towns)
            {
                Console.WriteLine(town.Name);
                //here we make another query to take addresses 
                var addresses = town.Addresses;
                foreach (var address in addresses)
                {
                    Console.WriteLine(address.AddressText);
                }

                Console.WriteLine("-----------------------------------");
            }

            Console.WriteLine(sql);
        }

        private static void BetterApproach(SoftUniDbEntities db)
        {
            var sql = db
                .Towns
                .Where(t => t.Addresses.Any())
                .Select(t => new
                {
                    t.Name,
                    Addresses = t.Addresses
                        .Select(a => a.AddressText)
                        .ToList()
                });

            var cities = sql.ToList();

            foreach (var city in cities)
            {
                Console.WriteLine(city.Name);

                foreach (var address in city.Addresses)
                {
                    Console.WriteLine(address);
                }

                Console.WriteLine("-----------------------------------");
            }

            Console.WriteLine(sql);
        }

        private static void Projection()
        {
            using (var db = new SoftUniDbEntities())
            {
                var empl = db
                    .Employees
                    .Where(e => e.Department.Name == "Sales")
                    .Select(e => new
                    {
                        Id = e.EmployeeID,
                        Name = e.FirstName + " " + e.LastName,
                        DepartmentName = e.Department.Name
                    });

                Console.WriteLine(empl);
                Console.WriteLine();

                var result = empl.ToList();
                Console.WriteLine(result[0]);
            }
        }

        private static void ExpressionExample()
        {
            using (var db = new SoftUniDbEntities())
            {
                var project = db.Projects
                    .Where(pr => pr.Name.PadLeft(10) == "")
                    .ToList();

                var result = db.Projects
                    .ToList()
                    .Where(pr => pr.Name.PadLeft(10) == "")
                    .ToList();

                Console.WriteLine(string.Join(", ", result));
            }
        }

        private static void ExpressionVsFunc()
        {
            using (var db = new SoftUniDbEntities())
            {
                //LINQ to Entities must use always Expression
                //If you see LINQ query which expects function its means that you are very wrong 
                //and you will not use optimized query!
                //Expression => Lambda which can be taken apart :)
                var list = new List<Project>();

                var result = list
                    .FirstOrDefault(pr => pr.ProjectID == 1);

                var proj = db.Projects
                    .FirstOrDefault(pr => pr.ProjectID == 1);

                Expression<Func<Project, bool>> expr = pr => pr.ProjectID == 1;
                Func<Project, bool> func = pr => pr.ProjectID == 1;

                func(proj);

                Console.WriteLine(expr.Body);
                Console.WriteLine(expr.Parameters.First());

                var result2 = list
                    .FirstOrDefault(func);

                var proj2 = db.Projects
                    .FirstOrDefault(expr);

                //Query terminator example
                //Here OrderByDescending will be executed at application level
                //while where will be executed at database level
                //ToList executes the query
                var test = db.Projects
                    .Where(x => x.ProjectID > 20)
                    .ToList()
                    .OrderByDescending(x => x.Name);
            }
        }
    }
}