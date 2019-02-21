namespace AcTester.Data.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using Interfaces;

    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private DbSet<TEntity> set;

        public Repository(DbSet<TEntity> entitySet)
        {
            this.set = entitySet;
        }

        public void Add(TEntity entity)
        {
            this.set.Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            this.set.AddRange(entities);
        }

        public void Remove(TEntity entity)
        {
            this.set.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                this.Remove(entity);
            }
        }

        public TEntity Find(int id)
        {
            return this.set.Find(id);
        }

        public TEntity First()
        {
            return this.set.FirstOrDefault();
        }

        public TEntity First(Expression<Func<TEntity, bool>> where)
        {
            return this.set.FirstOrDefault(where);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return this.set;
        }

        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> where)
        {
            return this.set.Where(where);
        }

        public int Count()
        {
            return this.set.Count();
        }

        public int Count(Expression<Func<TEntity, bool>> where)
        {
            return this.set.Count(where);
        }
    }
}
