namespace BusTicketsSystem.Services.Interfaces
{
    using Models;

    public interface ICustomerService
    {
        TModel ById<TModel>(int id);

        bool Exists(int id);

        Customer Create(string firstName, string lastName, int homeTownId);
    }
}