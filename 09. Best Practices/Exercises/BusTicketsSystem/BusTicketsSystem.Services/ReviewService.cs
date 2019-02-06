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

    public class ReviewService : IReviewService
    {
        private readonly BusTicketsContext context;
        private readonly IMapper mapper;

        public ReviewService(BusTicketsContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public TModel ById<TModel>(int id)
            => this.By<TModel>(u => u.Id == id).SingleOrDefault();

        public bool Exists(int id)
            => this.ById<ReviewExistsByIdDto>(id) != null;

        public IEnumerable<TModel> FindBy<TModel>(Expression<Func<Review, bool>> predicate)
            => this.By<TModel>(predicate);

        public Review Create(string content, int busStationId, int customerId, double grade)
        {
            var review = new Review()
            {
                Content = content,
                BusStationId = busStationId,
                CustomerId = customerId,
                Grade = grade
            };

            this.context.Reviews.Add(review);
            this.context.SaveChanges();
            return review;
        }

        private IEnumerable<TModel> By<TModel>(Expression<Func<Review, bool>> predicate)
        {
            return this.context.Reviews
                .Where(predicate)
                .ProjectTo<TModel>(this.mapper.ConfigurationProvider)
                .ToArray();
        }
    }
}