namespace TestProject
{
    using System.Collections.Generic;
    using MiniORM;
    using MiniORM.App.Data.Entities;

    public class TestStartUp
    {
        public static void Main()
        {
            var empList = new List<Employee>();
            var emp = new Employee
            {
                FirstName = "Gosho",
                MiddleName = "P",
                LastName = "Goshov",
                IsEmployed = true,
                DepartmentId = 1
            };
            

            var db = new DbSet<Employee>(empList);
            db.Add(emp);
            db.Remove(emp);
        }
    }
}