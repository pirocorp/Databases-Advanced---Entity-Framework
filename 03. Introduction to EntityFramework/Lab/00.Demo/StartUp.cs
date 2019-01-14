namespace _00.Demo
{
    using System;
    using System.Linq;
    using Data;
    using Data.Models;
    using Microsoft.EntityFrameworkCore;

    public class StartUp
    {
        public static void Main()
        {
            using (var context = new SoftUniDbContext())
            {
                //SelectionDemo(context);

                //var employeeToFIre = context.Employees.Find(1);

                //var employee = context.Employees.FirstOrDefault(e => e.FirstName == "Guy" && e.LastName == "Gilbert");

                //var employee = context
                //    .Employees
                //    .Include(e => e.Department)
                //    .FirstOrDefault(e => e.FirstName == "Guy" && e.LastName == "Gilbert");

                //AddRemove(context);

                //Joins(context);

                //Projection(context);

                //var employees = context.Employees
                //    .Where(e => e.Salary > 50000);

                //Console.WriteLine(employees.ToSql());
                //Console.WriteLine();

                //foreach (var employee in employees.ToList())
                //{
                //    Console.WriteLine($"{employee.FirstName} {employee.LastName} {employee.JobTitle}");
                //}
            }
        }

        private static void Projection(SoftUniDbContext context)
        {
            var towns = context
                .Towns
                .Select(t => new
                {
                    t.Name,
                    t.Addresses
                })
                .OrderByDescending(t => t.Addresses.Count)
                .ToList();
        }

        private static void Joins(SoftUniDbContext context)
        {
            var towns = context
                .Towns
                .Include(t => t.Addresses)
                .ThenInclude(a => a.Employees)
                .OrderByDescending(t => t.Addresses.Count)
                .ToList();

            var sql = context
                .Towns
                .Include(t => t.Addresses)
                .ThenInclude(a => a.Employees)
                .OrderByDescending(t => t.Addresses.Count)
                .ToSql();

            Console.WriteLine(sql);
            Console.WriteLine();

            foreach (var town in towns)
            {
                Console.WriteLine($"{town.Name} ({town.Addresses.Count})");

                foreach (var address in town.Addresses.OrderByDescending(a => a.Employees.Count))
                {
                    Console.WriteLine($"  {address.AddressText} ({address.Employees.Count})");

                    foreach (var employee in address.Employees)
                    {
                        Console.WriteLine($"    {employee.FirstName} {employee.LastName}");
                    }
                }
            }
        }

        private static void AddRemove(SoftUniDbContext context)
        {
            var town = new Town()
            {
                Name = "Gabrovo"
            };

            var adress = new Address()
            {
                AddressText = "ul. Shazam 1"
            };

            town.Addresses.Add(adress);

            context.Towns.Add(town);
            context.SaveChanges();

            var gabrovo = context.Towns.SingleOrDefault(t => t.Name == "Gabrovo");
            context.Towns.Remove(gabrovo);
            context.SaveChanges();
        }

        private static void SelectionDemo(SoftUniDbContext context)
        {
            var employees = context.Employees
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    e.JobTitle,
                    e.EmployeesProjects.Count
                })
                .OrderBy(e => e.FirstName)
                .ThenBy(e => e.LastName)
                .GroupBy(e => e.JobTitle)
                .OrderByDescending(x => x.Count())
                .ToList();

            var sql = context.Employees
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    e.JobTitle,
                    e.EmployeesProjects.Count
                })
                .OrderBy(e => e.FirstName)
                .ThenBy(e => e.LastName)
                .GroupBy(e => e.JobTitle)
                .OrderByDescending(x => x.Count())
                .ToSql();

            Console.WriteLine(sql);
            Console.WriteLine();

            foreach (var group in employees)
            {
                Console.WriteLine($"{@group.Key} ({@group.Count()}):");

                foreach (var employee in @group)
                {
                    Console.WriteLine($"{employee.FirstName} {employee.LastName}");
                }
            }
        }
    }
}
