namespace BusTicketsSystem.Services.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using Models;
    using Models.Enums;

    public interface ITripService
    {
        TModel ById<TModel>(int id);

        bool Exists(int id);

        IEnumerable<TModel> FindBy<TModel>(Expression<Func<Trip, bool>> predicate);

        Trip Create(DateTime departureTime, DateTime arrivalTime, int originBusStationId,
            int destinationBusStationId, int busCompanyId);

        Trip ChangeStatus(int tripId, Status status);
    }
}