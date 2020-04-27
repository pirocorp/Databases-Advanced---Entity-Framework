namespace _02._Bridge
{
    using System;

    /// <summary>
    /// The 'Abstraction' class
    /// </summary>
    public class CustomersBase
    {
        private DataObject _dataObject;
        protected readonly string group;

        public CustomersBase(string group)
        {
            this.group = group;
        }

        public DataObject Data
        {
            get => this._dataObject;
            set => this._dataObject = value;
        }

        public virtual void Next()
        {
            this._dataObject.NextRecord();
        }

        public virtual void Prior()
        {
            this._dataObject.PriorRecord();
        }

        public virtual void Add(string customer)
        {
            this._dataObject.AddRecord(customer);
        }

        public virtual void Delete(string customer)
        {
            this._dataObject.DeleteRecord(customer);
        }

        public virtual void Show()
        {
            this._dataObject.ShowRecord();
        }

        public virtual void ShowAll()
        {
            Console.WriteLine("Customer Group: " + this.group);
            this._dataObject.ShowAllRecords();
        }
    }
}
