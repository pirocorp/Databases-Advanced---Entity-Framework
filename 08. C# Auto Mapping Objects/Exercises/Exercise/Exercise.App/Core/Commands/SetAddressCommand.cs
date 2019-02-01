namespace Exercise.App.Core.Commands
{
    using System.Linq;
    using Contracts;

    public class SetAddressCommand : ICommand
    {
        private readonly IEmployeeController employeeController;

        public SetAddressCommand(IEmployeeController employeeController)
        {
            this.employeeController = employeeController;
        }

        public string Execute(string[] args)
        {
            var id = int.Parse(args[0]);

            var address = string.Join(" ", args.Skip(1));

            this.employeeController.SetAddress(id, address);
            return "Command accomplished successfully!";
        }
    }
}