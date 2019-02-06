namespace BusTicketsSystem.Services.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using Models;

    public interface IBankAccountService
    {
        TModel ById<TModel>(int id);

        bool Exists(int id);

        IEnumerable<TModel> FindBy<TModel>(Expression<Func<BankAccount, bool>> predicate);

        BankAccount Create(string accountNumber, int customerId);

        void Withdraw(int accId, decimal amount);
    }
}