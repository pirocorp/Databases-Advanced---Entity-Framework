namespace ACTester.Ninject
{
    using AcTester.Data;
    using AcTester.Data.Interfaces;
    using Controller;
    using Core;
    using global::Ninject.Modules;
    using Interfaces;
    using UI;

    public class InjectModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IActionManager>().To<ActionManager>();
            this.Bind<IAirConditionerTesterSystem>().To<AirConditionerTesterSystem>();
            this.Bind<IUnitOfWork>().To<UnitOfWork>();
            this.Bind<IUserInterface>().To<ConsoleUserInterface>();
        }
    }
}
