namespace _01._Define_Bank_Account_Class
{
    using System;

    public class BankAccount
    {
        private int id;
        private decimal balance;

        private BankAccount(int id)
        {
            this.ID = id;
        }

        public int ID
        {
            get => this.id;
            private set => this.id = value;
        }

        public decimal Balance
        {
            get => this.balance;
            private set => this.balance = value;
        }

        public void Deposit(decimal amount)
        {
            this.Balance += amount;
        }

        public void Withdraw(decimal amount)
        {
            if (amount > this.balance)
            {
                throw new ArgumentException($"Insufficient balance");
            }

            this.Balance -= amount;
        }

        public static BankAccount Create(int id)
        {
            var newBankAccount = new BankAccount(id);
            return newBankAccount;
        }

        public override string ToString()
        {
            var result = $"Account ID {this.ID}, balance = {this.Balance:F2}";
            return result;
        }
    }
}