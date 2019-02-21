namespace AcTester.Data
{
    using System.Data.Entity;
    using Models;

    public class AcTesterContext : DbContext
    {
        public AcTesterContext()
            : base("name=AcTesterContext")
        {
        }

        public DbSet<AirConditioner> AirConditioners { get; set; }

        public DbSet<Report> Reports { get; set; }
    }

}