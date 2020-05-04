namespace MyApp.Core.Commands
{
    using System.Linq;
    using Data;

    /// <summary>
    /// SetAddress &lt;employeeId&gt; &lt;address&gt;
    /// <para>sets the address of the employee to the given string</para>
    /// </summary>
    public class SetAddressCommand : ICommand
    {
        private readonly MyAppContext _context;

        public SetAddressCommand(MyAppContext context)
        {
            this._context = context;
        }

        public string Execute(string[] inputArgs)
        {
            var employeeId = int.Parse(inputArgs[0]);
            var address = string.Join(" ", inputArgs.Skip(1));

            var employee = this._context.Employees
                .Find(employeeId);

            employee.Address = address;
            this._context.SaveChanges();

            return $"Employee {employee.FirstName} {employee.LastName} has set address: {employee.Address}";
        }
    }
}
