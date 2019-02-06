namespace BusTicketsSystem.Services.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using Models;

    public interface IBusCompanyService
    {
        TModel ById<TModel>(int id);

        bool Exists(int id);

        IEnumerable<TModel> FindBy<TModel>(Expression<Func<BusCompany, bool>> predicate);

        BusCompany Create(string name, int countryId);
    }
}