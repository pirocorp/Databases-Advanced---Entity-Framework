namespace BusTicketsSystem.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using Data;
    using Dtos;
    using Interfaces;
    using Models;
    using Models.Enums;

    public class TripService : ITripService
    {
        private readonly BusTicketsContext context;
        private readonly IMapper mapper;

        public TripService(BusTicketsContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public TModel ById<TModel>(int id)
            => this.By<TModel>(u => u.Id == id).SingleOrDefault();

        public bool Exists(int id)
            => this.ById<TripExistsByIdDto>(id) != null;

        public IEnumerable<TModel> FindBy<TModel>(Expression<Func<Trip, bool>> predicate)
            => this.By<TModel>(predicate);

        public Trip Create(DateTime departureTime, DateTime arrivalTime, int originBusStationId,
            int destinationBusStationId, int busCompanyId)
        {
            var trip = new Trip()
            {
                DepartureTime = departureTime,
                ArrivalTime = arrivalTime,
                OriginBusStationId = originBusStationId,
                DestinationBusStationId = destinationBusStationId,
                BusCompanyId = busCompanyId
            };

            this.context.Trips.Add(trip);
            this.context.SaveChanges();
            return trip;
        }

        private IEnumerable<TModel> By<TModel>(Expression<Func<Trip, bool>> predicate)
        {
            return this.context.Trips
                .Where(predicate)
                .ProjectTo<TModel>(this.mapper.ConfigurationProvider)
                .ToArray();
        }
    }
}