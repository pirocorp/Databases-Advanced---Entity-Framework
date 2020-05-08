namespace TeisterMask.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Employee
    {
        public Employee()
        {
            this.EmployeesTasks = new HashSet<EmployeeTask>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [RegularExpression("[A-Za-z0-9]{3,40}")]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [RegularExpression("\\d{3}-\\d{3}-\\d{4}")]
        public string Phone { get; set; }

        public ICollection<EmployeeTask> EmployeesTasks { get; set; }
    }
}
