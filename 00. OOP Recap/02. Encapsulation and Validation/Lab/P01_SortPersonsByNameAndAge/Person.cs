namespace P01_SortPersonsByNameAndAge
{
    using System;

    public class Person
    {
        private string firstName;
        private string lastName;
        private int age;
        private decimal salary;

        public Person(string firstName, string lastName, int age, decimal salary)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Age = age;
            this.Salary = salary;
        }
        
        public string FirstName 
        {
            get => this.firstName;
            private set
            {
                if (value.Length < 3)
                {
                    throw new ArgumentException($"First name cannot be less than 3 symbols");
                }

                this.firstName = value;
            }
        }

        public string LastName 
        {
            get => this.lastName;
            set
            {
                if (value.Length < 3)
                {
                    throw new ArgumentException($"Last name cannot be less than 3 symbols");
                }

                this.lastName = value;
            }
        }

        public int Age 
        {
            get => this.age;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException($"Age cannot be zero or negative integer");
                }

                this.age = value;
            }
        }

        public decimal Salary 
        {
            get => this.salary;
            set
            {
                if (value < 460)
                {
                    throw new ArgumentException($"Salary cannot be less than 460 leva");
                }

                this.salary = value;
            }
        }

        public void IncreaseSalary(decimal percent)
        {
            if (this.Age > 30)
            {
                this.salary += this.salary * percent / 100;
            }
            else
            {
                this.salary += this.salary * percent / 200;
            }
        }

        public override string ToString()
        {
            return $"{this.FirstName} {this.LastName} get {this.Salary:F2} leva.";
        }
    }
}