namespace Exercise.App.Core.Commands
{
    using Contracts;

    public class EmployeeInfoCommand : ICommand
    {
        private readonly IEmployeeService employeeService;

        public EmployeeInfoCommand(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        public string Execute(string[] args)
        {
            var id = int.Parse(args[0]);

            var employeeDto = this.employeeService.GetEmployeeInfo(id);

            return $"ID: {employeeDto.Id} - {employeeDto.FirstName} {employeeDto.LastName} -  ${employeeDto.Salary:F2}";
        }
    }
}