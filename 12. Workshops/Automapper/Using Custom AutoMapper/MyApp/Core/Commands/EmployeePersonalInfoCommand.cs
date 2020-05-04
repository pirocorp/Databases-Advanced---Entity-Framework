namespace MyApp.Core.Commands
{
    using System.Text;
    using Automapper;
    using Data;
    using ViewModels;

    /// <summary>
    /// EmployeePersonalInfo &lt;employeeId&gt; 
    /// </summary>
    public class EmployeePersonalInfoCommand : ICommand
    {
        private const string DATE_TIME_FORMAT = "dd-MM-yyyy";

        private readonly MyAppContext _context;
        private readonly Mapper _mapper;

        public EmployeePersonalInfoCommand(MyAppContext context, Mapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public string Execute(string[] inputArgs)
        {
            var employeeId = int.Parse(inputArgs[0]);

            var employee = this._context.Employees.Find(employeeId);

            var info = this._mapper.CreateMappedObject<EmployeePersonalInfoDto>(employee);

            var result = new StringBuilder();

            result.AppendLine($"ID: {info.Id} - {info.FirstName} {info.LastName} - ${info.Salary:F2}");
            result.AppendLine($"Birthday: {info.BirthDay?.ToString(DATE_TIME_FORMAT)}");
            result.AppendLine($"Address: {info.Address}");

            return result.ToString().TrimEnd();
        }
    }
}
