namespace P012_BillsPaymentSystemTPH.Models
{
    using System.Collections.Generic;
    using PaymentMethods;

    public class User
    {
        public int UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public ICollection<PaymentMethod> PaymentMethods { get; set; }
    }
}
