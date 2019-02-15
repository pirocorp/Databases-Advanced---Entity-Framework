namespace Forum.Console
{
    using System;
    using Data.Common;

    using Models;

    public class MyDataProvider
    {
        private readonly Func<IUnitOfWork> unitOfWork;
        private readonly IRepository<Category> categories;

        public MyDataProvider(Func<IUnitOfWork> unitOfWork, IRepository<Category> categories)
        {
            this.unitOfWork = unitOfWork;
            this.categories = categories;
        }

        public IRepository<Category> Categories => this.categories;

        public Func<IUnitOfWork> UnitOfWork => this.unitOfWork;
    }
}