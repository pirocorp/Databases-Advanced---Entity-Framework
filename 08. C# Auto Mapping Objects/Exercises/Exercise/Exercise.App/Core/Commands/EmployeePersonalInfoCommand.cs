namespace Exercise.App.Core.Commands
{
    using System;

    using Contracts;

    public class EmployeePersonalInfoCommand : ICommand
    {
        private readonly IEmployeeService employeeService;

        public EmployeePersonalInfoCommand(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        public string Execute(string[] args)
        {
            var id = int.Parse(args[0]);

            var dto = this.employeeService.GetEmployeePersonalInfo(id);

            return $"ID: {dto.Id} - {dto.FirstName} {dto.LastName} - ${dto.Salary:F2}" + Environment.NewLine +
                   $"Birthday: {dto.Birthday?.ToString("dd-MM-yyyy")}" + Environment.NewLine +
                   $"Address: {dto.Address}";
        }
    }
}