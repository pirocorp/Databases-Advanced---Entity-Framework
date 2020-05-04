namespace MyApp.Core.Commands
{
    using System;
    using System.Globalization;
    using Data;

    /// <summary>
    /// SetBirthday &lt;employeeId&gt; &lt;date: "dd-MM-yyyy"&gt;
    /// <para>sets the birthday of the employee to the given date</para>
    /// </summary>
    public class SetBirthdayCommand : ICommand
    {
        private const string DATE_TIME_FORMAT = "dd-MM-yyyy";
        private readonly MyAppContext _context;

        public SetBirthdayCommand(MyAppContext context)
        {
            this._context = context;
        }

        public string Execute(string[] inputArgs)
        {
            var employeeId = int.Parse(inputArgs[0]);

            var birthDay = DateTime.ParseExact(inputArgs[2], DATE_TIME_FORMAT, CultureInfo.InvariantCulture);

            var employee = this._context.Employees.Find(employeeId);
            employee.BirthDay = birthDay;

            this._context.SaveChanges();
            return $"On employee: {employee.FirstName} {employee.LastName} is set birthday: {employee.BirthDay.Value.ToString(DATE_TIME_FORMAT)}";
        }
    }
}
