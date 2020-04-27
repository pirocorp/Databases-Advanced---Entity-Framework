namespace _05._Facade
{
    /// <summary>
    /// Customer class
    /// </summary>
    public class Customer
    {
        private readonly string _name;

        public Customer(string name)
        {
            this._name = name;
        }

        // Gets the name
        public string Name => this._name;
    }
}
