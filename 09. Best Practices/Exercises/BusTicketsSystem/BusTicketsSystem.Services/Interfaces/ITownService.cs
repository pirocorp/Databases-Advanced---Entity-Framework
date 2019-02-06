namespace BusTicketsSystem.Services.Interfaces
{
    using Models;

    public interface ITownService
    {
        TModel ById<TModel>(int id);

        bool Exists(int id);

        Town Create(string name, int countryId);
    }
}