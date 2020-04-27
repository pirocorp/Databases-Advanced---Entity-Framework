namespace _02._Bridge
{
    using System;

    /// <summary>
    /// The 'RefinedAbstraction' class
    /// </summary>
    public class Customers : CustomersBase
    {
        public Customers(string group) 
            : base(group)
        {
        }

        public override void ShowAll()
        {
            // Add separator lines
            Console.WriteLine();
            Console.WriteLine("------------------------");
            base.ShowAll();
            Console.WriteLine("------------------------");
        }
    }
}
