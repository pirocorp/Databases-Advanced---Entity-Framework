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

    public class TownService : ITownService
    {
        private readonly BusTicketsContext context;
        private readonly IMapper mapper;

        public TownService(BusTicketsContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public TModel ById<TModel>(int id)
            => this.By<TModel>(u => u.Id == id).SingleOrDefault();

        public bool Exists(int id)
            => this.ById<TownExistsByIdDto>(id) != null;

        public Town Create(string name, int countryId)
        {
            var town = new Town()
            {
                Name = name,
                CountryId = countryId
            };

            this.context.Towns.Add(town);
            this.context.SaveChanges();
            return town;
        }

        private IEnumerable<TModel> By<TModel>(Expression<Func<Town, bool>> predicate)
        {
            return this.context.Towns
                .Where(predicate)
                .ProjectTo<TModel>(this.mapper.ConfigurationProvider)
                .ToArray();
        }
    }
}