namespace BusTicketsSystem.Services.Interfaces
{
    using Models;

    public interface IBankAccountService
    {
        TModel ById<TModel>(int id);

        bool Exists(int id);

        BankAccount Create(string accountNumber, int customerId);
    }
}