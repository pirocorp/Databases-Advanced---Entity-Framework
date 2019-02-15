namespace Forum.Data.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    public interface IRepository<T>
        where T: class
    {
        IQueryable<T> All { get; }

        IEnumerable<T> GetAll();

        IEnumerable<T> GetAll(Expression<Func<T, bool>> filter);

        IEnumerable<TOut> GetAll<TOut>(Expression<Func<T, TOut>> select);

        IEnumerable<TOut> GetAll<TOut>(Expression<Func<T, bool>> filter,
            Expression<Func<T, TOut>> select);

        IEnumerable<TOut> GetAll<TSort, TOut>(Expression<Func<T, bool>> filter,
            Expression<Func<T, TSort>> sort,
            Expression<Func<T, TOut>> select);

        T GetById(object id);

        void Add(T entity);

        void Delete(T entity);

        void Update(T entity);
    }
}