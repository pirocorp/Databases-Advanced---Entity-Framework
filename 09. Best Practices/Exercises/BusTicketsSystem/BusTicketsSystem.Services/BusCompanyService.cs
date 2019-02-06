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

    public class BusCompanyService : IBusCompanyService
    {
        private readonly BusTicketsContext context;
        private readonly IMapper mapper;

        public BusCompanyService(BusTicketsContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public TModel ById<TModel>(int id)
            => this.By<TModel>(u => u.Id == id).SingleOrDefault();

        public bool Exists(int id)
            => this.ById<BusCompanyExistsByIdDto>(id) != null;

        public IEnumerable<TModel> FindBy<TModel>(Expression<Func<BusCompany, bool>> predicate)
            => this.By<TModel>(predicate);

        public BusCompany Create(string name, int countryId)
        {
            var busCompany = new BusCompany()
            {
                Name = name,
                CountryId = countryId
            };

            this.context.BusCompanies.Add(busCompany);
            this.context.SaveChanges();
            return busCompany;
        }

        private IEnumerable<TModel> By<TModel>(Expression<Func<BusCompany, bool>> predicate)
        {
            return this.context.BusCompanies
                .Where(predicate)
                .ProjectTo<TModel>(this.mapper.ConfigurationProvider)
                .ToArray();
        }
    }
}