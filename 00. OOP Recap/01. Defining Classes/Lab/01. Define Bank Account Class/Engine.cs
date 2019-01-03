namespace _01._Define_Bank_Account_Class
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;

    public class Engine
    {
        private Dictionary<int, BankAccount> accounts;

        public Engine()
        {
            this.accounts = new Dictionary<int, BankAccount>();
        }

        public void Run()
        {
            var input = string.Empty;

            while ((input = Console.ReadLine()) != "End")
            {
                var inputArgs = input.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
                var command = inputArgs[0];
                var commandArgs = inputArgs.Skip(1).ToArray();

                var accId = int.Parse(commandArgs[0]);
                var accExists = this.CheckAccountExists(accId);

                switch (command)
                {
                    case "Create":
                        this.Create(accId);
                        break;
                    case "Deposit":
                        if (accExists)
                        {
                            this.Deposit(accId, commandArgs);
                        }
                        else
                        {
                            Console.WriteLine($"Account does not exist");
                        }
                        break;
                    case "Withdraw":
                        if (accExists)
                        {
                            this.Withdraw(accId, commandArgs);
                        }
                        else
                        {
                            Console.WriteLine($"Account does not exist");
                        }
                        break;
                    case "Print":
                        if (accExists)
                        {
                            this.Print(accId);
                        }
                        else
                        {
                            Console.WriteLine($"Account does not exist");
                        }
                        break;
                }
            }
        }

        private void Print(int accId)
        {
            Console.WriteLine(this.accounts[accId].ToString());
        }

        private void Withdraw(int accId, string[] commandArgs)
        {
            var amount = decimal.Parse(commandArgs[1]);
            try
            {
                this.accounts[accId].Withdraw(amount);
            }
            catch (ArgumentException ae)
            {
                Console.WriteLine(ae.Message);
            }
        }

        private void Deposit(int accId, string[] commandArgs)
        {
            var amount = decimal.Parse(commandArgs[1]);
            this.accounts[accId].Deposit(amount);
        }

        private void Create(int accId)
        {
            if (this.accounts.ContainsKey(accId))
            {
                Console.WriteLine($"Account already exists");
                return;
            }

            var newAcc = BankAccount.Create(accId);
            this.accounts.Add(accId, newAcc);
        }

        private bool CheckAccountExists(int accId)
        {
            if (!this.accounts.ContainsKey(accId))
            {
                return false;
            }

            return true;
        }
    }
}