namespace P03_Mankind
{
    using System;

    public class Student : Human
    {
        private string facultyNumber;

        public Student(string firstName, string lastName, string facultyNumber) 
            : base(firstName, lastName)
        {
            this.FacultyNumber = facultyNumber;
        }

        public string FacultyNumber
        {
            get => this.facultyNumber;
            private set
            {
                if (value.Length < 5 || value.Length > 10)
                {
                    throw new ArgumentException("Invalid faculty number!");
                }

                this.facultyNumber = value;
            }
        }

        public override string ToString()
        {
            var result = $"First Name: {this.FirstName}" + Environment.NewLine +
                         $"Last Name: {this.LastName}" + Environment.NewLine +
                         $"Faculty number: {this.FacultyNumber}";

            return result;
        }
    }
}