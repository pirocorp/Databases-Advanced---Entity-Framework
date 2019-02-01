namespace Exercise.App.Core.Commands
{
    using System.Text;
    using Contracts;

    public class ManagerInfoCommand : ICommand
    {
        private readonly IManagerService managerService;

        public ManagerInfoCommand(IManagerService managerService)
        {
            this.managerService = managerService;
        }

        public string Execute(string[] args)
        {
            var employeeId = int.Parse(args[0]);

            var managerDto = this.managerService.GetManagerInfo(employeeId);

            var sb = new StringBuilder();
            sb.AppendLine($"{managerDto.FirstName} {managerDto.LastName} | Employees: {managerDto.EmployeesCount}");

            foreach (var employee in managerDto.EmployeesDtos)
            {
                sb.AppendLine($"    - {employee.FirstName} {employee.LastName} - ${employee.Salary:F2}");
            }

            return sb.ToString().Trim();
        }
    }
}