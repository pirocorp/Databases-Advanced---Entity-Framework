namespace MyApp.Core.Commands
{
    using System;
    using Automapper;
    using Data;

    /// <summary>
    /// SetManager &lt;employeeId&gt; &lt;managerId&gt;
    /// </summary>
    public class SetManagerCommand : ICommand
    {
        private readonly MyAppContext _context;

        public SetManagerCommand(MyAppContext context, Mapper mapper)
        {
            this._context = context;
        }

        public string Execute(string[] inputArgs)
        {
            var employeeId = int.Parse(inputArgs[0]);
            var managerId = int.Parse(inputArgs[1]);

            var employee = this._context.Employees.Find(employeeId);
            var manager = this._context.Employees.Find(managerId);

            if (manager == null || employee == null)
            {
                throw new ArgumentException("Employ not found.");
            }

            employee.Manager = manager;

            this._context.SaveChanges();

            return $"Employee: {employee.FirstName} {employee.LastName} is managed by: {manager.FirstName} {manager.LastName}";
        }
    }
}
