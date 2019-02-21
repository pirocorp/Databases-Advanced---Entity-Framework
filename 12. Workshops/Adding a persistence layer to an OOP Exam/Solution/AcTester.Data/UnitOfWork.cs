namespace AcTester.Data
{
    using Interfaces;
    using Models;
    using Repositories;

    public class UnitOfWork : IUnitOfWork
    {
        private IRepository<AirConditioner> airConditionersRepo;
        private IRepository<Report> reportsRepo;
        private AcTesterContext context;

        public UnitOfWork()
        {
            this.context = new AcTesterContext();
        }

        public IRepository<AirConditioner> AirConditionersRepo
        {
            get
            {
                return this.airConditionersRepo ?? (this.airConditionersRepo = new Repository<AirConditioner>(this.context.AirConditioners));
            }
        }

        public IRepository<Report> ReportsRepo
        {
            get { return this.reportsRepo ?? (this.reportsRepo = new Repository<Report>(this.context.Reports)); }
        }

        public void Save()
        {
            this.context.SaveChanges();
        }
    }
}
