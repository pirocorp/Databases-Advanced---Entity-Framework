namespace MyApp.Core.Commands
{
    using System;
    using System.Linq;
    using System.Text;
    using Data;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// ListEmployeesOlderThan &lt;age&gt;
    /// </summary>
    public class ListEmployeesOlderThanCommand : ICommand
    {
        private readonly MyAppContext _context;

        public ListEmployeesOlderThanCommand(MyAppContext context)
        {
            this._context = context;
        }

        public string Execute(string[] inputArgs)
        {
            var age = int.Parse(inputArgs[0]);

            var givenDate = DateTime.Now;
            givenDate = givenDate.AddYears(-age);

            var employees = this._context.Employees
                .Include(e => e.Manager)
                .Where(e => e.BirthDay.Value <= givenDate)
                .ToList();

            var result = new StringBuilder();

            foreach (var e in employees)
            {
                result.AppendLine($"{e.FirstName} {e.LastName} - ${e.Salary:F2} - Manager: {(e.Manager == null ? "[no manager]" : $"{e.Manager.FirstName} {e.Manager.LastName}")}");
            }

            return result.ToString().TrimEnd();
        }
    }
}
