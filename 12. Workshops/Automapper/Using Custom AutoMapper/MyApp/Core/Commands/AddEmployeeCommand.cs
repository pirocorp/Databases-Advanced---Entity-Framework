namespace MyApp.Core.Commands
{
    using Automapper;
    using Data;
    using Models;
    using ViewModels;

    /// <summary>
    /// AddEmployee &lt;firstName&gt; &lt;lastName&gt; &lt;salary&gt;
    /// <para>adds a new Employee to the database</para>
    /// </summary>
    public class AddEmployeeCommand : ICommand
    {
        private readonly MyAppContext _context;
        private readonly Mapper _mapper;

        public AddEmployeeCommand(MyAppContext context, Mapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public string Execute(string[] inputArgs)
        {
            var firstName = inputArgs[0];
            var lastName = inputArgs[1];
            var salary = decimal.Parse(inputArgs[2]);

            //TODO Validate

            var employee = new Employee
            {
                FirstName = firstName,
                LastName = lastName,
                Salary = salary,
            };

            this._context.Employees.Add(employee);
            this._context.SaveChanges();

            var employeeDto = this._mapper.CreateMappedObject<EmployeeDto>(employee);

            var result = $"Registered successfully: {employeeDto.FirstName} {employeeDto.LastName} - {employeeDto.Salary}|";
            return result;
        }
    }
}
