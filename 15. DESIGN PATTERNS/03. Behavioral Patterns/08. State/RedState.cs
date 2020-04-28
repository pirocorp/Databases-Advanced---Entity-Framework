namespace _08._State
{
    using System;

    /// <summary>
    /// A 'ConcreteState' class
    /// <remarks>
    /// Red indicates that account is overdrawn 
    /// </remarks>
    /// </summary>
    public class RedState : State
    {
        private double _serviceFee;

        public RedState(State state)
        {
            this.balance = state.Balance;
            this.account = state.Account;
            this.Initialize();
        }

        private void Initialize()
        {
            // Should come from a datasource
            this.interest = 0.0;
            this.lowerLimit = -100.0;
            this.upperLimit = 0.0;
            this._serviceFee = 15.00;
        }

        public override void Deposit(double amount)
        {
            this.balance += amount;
            this.StateChangeCheck();
        }

        public override void Withdraw(double amount)
        {
            amount = amount - this._serviceFee;
            Console.WriteLine("No funds available for withdrawal!");
        }

        public override void PayInterest()
        {
            // No interest is paid
        }

        private void StateChangeCheck()
        {
            if (this.balance > this.upperLimit)
            {
                this.account.State = new SilverState(this);
            }
        }
    }
}
