namespace P01_HospitalDatabase
{
    using System.Linq;
    using Data;
    using Microsoft.EntityFrameworkCore;

    public class Startup
    {
        public static void Main()
        {
            var demo = new Demo();
            demo.Run();
        }
    }
}