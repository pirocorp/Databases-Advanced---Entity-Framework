namespace P01_SortPersonsByNameAndAge
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Person
    {
        private readonly string firstName;
        private readonly string lastName;
        private readonly int age;
        private decimal salary;

        public Person(string firstName, string lastName, int age, decimal salary)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.age = age;
            this.salary = salary;
        }
        
        public string FirstName => this.firstName;

        public string LastName => this.lastName;

        public int Age => this.age;

        public decimal Salary => this.salary;

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