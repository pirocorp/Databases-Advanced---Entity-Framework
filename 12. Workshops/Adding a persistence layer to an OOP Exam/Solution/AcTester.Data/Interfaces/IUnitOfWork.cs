namespace AcTester.Data.Interfaces
{
    using Models;

    public interface IUnitOfWork
    {
        IRepository<AirConditioner> AirConditionersRepo { get; }

        IRepository<Report> ReportsRepo { get; }
        void Save();
    }
}
