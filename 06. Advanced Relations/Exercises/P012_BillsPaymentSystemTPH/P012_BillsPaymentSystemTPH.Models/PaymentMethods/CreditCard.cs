namespace P012_BillsPaymentSystemTPH.Models.PaymentMethods
{
    using System;
    using Enums;

    public class CreditCard : PaymentMethod
    {
        public CardType Type { get; set; }

        public DateTime ExpirationDate { get; set; }
    }
}