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

        public Person(string firstName, string lastName, int age)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.age = age;
        }


        public string FirstName => this.firstName;

        public string LastName => this.lastName;

        public int Age => this.age;

        public override string ToString()
        {
            return $"{this.FirstName} {this.LastName} is a {this.Age} years old.";
        }
    }
}