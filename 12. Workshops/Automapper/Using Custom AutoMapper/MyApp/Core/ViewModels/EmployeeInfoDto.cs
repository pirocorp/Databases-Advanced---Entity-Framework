﻿namespace MyApp.Core.ViewModels
{
    public class EmployeeInfoDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public decimal Salary { get; set; }

        public override string ToString()
        {
            return $"ID: {this.Id} - {this.FirstName} {this.LastName} -  ${this.Salary:F2}";
        }
    }
}
