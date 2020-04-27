namespace _02._Bridge
{
    public static class BridgeDemo
    {
        public static void Main()
        {
            // Create RefinedAbstraction
            var customers = new Customers("Chicago");

            // Set ConcreteImplementor
            customers.Data = new CustomersData();

            // Exercise the bridge
            customers.Show();
            customers.Next();
            customers.Show();
            customers.Next();
            customers.Show();
            customers.Add("Henry Velasquez");

            customers.ShowAll();
        }
    }
}
