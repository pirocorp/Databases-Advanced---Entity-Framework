namespace P012_BillsPaymentSystemTPH.Models.PaymentMethods
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class BankAccount : PaymentMethod
    {
        public string BankName { get; set; }

        public string Swift { get; set; }
    }
}