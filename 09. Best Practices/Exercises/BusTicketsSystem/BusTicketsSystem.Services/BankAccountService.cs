namespace BusTicketsSystem.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using Data;
    using Dtos;
    using Interfaces;
    using Models;

    public class BankAccountService : IBankAccountService
    {
        private readonly BusTicketsContext context;
        private readonly IMapper mapper;

        public BankAccountService(BusTicketsContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public TModel ById<TModel>(int id)
            => this.By<TModel>(u => u.Id == id).SingleOrDefault();

        public TModel ByName<TModel>(string accNumber)
            => this.By<TModel>(u => u.AccountNumber == accNumber).SingleOrDefault();

        public bool Exists(int id)
            => this.ById<BankAccountExistsByIdDto>(id) != null;

        public bool Exists(string name)
            => this.ByName<BankAccountExistsByBankAccountDto>(name) != null;

        public BankAccount Create(string accountNumber, int customerId)
        {
            var bankAccount = new BankAccount()
            {
                AccountNumber = accountNumber,
                CustomerId = customerId
            };

            this.context.BankAccounts.Add(bankAccount);
            this.context.SaveChanges();
            return bankAccount;
        }

        public IEnumerable<TModel> FindBy<TModel>(Expression<Func<BankAccount, bool>> predicate)
            => this.By<TModel>(predicate);

        public void Withdraw(int accId, decimal amount)
        {
            var bankAcc = this.context.BankAccounts.FirstOrDefault(b => b.Id == accId);

            if (bankAcc == null)
            {
                throw new ArgumentException("Bank account doesn't exists!");
            }

            if (bankAcc.Balance < amount)
            {
                throw new ArgumentException("Insufficient account balance");
            }

            bankAcc.Balance -= amount;
            this.context.SaveChanges();
        }

        private IEnumerable<TModel> By<TModel>(Expression<Func<BankAccount, bool>> predicate)
        {
            return this.context.BankAccounts
                .Where(predicate)
                .ProjectTo<TModel>(this.mapper.ConfigurationProvider)
                .ToArray();
        }
    }
}