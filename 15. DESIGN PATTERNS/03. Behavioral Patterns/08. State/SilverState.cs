namespace _08._State
{
    /// <summary>
    /// A 'ConcreteState' class
    /// <remarks>
    /// Silver indicates a non-interest bearing state
    /// </remarks>
    /// </summary>
    public class SilverState : State
    {
        public SilverState(State state) :
            this(state.Balance, state.Account)
        {
        }

        public SilverState(double balance, Account account)
        {
            this.balance = balance;
            this.account = account;
            this.Initialize();
        }

        private void Initialize()
        {
            // Should come from a datasource
            this.interest = 0.0;
            this.lowerLimit = 0.0;
            this.upperLimit = 1000.0;
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
            if (this.balance < this.lowerLimit)
            {
                this.account.State = new RedState(this);
            }
            else if (this.balance > this.upperLimit)
            {
                this.account.State = new GoldState(this);
            }
        }
    }
}
