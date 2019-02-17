namespace TeamBuilder.Services.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    public interface IGenericService<T> where T : class
    {
        IQueryable<T> All { get; }

        T GetById(object id);

        void Add(T entity);

        void Update(T entity);

        IEnumerable<T> GetAll();

        IEnumerable<T> GetAll(Expression<Func<T, bool>> filter);

        IEnumerable<TOut> GetAll<TOut>(Expression<Func<T, TOut>> select);

        IEnumerable<TOut> GetAll<TOut>(Expression<Func<T, bool>> filter, Expression<Func<T, TOut>> select);

        IEnumerable<TOut> GetAll<TSort, TOut>(Expression<Func<T, bool>> filter, Expression<Func<T, TSort>> sort, Expression<Func<T, TOut>> select);

        IEnumerable<TModel> ProjectTo<TModel>();

        IEnumerable<TModel> ProjectTo<TModel>(Expression<Func<T, bool>> filter);

        IEnumerable<TModel> ProjectTo<TSort, TModel>(Expression<Func<T, TSort>> sort);

        IEnumerable<TModel> ProjectTo<TSort, TModel>(Expression<Func<T, bool>> filter, Expression<Func<T, TSort>> sort);
    }
}