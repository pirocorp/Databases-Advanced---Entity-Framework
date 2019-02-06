namespace BusTicketsSystem.Services.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Models;

    public interface IBusStationService
    {
        TModel ById<TModel>(int id);

        IEnumerable<TModel> FindBy<TModel>(Expression<Func<BusStation, bool>> predicate);

        bool Exists(int id);

        BusStation Create(string name, int countryId);
    }
}