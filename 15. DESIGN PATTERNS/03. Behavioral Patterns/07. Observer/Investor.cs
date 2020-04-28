namespace _07._Observer
{
    using System;

    /// <summary>
    /// The 'ConcreteObserver' class
    /// </summary>
    public class Investor : IInvestor
    {
        private readonly string _name;
        private Stock _stock;

        public Investor(string name)
        {
            this._name = name;
        }

        public void Update(Stock stock)
        {
            Console.WriteLine("Notified {0} of {1}'s " + "change to {2:C}", this._name, stock.Symbol, stock.Price);
        }

        public Stock Stock
        {
            get => this._stock;
            set => this._stock = value;
        }
    }
}
