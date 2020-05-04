namespace MyApp.Core.Commands
{
    using Automapper;
    using Data;
    using ViewModels;

    /// <summary>
    /// EmployeeInfo &lt;employeeId&gt; 
    /// </summary>
    public class EmployeeInfoCommand : ICommand
    {
        private readonly MyAppContext _context;
        private readonly Mapper _mapper;

        public EmployeeInfoCommand(MyAppContext context, Mapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public string Execute(string[] inputArgs)
        {
            var employeeId = int.Parse(inputArgs[0]);

            var employee = this._context.Employees.Find(employeeId);

            var info = this._mapper.CreateMappedObject<EmployeeInfoDto>(employee);

            return info.ToString();
        }
    }
}
