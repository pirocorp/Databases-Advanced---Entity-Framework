namespace Forum.Console
{
    using System;
    using System.Data.Entity;

    using Ninject.Modules;

    using Data;
    using Data.Common;
    using Ninject;

    public class NinjectConfig : NinjectModule
    {
        public override void Load()
        {
            this.Bind<DbContext>().To<ForumDbContext>().InSingletonScope();
            this.Bind(typeof(IRepository<>)).To(typeof(EfGenericRepository<>));

            //This binds Func<IUnitOfWork> to Method Get<EfUnitOfWork>
            //So if we call Get<EfUnitOfWork>() we will receive instance of EfUnitOfWork
            //Everywhere we have dependency Func<IUnitOfWork> Ninject injects Get<EfUnitOfWork>()
            //So when we call this method Get<EfUnitOfWork>() we receive  instance of EfUnitOfWork
            this.Bind<Func<IUnitOfWork>>().ToMethod(ctx =>() => ctx.Kernel.Get<EfUnitOfWork>());
            this.Bind<IUnitOfWork>().To<EfUnitOfWork>();
        }
    }
}