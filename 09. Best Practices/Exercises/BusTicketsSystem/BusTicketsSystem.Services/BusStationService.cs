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

    public class BusStationService : IBusStationService
    {
        private readonly BusTicketsContext context;
        private readonly IMapper mapper;

        public BusStationService(BusTicketsContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public TModel ById<TModel>(int id)
            => this.By<TModel>(u => u.Id == id).SingleOrDefault();

        public bool Exists(int id)
            => this.ById<BusStationExistsByIdDto>(id) != null;

        public IEnumerable<TModel> FindBy<TModel>(Expression<Func<BusStation, bool>> predicate)
            => this.By<TModel>(predicate);

        public BusStation Create(string name, int townId)
        {
            var busStation = new BusStation()
            {
                Name = name,
                TownId = townId
            };

            this.context.BusStations.Add(busStation);
            this.context.SaveChanges();
            return busStation;
        }

        private IEnumerable<TModel> By<TModel>(Expression<Func<BusStation, bool>> predicate)
        {
            return this.context.BusStations
                .Where(predicate)
                .ProjectTo<TModel>(this.mapper.ConfigurationProvider)
                .ToArray();
        }
    }
}