﻿namespace Exercise.App.Core.Dtos
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class EmployeePersonalInfoDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public decimal Salary { get; set; }

        public DateTime? Birthday { get; set; }

        public string Address { get; set; }
    }
}