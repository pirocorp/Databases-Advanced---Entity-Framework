namespace FastFood.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Employee
	{
        private ICollection<Order> orders;

        public Employee()
        {
            this.orders = new HashSet<Order>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [Range(15, 80)]
        public int Age { get; set; }

        public int PositionId { get; set; }
        public virtual Position Position { get; set; }

        public virtual ICollection<Order> Orders
        {
            get => this.orders;
            set => this.orders = value;
        }
    }
}