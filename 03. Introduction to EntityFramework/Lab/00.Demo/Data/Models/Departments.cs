using System.Collections.Generic;

namespace _00.Demo.Data.Models
{
    public class Department
    {
        public Department()
        {
        }

        public int DepartmentId { get; set; }

        public string Name { get; set; }

        public int ManagerId { get; set; }
        public Employee Manager { get; set; }

        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}
