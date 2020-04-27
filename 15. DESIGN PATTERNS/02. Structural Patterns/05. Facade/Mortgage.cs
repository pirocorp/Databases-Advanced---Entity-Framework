namespace _05._Facade
{
    using System;

    public class Mortgage
    {
        private readonly Bank _bank = new Bank();
        private readonly Loan _loan = new Loan();
        private readonly Credit _credit = new Credit();

        public bool IsEligible(Customer customer, int amount)
        {
            Console.WriteLine($"{customer.Name} applies for {amount:C} loan\n");

            var eligible = true;

            // Check creditworthyness of applicant
            if (!this._bank.HasSufficientSavings(customer, amount))
            {
                eligible = false;
            }
            else if (!this._loan.HasNoBadLoans(customer))
            {
                eligible = false;
            }
            else if (!this._credit.HasGoodCredit(customer))
            {
                eligible = false;
            }

            return eligible;
        }
    }
}
