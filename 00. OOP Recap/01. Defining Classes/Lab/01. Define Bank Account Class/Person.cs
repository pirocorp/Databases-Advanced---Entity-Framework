namespace _01._Define_Bank_Account_Class
{
    using System.Collections.Generic;
    using System.Linq;

    public class Person
    {
        private string name;
        private int age;
        private readonly List<BankAccount> accounts;

        public Person(string name, int age)
        {
            this.Name = name;
            this.Age = age;
            this.accounts = new List<BankAccount>();
        }

        public Person(string name, int age, IEnumerable<BankAccount> accounts)
            : this(name, age)
        {
            this.accounts.AddRange(accounts);
        }
        
        public string Name
        {
            get => this.name;
            private set => this.name = value;
        }

        public int Age
        {
            get => this.age;
            private set => this.age = value;
        }

        public IEnumerable<BankAccount> Accounts => this.accounts.AsReadOnly();

        public decimal GetBalance()
        {
            var totalBalance = this.accounts.Sum(x => x.Balance);
            return totalBalance;
        }
    }
}