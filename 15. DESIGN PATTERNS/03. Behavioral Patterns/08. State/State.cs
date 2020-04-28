namespace _08._State
{
    /// <summary>
    /// The 'State' abstract class
    /// </summary
    public abstract class State
    {
        protected Account account;
        protected double balance;

        protected double interest;
        protected double lowerLimit;
        protected double upperLimit;

        public Account Account
        {
            get => this.account;
            set => this.account = value;
        }

        public double Balance
        {
            get => this.balance;
            set => this.balance = value;
        }

        public abstract void Deposit(double amount);

        public abstract void Withdraw(double amount);

        public abstract void PayInterest();
    }
}
