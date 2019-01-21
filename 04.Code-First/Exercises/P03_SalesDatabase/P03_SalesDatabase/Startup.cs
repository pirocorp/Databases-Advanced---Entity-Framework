namespace P03_SalesDatabase
{
    using System.Linq;

    using Data;

    public class Startup
    {
        public static void Main()
        {
            var db = new SalesContext();

            var result = db.Sales.ToList();
        }
    }
}
