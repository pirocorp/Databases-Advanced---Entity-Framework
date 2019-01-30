namespace Demo.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string JobTitle { get; set; }

        public int? Age { get; set; }

        public int? DepartmentId { get; set; }
        public virtual Department Department { get; set; }
    }
}