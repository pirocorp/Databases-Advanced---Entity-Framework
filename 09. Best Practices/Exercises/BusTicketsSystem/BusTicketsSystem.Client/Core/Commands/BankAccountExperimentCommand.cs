namespace BusTicketsSystem.Client.Core.Commands
{
    using System;
    using Dtos.BankAccountDtos;
    using Interfaces;
    using Services.Interfaces;

    public class BankAccountExperimentCommand : ICommand
    {
        private readonly IBankAccountService bankAccountService;

        public BankAccountExperimentCommand(IBankAccountService bankAccountService)
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

            var details = this.bankAccountService.ById<BankAccountExperimentDto>(bankAccId);
            return $"{details.Id}: {details.AccountNumber}";
        }
    }
}