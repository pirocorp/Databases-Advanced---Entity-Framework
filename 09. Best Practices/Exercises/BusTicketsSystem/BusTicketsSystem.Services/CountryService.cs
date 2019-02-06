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

    public class CountryService : ICountryService
    {
        private readonly BusTicketsContext context;
        private readonly IMapper mapper;

        public CountryService(BusTicketsContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public TModel ById<TModel>(int id)
            => this.By<TModel>(u => u.Id == id).SingleOrDefault();

        public TModel ByName<TModel>(string name)
            => this.By<TModel>(u => u.Name == name).SingleOrDefault();

        public bool Exists(int id)
            => this.ById<CountryExistsByIdDto>(id) != null;

        public bool Exists(string name)
            => this.ByName<CountryExistsByNameDto>(name) != null;

        public Country Create(string countryName)
        {
            var country = new Country()
            {
                Name = countryName
            };

            this.context.Countries.Add(country);
            this.context.SaveChanges();
            return country;
        }

        private IEnumerable<TModel> By<TModel>(Expression<Func<Country, bool>> predicate)
        {
            return this.context.Countries
                .Where(predicate)
                .ProjectTo<TModel>(this.mapper.ConfigurationProvider)
                .ToArray();
        }
    }
}