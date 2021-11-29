namespace TeisterMask.Data.Models
{
    using System;
    using System.Collections.Generic;
    using Enums;

    public class Task
    {
        public Task()
        {
            this.EmployeesTasks = new List<EmployeeTask>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime OpenDate { get; set; }

        public DateTime DueDate { get; set; }

        public ExecutionType ExecutionType { get; set; }

        public LabelType LabelType { get; set; }

        public int ProjectId { get; set; }

        public Project Project { get; set; }

        public ICollection<EmployeeTask> EmployeesTasks { get; set; }
    }
}
