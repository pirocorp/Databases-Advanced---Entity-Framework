namespace P012_BillsPaymentSystemTPH.Models.PaymentMethods
{
    public abstract class PaymentMethod
    {
        public int PaymentMethodId { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}