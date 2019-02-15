namespace Forum.Data.Common
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;

    public class EfGenericRepository<T> : IRepository<T>
        where T : class
    {
        private readonly DbContext context;

        public EfGenericRepository(DbContext context)
        {
            this.context = context;
            this.DbSet = this.context.Set<T>();
        }

        protected DbContext Context { get; set; }

        protected DbSet<T> DbSet { get; set; }

        //If you want to use IQueryable you must use properties
        //Otherwise you will get Exceptions from LINQ to Entities 
        public IQueryable<T> All => this.DbSet;

        public void Add(T entity)
        {
            //Classical solution :)
            this.DbSet.Add(entity);

            //For more complex problems
            //Working with many different contexts
            //Checks for current state of entity
            var entry = this.context.Entry(entity);
            entry.State = EntityState.Added;
        }

        public IEnumerable<T> GetAll()
        {
            return this.DbSet.ToArray();
        }

        //Filtering Where() with predicate
        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter)
        {
            return this.DbSet.Where(filter).ToArray();
        }

        //Filtering (Select()) with function
        public IEnumerable<TOut> GetAll<TOut>(Expression<Func<T, TOut>> select)
        {
            return this.DbSet.Select(select).ToArray();
        }

        //Filtering (Where() and Select()) with predicate and function
        public IEnumerable<TOut> GetAll<TOut>(Expression<Func<T, bool>> filter, 
                                              Expression<Func<T, TOut>> select)
        {
            IQueryable<T> result = this.DbSet;

            if (filter != null)
            {
                result = result.Where(filter);
            }

            if (select != null)
            {
                return result.Select(select).ToArray();
            }

            return result.OfType<TOut>().ToArray();
        }

        //Filtering (Where() and Select(), and OrderBy()) with predicate and function
        public IEnumerable<TOut> GetAll<TSort, TOut>(Expression<Func<T, bool>> filter,
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

        public T GetById(object id)
        {
            return this.DbSet.Find(id);
        }

        public void Delete(T entity)
        {
            //Just like Add
            var entry = this.context.Entry(entity);
            entry.State = EntityState.Deleted;
        }

        public void Update(T entity)
        {
            //Just like Add
            var entry = this.context.Entry(entity);
            entry.State = EntityState.Modified;
        }
    }
}