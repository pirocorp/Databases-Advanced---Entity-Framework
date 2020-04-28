namespace _10._Template_Method
{
    using System.Data;

    /// <summary>
    /// The 'AbstractClass' abstract class
    /// </summary>
    public abstract class DataAccessObject
    {
        protected string connectionString;
        protected DataSet dataSet;

        public virtual void Connect()
        {
            // Make sure mdb is available to app

            this.connectionString =
                "provider=Microsoft.JET.OLEDB.4.0; " +
                "data source=..\\..\\..\\db1.mdb";
        }

        public abstract void Select();

        public abstract void Process();

        public virtual void Disconnect()
        {
            this.connectionString = "";
        }

        // The 'Template Method'
        public void Run()
        {
            this.Connect();
            this.Select();
            this.Process();
            this.Disconnect();
        }
    }
}
