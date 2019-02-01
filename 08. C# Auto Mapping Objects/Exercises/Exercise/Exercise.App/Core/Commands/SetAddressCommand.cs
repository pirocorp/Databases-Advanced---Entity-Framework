namespace Exercise.App.Core.Commands
{
    using System.Linq;
    using Contracts;

    public class SetAddressCommand : ICommand
    {
        private readonly IEmployeeService employeeService;

        public SetAddressCommand(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        public string Execute(string[] args)
        {
            var id = int.Parse(args[0]);

            var address = string.Join(" ", args.Skip(1));

            var dto = this.employeeService.SetAddress(id, address);
            return $"{dto.FirstName} {dto.LastName} address was set to: {dto.Address}";
        }
    }
}