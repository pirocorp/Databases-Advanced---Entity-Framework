using System;
using System.Collections.Generic;

namespace _02._Bridge
{
    /// <summary>
    /// The 'ConcreteImplementor' class
    /// </summary>
    public class CustomersData : DataObject
    {
        private readonly List<string> _customers;
        private int _current;

        public CustomersData()
        {
            this._customers = new List<string>();
            this._current = 0;

            // Loaded from a database 
            this._customers.Add("Jim Jones");
            this._customers.Add("Samual Jackson");
            this._customers.Add("Allen Good");
            this._customers.Add("Ann Stills");
            this._customers.Add("Lisa Giolani");
        }

        public override void NextRecord()
        {
            if (this._current <= this._customers.Count - 1)
            {
                this._current++;
            }
        }

        public override void PriorRecord()
        {
            if (this._current > 0)
            {
                this._current--;
            }
        }

        public override void AddRecord(string customer)
        {
            this._customers.Add(customer);
        }

        public override void DeleteRecord(string customer)
        {
            this._customers.Remove(customer);
        }

        public override void ShowRecord()
        {
            Console.WriteLine(this._customers[this._current]);
        }

        public override void ShowAllRecords()
        {
            foreach (string customer in this._customers)
            {
                Console.WriteLine(" " + customer);
            }
        }
    }
}
