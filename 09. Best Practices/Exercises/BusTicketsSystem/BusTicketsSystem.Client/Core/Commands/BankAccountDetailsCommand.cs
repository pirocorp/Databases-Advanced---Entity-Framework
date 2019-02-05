namespace BusTicketsSystem.Client.Core.Commands
{
    using System;
    using System.Text;
    using AutoMapper;
    using Dtos;
    using Dtos.BankAccountDtos;
    using Interfaces;
    using Services.Interfaces;

    public class BankAccountDetailsCommand : ICommand
    {
        private readonly IBankAccountService bankAccountService;

        public BankAccountDetailsCommand(IBankAccountService bankAccountService)
        {
            this.bankAccountService = bankAccountService;
        }

        //BankAccountDetails <bankId>
        public string Execute(string[] args)
        {
            var bankAccId = int.Parse(args[0]);

            var accountExists = this.bankAccountService.Exists(bankAccId);

            if (!accountExists)
            {
                throw new ArgumentException("Account does not exists!");
            }

            var details = this.bankAccountService.ById<BankAccountDetailsDto>(bankAccId);
            var result = $"{details.CustomerId}: {details.CustomerFirstName} {details.CustomerLastName}" + Environment.NewLine +
                         $"Account Id: {details.Id}, Account Number: {details.AccountNumber}" + Environment.NewLine +
                         $"Balance: {details.Balance}";

            return result;
        }
    }
}