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

    public class CustomerService : ICustomerService
    {
        private readonly BusTicketsContext context;
        private readonly IMapper mapper;

        public CustomerService(BusTicketsContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public TModel ById<TModel>(int id)
            => this.By<TModel>(u => u.Id == id).SingleOrDefault();

        public bool Exists(int id)
            => this.ById<CustomerExistsByIdDto>(id) != null;

        public Customer Create(string firstName, string lastName, int homeTownId)
        {
            var customer = new Customer()
            {
                FirstName = firstName,
                LastName = lastName,
                HomeTownId = homeTownId
            };

            this.context.Customers.Add(customer);
            this.context.SaveChanges();
            return customer;
        }

        private IEnumerable<TModel> By<TModel>(Expression<Func<Customer, bool>> predicate)
        {
            return this.context.Customers
                .Where(predicate)
                .ProjectTo<TModel>(this.mapper.ConfigurationProvider)
                .ToArray();
        }
    }
}