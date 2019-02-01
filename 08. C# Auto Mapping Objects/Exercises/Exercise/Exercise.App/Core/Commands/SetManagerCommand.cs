namespace Exercise.App.Core.Commands
{
    using System;
    using Contracts;

    public class SetManagerCommand : ICommand
    {
        private readonly IManagerController managerController;

        public SetManagerCommand(IManagerController managerController)
        {
            this.managerController = managerController;
        }

        public string Execute(string[] args)
        {
            var employeeId = int.Parse(args[0]);

            var managerId = int.Parse(args[1]);

            var dto = this.managerController.SetManager(employeeId, managerId);

            return $"Manager for {dto.FirstName} {dto.LastName} is set to: {dto.ManagerFirstName} {dto.ManagerLastName}";
        }
    }
}