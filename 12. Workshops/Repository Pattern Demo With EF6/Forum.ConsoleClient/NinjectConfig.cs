namespace Forum.ConsoleClient
{
    using System;
    using Ninject.Modules;

    public class NinjectConfig : NinjectModule
    {
        public override void Load()
        {
            this.Bind<DbContext>()
        }
    }
}