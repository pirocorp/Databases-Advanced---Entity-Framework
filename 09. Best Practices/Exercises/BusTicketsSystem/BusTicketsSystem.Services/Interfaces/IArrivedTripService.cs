namespace BusTicketsSystem.Services.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using Models;

    public interface IArrivedTripService
    {
        TModel ById<TModel>(int id);

        bool Exists(int id);

        IEnumerable<TModel> FindBy<TModel>(Expression<Func<ArrivedTrip, bool>> predicate);

        ArrivedTrip Create(DateTime actualArrivalTime, int passengersCount,
            int originBusStationId, int destinationBusStationId);
    }
}