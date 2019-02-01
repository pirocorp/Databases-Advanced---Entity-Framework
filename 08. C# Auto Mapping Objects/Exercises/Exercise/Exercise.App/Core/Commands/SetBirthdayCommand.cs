namespace Exercise.App.Core.Commands
{
    using System;
    using Contracts;

    public class SetBirthdayCommand : ICommand
    {
        private readonly IEmployeeService employeeService;

        public SetBirthdayCommand(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        public string Execute(string[] args)
        {
            var id = int.Parse(args[0]);
            var date = DateTime.ParseExact(args[1], "dd-MM-yyyy", null);

            var dto = this.employeeService.SetBirthday(id, date);
            return $"{dto.FirstName} {dto.LastName} birthdate was set to: {dto.Birthday:dd-MM-yyyy}";
        }
    }
}