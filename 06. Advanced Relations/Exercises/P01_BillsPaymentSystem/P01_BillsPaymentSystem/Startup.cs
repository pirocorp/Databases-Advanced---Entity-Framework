namespace P01_BillsPaymentSystem
{
    using Microsoft.EntityFrameworkCore;

    using System.Linq;
    using Data;
    using Initializer;

    public class Startup
    {
        public static void Main()
        {
            var db = new BillsPaymentSystemContext();
            
            var result = db.Users
                .Include(u => u.PaymentMethods)
                .ThenInclude(pm => pm.BankAccount)
                .Include(u => u.PaymentMethods)
                .ThenInclude(pm => pm.CreditCard)
                .ToList();
        }
    }
}
