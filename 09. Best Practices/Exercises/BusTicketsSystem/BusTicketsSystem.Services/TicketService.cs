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

    public class TicketService : ITicketService
    {
        private readonly BusTicketsContext context;
        private readonly IMapper mapper;

        public TicketService(BusTicketsContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public TModel ById<TModel>(int id)
            => this.By<TModel>(u => u.Id == id).SingleOrDefault();

        public bool Exists(int id)
            => this.ById<TicketExistsByIdDto>(id) != null;

        public IEnumerable<TModel> FindBy<TModel>(Expression<Func<Ticket, bool>> predicate)
            => this.By<TModel>(predicate);

        public Ticket Create(decimal price, string seat, int customerId, int tripId)
        {
            var ticket = new Ticket()
            {
                Price = price,
                Seat = seat,
                CustomerId = customerId,
                TripId = tripId
            };

            this.context.Add(ticket);
            this.context.SaveChanges();
            return ticket;
        }

        private IEnumerable<TModel> By<TModel>(Expression<Func<Ticket, bool>> predicate)
        {
            return this.context.Tickets
                .Where(predicate)
                .ProjectTo<TModel>(this.mapper.ConfigurationProvider)
                .ToArray();
        }
    }
}