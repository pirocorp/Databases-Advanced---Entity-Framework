namespace _05._Facade
{
    using System;

    public static class FacadeDemo
    {
        public static void Main()
        {
            // Facade
            var mortgage = new Mortgage();

            // Evaluate mortgage eligibility for customer
            var customer = new Customer("Ann McKinsey");
            var eligible = mortgage.IsEligible(customer, 125000);

            Console.WriteLine("\n" + customer.Name +
                              " has been " + (eligible ? "Approved" : "Rejected"));
        }
    }
}
