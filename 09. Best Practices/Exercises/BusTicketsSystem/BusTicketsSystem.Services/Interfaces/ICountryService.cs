namespace BusTicketsSystem.Services.Interfaces
{
    using Models;

    public interface ICountryService
    {
        TModel ById<TModel>(int id);

        TModel ByName<TModel>(string name);

        bool Exists(int id);

        bool Exists(string name);

        Country Create(string countryName);
    }
}