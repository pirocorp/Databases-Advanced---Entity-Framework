namespace Forum.Data.Common
{
    using System.Data.Entity;

    public class EfUnitOfWork : IUnitOfWork
    {
        private readonly DbContext context;

        public EfUnitOfWork(DbContext context)
        {
            this.context = context;
        }

        public void Commit()
        {
            this.context.SaveChanges();
        }

        public void Dispose()
        {
        }
    }
}