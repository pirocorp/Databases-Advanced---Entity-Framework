namespace ACTester.Database
{
    using Interfaces;

    public class AirConditionerTesterDatabase : IAirConditionerTesterDatabase
    {
        public AirConditionerTesterDatabase()
        {
            this.AirConditioners = new Repository<IAirConditioner>();
            this.Reports = new ReportsRepository();
        }

        public IRepository<IAirConditioner> AirConditioners { get; private set; }

        public IReportsRepository Reports { get; private set; }   
    }
}
