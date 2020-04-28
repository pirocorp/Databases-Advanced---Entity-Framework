namespace _07._Observer
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The 'Subject' abstract class
    /// </summary>
    public class Stock
    {
        private string _symbol;
        private double _price;
        private readonly List<IInvestor> _investors;

        public Stock(string symbol, double price)
        {
            this._symbol = symbol;
            this._price = price;
            this._investors = new List<IInvestor>();
        }

        public void Attach(IInvestor investor)
        {
            this._investors.Add(investor);
        }

        public void Detach(IInvestor investor)
        {
            this._investors.Remove(investor);
        }

        public void Notify()
        {
            foreach (var investor in this._investors)
            {
                investor.Update(this);
            }

            Console.WriteLine("");
        }

        public double Price
        {
            get => this._price;
            set

            {
                if (Math.Abs(this._price - value) > 0.0000001)
                {
                    this._price = value;
                    this.Notify();
                }
            }
        }

        public string Symbol => this._symbol;
    }
}
