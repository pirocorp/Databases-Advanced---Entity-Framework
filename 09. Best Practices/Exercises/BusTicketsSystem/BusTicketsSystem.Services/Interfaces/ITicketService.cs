namespace BusTicketsSystem.Services.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using Models;

    public interface ITicketService
    {
        TModel ById<TModel>(int id);

        bool Exists(int id);

        IEnumerable<TModel> FindBy<TModel>(Expression<Func<Ticket, bool>> predicate);

        Ticket Create(decimal price, string seat, int customerId, int tripId);
    }
}