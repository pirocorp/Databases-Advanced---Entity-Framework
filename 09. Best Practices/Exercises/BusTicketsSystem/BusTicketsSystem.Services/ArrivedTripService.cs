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

    public class ArrivedTripService : IArrivedTripService
    {
        private readonly BusTicketsContext context;
        private readonly IMapper mapper;

        public ArrivedTripService(BusTicketsContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public TModel ById<TModel>(int id)
            => this.By<TModel>(u => u.Id == id).SingleOrDefault();

        public bool Exists(int id)
            => this.ById<ArrivedTripExistsByIdDto>(id) != null;

        public IEnumerable<TModel> FindBy<TModel>(Expression<Func<ArrivedTrip, bool>> predicate)
            => this.By<TModel>(predicate);

        public ArrivedTrip Create(DateTime actualArrivalTime, int passengersCount,
            int originBusStationId, int destinationBusStationId)
        {
            var arrivedTrip = new ArrivedTrip()
            {
                ActualArrivalTime = actualArrivalTime,
                PassengersCount = passengersCount,
                OriginBusStationId = originBusStationId,
                DestinationBusStationId = destinationBusStationId
            };

            this.context.ArrivedTrips.Add(arrivedTrip);
            this.context.SaveChanges();
            return arrivedTrip;
        }


        private IEnumerable<TModel> By<TModel>(Expression<Func<ArrivedTrip, bool>> predicate)
        {
            return this.context.ArrivedTrips
                .Where(predicate)
                .ProjectTo<TModel>(this.mapper.ConfigurationProvider)
                .ToArray();
        }
    }
}