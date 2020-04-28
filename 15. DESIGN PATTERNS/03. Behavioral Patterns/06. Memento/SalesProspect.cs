namespace _06._Memento
{
    using System;

    /// <summary>
    /// The 'Originator' class
    /// </summary>
    public class SalesProspect
    {
        private string _name;
        private string _phone;
        private double _budget;

        public SalesProspect()
        {
        }

        public string Name
        {
            get => this._name;
            set
            {
                this._name = value;
                Console.WriteLine("Name:  " + this._name);
            }
        }

        public string Phone
        {
            get => this._phone;
            set
            {
                this._phone = value;
                Console.WriteLine("Phone: " + this._phone);
            }
        }

        public double Budget
        {
            get => this._budget;
            set
            {
                this._budget = value;
                Console.WriteLine("Budget: " + this._budget);
            }
        }

        public Memento SaveMemento()
        {
            Console.WriteLine("\nSaving state --\n");
            return new Memento(this._name, this._phone, this._budget);
        }

        public void RestoreMemento(Memento memento)
        {
            Console.WriteLine("\nRestoring state --\n");
            this.Name = memento.Name;
            this.Phone = memento.Phone;
            this.Budget = memento.Budget;
        }
    }
}
