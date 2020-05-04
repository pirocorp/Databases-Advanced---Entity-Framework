namespace MyApp.Core.Commands
{
    using System.Linq;
    using System.Text;
    using Automapper;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using ViewModels;

    /// <summary>
    /// ManagerInfo &lt;employeeId&gt; 
    /// </summary>
    public class ManagerInfoCommand : ICommand
    {
        private readonly MyAppContext _context;
        private readonly Mapper _mapper;

        public ManagerInfoCommand(MyAppContext context, Mapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public string Execute(string[] inputArgs)
        {
            var managerId = int.Parse(inputArgs[0]);

            var manager = this._context.Employees
                .Include(m => m.ManagedEmployees)
                .FirstOrDefault(m => m.Id == managerId);

            var managerDto = this._mapper.CreateMappedObject<ManagerDto>(manager);

            var sb = new StringBuilder();

            sb.AppendLine(
                $"{managerDto.FirstName} {managerDto.LastName} | Employees: {managerDto.ManagedEmployees.Count}");

            foreach (var employeeDto in managerDto.ManagedEmployees)
            {
                sb.AppendLine($"- {employeeDto.FirstName} {employeeDto.LastName} - ${employeeDto.Salary:F2}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
