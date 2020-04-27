namespace _01._Chain_of_Responsibility
{
    /// <summary>
    /// Class holding request details
    /// </summary>
    public class Purchase
    {
        public Purchase(int number, double amount, string purpose)
        {
            this.Number = number;
            this.Amount = amount;
            this.Purpose = purpose;
        }

        public int Number { get; set; }

        public double Amount { get; set; }

        public string Purpose { get; set; }
    }
}
