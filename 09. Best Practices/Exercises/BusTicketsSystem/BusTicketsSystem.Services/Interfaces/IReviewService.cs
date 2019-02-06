namespace BusTicketsSystem.Services.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using Models;

    public interface IReviewService
    {
        TModel ById<TModel>(int id);

        bool Exists(int id);

        Review Create(string content, int busStationId, int customerId, double grade);

        IEnumerable<TModel> FindBy<TModel>(Expression<Func<Review, bool>> predicate);
    }
}