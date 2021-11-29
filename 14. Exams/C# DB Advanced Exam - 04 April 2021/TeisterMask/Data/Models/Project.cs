namespace TeisterMask.Data.Models
{
    using System;
    using System.Collections.Generic;

    public class Project
    {
        public Project()
        {
            this.Tasks = new List<Task>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime OpenDate { get; set; }

        public DateTime? DueDate { get; set; }

        public ICollection<Task> Tasks { get; set; }
    }
}
