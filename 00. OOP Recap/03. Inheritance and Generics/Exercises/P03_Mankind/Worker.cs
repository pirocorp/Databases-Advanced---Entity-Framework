namespace P03_Mankind
{
    using System;

    public class Worker : Human
    {
        private decimal weekSalary;
        private decimal workHoursPerDay;

        public Worker(string firstName, string lastName, decimal weekSalary, decimal workHoursPerDay) 
            : base(firstName, lastName)
        {
            this.WeekSalary = weekSalary;
            this.WorkHoursPerDay = workHoursPerDay;
        }
        
        public decimal WeekSalary
        {
            get => this.weekSalary;
            private set
            {
                if (value <= 10)
                {
                    throw new ArgumentException("Expected value mismatch! Argument: weekSalary");
                }

                this.weekSalary = value;
            }
        }

        public decimal WorkHoursPerDay
        {
            get => this.workHoursPerDay;
            private set
            {
                if (value < 1 || value > 12)
                {
                    throw new ArgumentException("Expected value mismatch! Argument: workHoursPerDay");
                }

                this.workHoursPerDay = value;
            }
        }

        public decimal HourSalary => this.WeekSalary / (this.WorkHoursPerDay * 5);

        public override string ToString()
        {
            var result = $"First Name: {this.FirstName}" + Environment.NewLine +
                         $"Last Name: {this.LastName}" + Environment.NewLine +
                         $"Week Salary: {this.weekSalary:F2}" + Environment.NewLine +
                         $"Hours per day: {this.WorkHoursPerDay:F2}" + Environment.NewLine +
                         $"Salary per hour: {this.HourSalary:F2}";

            return result;
        }
    }
}