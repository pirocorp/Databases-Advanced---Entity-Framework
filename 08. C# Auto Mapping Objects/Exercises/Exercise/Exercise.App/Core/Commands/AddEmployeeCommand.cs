namespace Exercise.App.Core.Commands
{
    using Contracts;
    using Dtos;

    public class AddEmployeeCommand : ICommand
    {
        private readonly IEmployeeService employeeService;

        public AddEmployeeCommand(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        public string Execute(string[] args)
        {
            var firstName = args[0];
            var lastName = args[1];
            var salary = decimal.Parse(args[2]);

            var employeeDto = new EmployeeDto
            {
                FirstName = firstName,
                LastName = lastName,
                Salary = salary
            };

            this.employeeService.AddEmployee(employeeDto);
            return $"Employee {firstName} {lastName} was added successfully";
        }
    }
}