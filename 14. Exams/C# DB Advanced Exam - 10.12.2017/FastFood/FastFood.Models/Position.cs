namespace FastFood.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Position
    {
        private ICollection<Employee> employees;

        public Position()
        {
            this.employees = new HashSet<Employee>();
        }

        public int Id { get; set; }

        [StringLength(30, MinimumLength = 3)]
        [Required]
        public string Name { get; set; }

        public virtual ICollection<Employee> Employees
        {
            get => this.employees;
            set => this.employees = value;
        }
    }
}