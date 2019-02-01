namespace Exercise.App.Core.Commands
{
    using System.Text;

    using Contracts;

    public class ListEmployeesOlderThanCommand : ICommand
    {
        private readonly IEmployeeController employeeController;

        public ListEmployeesOlderThanCommand(IEmployeeController employeeController)
        {
            this.employeeController = employeeController;
        }

        public string Execute(string[] args)
        {
            var age = int.Parse(args[0]);

            var listEmployeesOlderThanDtos = this.employeeController.GetListEmployeesOlderThan(age);

            var result = new StringBuilder();

            foreach (var dto in listEmployeesOlderThanDtos)
            {
                var manager = dto.ManagerLastName ?? "[no manager]";
                result.AppendLine($"{dto.FirstName} {dto.LastName} - ${dto.Salary:F2} - Manager: {manager}");
            }
            
            return result.ToString().Trim();
        }
    }
}