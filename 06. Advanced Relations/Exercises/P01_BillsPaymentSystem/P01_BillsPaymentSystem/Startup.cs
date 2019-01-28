namespace P01_BillsPaymentSystem
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Microsoft.EntityFrameworkCore;

    using Data;
    using Data.Models;

    public class Startup
    {
        public static void Main()
        {
            var db = new BillsPaymentSystemContext();
            //UserDetails(db);
            var amount = 0M;
            PayBills(amount);
        }

        private static void PayBills(decimal amount)
        {
            var db = new BillsPaymentSystemContext();
            var user = GetUser(db);

            var totalAmount = TotalAmount(user, out var allBankAccounts, out var allCreditCards);

            Console.WriteLine($"Total amount before transaction: {totalAmount}");

            if (totalAmount < amount)
            {
                Console.WriteLine($"Insufficient funds!");
                return;
            }

            foreach (var bankAccount in allBankAccounts)
            {
                if (bankAccount.Balance >= amount)
                {
                    bankAccount.Withdraw(amount);
                    amount = 0;
                }
                else
                {
                    amount -= bankAccount.Balance;
                    bankAccount.Withdraw(bankAccount.Balance);
                }

                if (amount == 0)
                {
                    db.SaveChanges();
                    return;
                }
            }

            foreach (var creditCard in allCreditCards)
            {
                if (creditCard.LimitLeft >= amount)
                {
                    creditCard.Withdraw(amount);
                    amount = 0;
                }
                else
                {
                    amount -= creditCard.LimitLeft;
                    creditCard.Withdraw(creditCard.LimitLeft);
                }

                if (amount == 0)
                {
                    db.SaveChanges();
                    return;
                }
            }

            db.SaveChanges();
        }

        private static decimal TotalAmount(User user, out List<BankAccount> allBankAccounts, out List<CreditCard> allCreditCards)
        {
            allBankAccounts = user.PaymentMethods
                .Where(pm => pm.BankAccount != null)
                .Select(pm => pm.BankAccount)
                .OrderBy(b => b.BankAccountId)
                .ToList();

            var totalBankAccountBalance = allBankAccounts
                .Sum(b => b.Balance);

            allCreditCards = user.PaymentMethods
                .Where(pm => pm.CreditCard != null)
                .Select(pm => pm.CreditCard)
                .OrderBy(c => c.CreditCardId)
                .ToList();

            var totalLimitLeftCreditCards = allCreditCards
                .Sum(c => c.LimitLeft);

            var totalAmount = totalLimitLeftCreditCards + totalBankAccountBalance;
            return totalAmount;
        }

        private static void UserDetails(BillsPaymentSystemContext db)
        {
            var user = GetUser(db);
            PrintUser(user);
        }

        private static void PrintUser(User user)
        {
            var result = new StringBuilder();

            result.AppendLine($"User: {user.FirstName} {user.LastName}");
            result.AppendLine($"Bank Accounts:");

            var bankAccounts = user.PaymentMethods
                .Where(pm => pm.BankAccount != null)
                .Select(pm => pm.BankAccount)
                .ToList();

            if (bankAccounts.Count == 0)
            {
                result.AppendLine("-- No Bank Accounts");
            }

            foreach (var bankAccount in bankAccounts)
            {
                result.AppendLine($"-- ID: {bankAccount.BankAccountId}");
                result.AppendLine($"--- Balance: {bankAccount.Balance:F2}");
                result.AppendLine($"--- Bank: {bankAccount.BankName}");
                result.AppendLine($"--- SWIFT: {bankAccount.SwiftCode}");
            }

            var creditCards = user.PaymentMethods
                .Where(pm => pm.CreditCard != null)
                .Select(pm => pm.CreditCard)
                .ToList();

            result.AppendLine($"Credit Cards:");


            if (creditCards.Count == 0)
            {
                result.AppendLine($"-- No Credit Cards");
            }

            foreach (var creditCard in creditCards)
            {
                result.AppendLine($"-- ID: {creditCard.CreditCardId}");
                result.AppendLine($"--- Limit: {creditCard.Limit:F2}");
                result.AppendLine($"--- Money Owed: {creditCard.MoneyOwed:F2}");
                result.AppendLine($"--- Limit Left:: {creditCard.LimitLeft:F2}");
                result.AppendLine($"--- Expiration Date: {creditCard.ExpirationDate.Year}/{creditCard.ExpirationDate.Month}");
            }

            Console.WriteLine(result.ToString().Trim());
        }

        private static User GetUser(BillsPaymentSystemContext db)
        {
            User user = null;

            while (user == null)
            {
                var userId = int.Parse(Console.ReadLine());

                user = db.Users
                    .Where(u => u.UserId == userId)
                    .Include(u => u.PaymentMethods)
                    .ThenInclude(pm => pm.CreditCard)
                    .Include(u => u.PaymentMethods)
                    .ThenInclude(pm => pm.BankAccount)
                    .FirstOrDefault();
            }

            return user;
        }
    }
}