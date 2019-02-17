namespace TeamBuilder.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    
    using Microsoft.EntityFrameworkCore;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using Data;
    using Interfaces;


    public abstract class GenericService<T> : IGenericService<T> 
        where T : class
    {
        private readonly TeamBuilderContext context;
        private readonly IMapper mapper;
        private readonly DbSet<T> dbSet;

        protected GenericService(TeamBuilderContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
            this.dbSet = this.context.Set<T>();
        }

        protected DbSet<T> DbSet => this.dbSet;

        //Exposing IQueryable as Property
        public IQueryable<T> All => this.DbSet;

        public T GetById(object id)
        {
            return this.DbSet.Find(id);
        }

        public void Add(T entity)
        {
            //For more complex problems
            //Working with many different contexts
            //Checks for current state of entity
            var entry = this.context.Entry(entity);
            entry.State = EntityState.Added;
            this.context.SaveChanges();
        }

        public void Update(T entity)
        {
            //Just like Add
            var entry = this.context.Entry(entity);
            entry.State = EntityState.Modified;
            this.context.SaveChanges();
        }

        protected void Delete(T entity)
        {
            //Just like Add
            var entry = this.context.Entry(entity);
            entry.State = EntityState.Deleted;
            this.context.SaveChanges();
        }

        //Just GetAll Elements
        public IEnumerable<T> GetAll()
        {
            return this.GetAll<T, T>(null, null, null);
        }

        //Filtering Where() with predicate
        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter)
        {
            return this.GetAll<T, T>(filter, null, null);
        }

        //Filtering (Select()) with function
        public IEnumerable<TOut> GetAll<TOut>(Expression<Func<T, TOut>> select)
        {
            return this.GetAll<TOut, TOut>(null, null, select);
        }

        //Filtering (Where() and Select()) with predicate and function
        public IEnumerable<TOut> GetAll<TOut>(
            Expression<Func<T, bool>> filter,
            Expression<Func<T, TOut>> select)
        {
            return this.GetAll<TOut, TOut>(filter, null, select);
        }

        //Filtering (Where() and Select(), and OrderBy()) with predicate and function
        public IEnumerable<TOut> GetAll<TSort, TOut>(
            Expression<Func<T, bool>> filter,
            Expression<Func<T, TSort>> sort,
            Expression<Func<T, TOut>> select)
        {
            IQueryable<T> result = this.DbSet;

            if (filter != null)
            {
                result = result.Where(filter);
            }

            if (sort != null)
            {
                result = result.OrderBy(sort);
            }

            if (select != null)
            {
                return result.Select(select).ToArray();
            }

            return result.OfType<TOut>().ToArray();
        }

        //Just ProjectTo
        public IEnumerable<TModel> ProjectTo<TModel>()
        {
            return this.ProjectTo<TModel, TModel>(null, null);
        }

        //ProjectTo With Where()
        public IEnumerable<TModel> ProjectTo<TModel>(
            Expression<Func<T, bool>> filter)
        {
            return this.ProjectTo<TModel, TModel>(filter, null);
        }

        //ProjectTo with OrderBy()
        public IEnumerable<TModel> ProjectTo<TSort, TModel>(
            Expression<Func<T, TSort>> sort)
        {
            return this.ProjectTo<TSort, TModel>(null, sort);
        }

        //ProjectTo with Where() and OrderBy()
        public IEnumerable<TModel> ProjectTo<TSort, TModel>(
            Expression<Func<T, bool>> filter,
            Expression<Func<T, TSort>> sort)
        {
            IQueryable<T> result = this.DbSet;

            if (filter != null)
            {
                result = result.Where(filter);
            }

            if (sort != null)
            {
                result = result.OrderBy(sort);
            }

            return result
                .ProjectTo<TModel>(this.mapper.ConfigurationProvider)
                .ToArray();
        }
    }
}