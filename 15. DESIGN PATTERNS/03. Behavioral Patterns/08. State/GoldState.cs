namespace _08._State
{
    /// <summary>
    /// A 'ConcreteState' class
    /// <remarks>
    /// Gold indicates an interest bearing state
    /// </remarks>
    /// </summary>
    public class GoldState : State
    {
        public GoldState(State state)
            : this(state.Balance, state.Account)
        {
        }

        public GoldState(double balance, Account account)
        {
            this.balance = balance;
            this.account = account;
            this.Initialize();
        }

        private void Initialize()
        {
            // Should come from a database
            this.interest = 0.05;
            this.lowerLimit = 1000.0;
            this.upperLimit = 10000000.0;
        }

        public override void Deposit(double amount)
        {
            this.balance += amount;
            this.StateChangeCheck();
        }

        public override void Withdraw(double amount)
        {
            this.balance -= amount;
            this.StateChangeCheck();
        }

        public override void PayInterest()
        {
            this.balance += this.interest * this.balance;
            this.StateChangeCheck();
        }

        private void StateChangeCheck()
        {
            if (this.balance < 0.0)
            {
                this.account.State = new RedState(this);
            }
            else if (this.balance < this.lowerLimit)
            {
                this.account.State = new SilverState(this);
            }
        }
    }
}
