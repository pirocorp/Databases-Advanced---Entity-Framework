namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Linq;
    using Contracts;
    using Dtos;
    using Services.Contracts;

    public class ModifyUserCommand : ICommand
    {
        private readonly IUserService userService;
        private readonly ITownService townService;
        private readonly IUserSessionService userSessionService;

        public ModifyUserCommand(IUserService userService, ITownService townService, IUserSessionService userSessionService)
        {
            this.userService = userService;
            this.townService = townService;
            this.userSessionService = userSessionService;
        }

        // ModifyUser <username> <property> <new value>
        // For example:
        // ModifyUser <username> Password <NewPassword>
        // ModifyUser <username> BornTown <newBornTownName>
        // ModifyUser <username> CurrentTown <newCurrentTownName>
        // !!! Cannot change username
        public string Execute(string[] data)
        {
            var username = data[0];
            var property = data[1];
            var value = data[2];

            if (!this.userSessionService.IsLoggedIn() || 
                this.userSessionService.User.Username != username)
            {
                throw new InvalidOperationException("Invalid credentials!");
            }

            var userId = this.userService.ByUsername<UserDto>(username).Id;

            if (property == "Password")
            {
                this.ChangePassword(userId, value);
            }
            else if (property == "BornTown")
            {
                this.SetBornTown(userId, value);
            }
            else if(property == "CurrentTown")
            {
                this.SetCurrentTown(userId, value);
            }
            else
            {
                throw new ArgumentException($"Property {property} not supported!");
            }

            return $"User {username} {property} is {value}.";
        }

        private void SetCurrentTown(int userId, string town)
        {
            var townExist = this.townService.Exists(town);

            if (!townExist)
            {
                throw new ArgumentException($"Value {town} not valid.{Environment.NewLine}Town {town} not found!");
            }

            var townId = this.townService.ByName<TownDto>(town).Id;
            this.userService.SetCurrentTown(userId, townId);
        }

        private void SetBornTown(int userId, string town)
        {
            var townExist = this.townService.Exists(town);

            if (!townExist)
            {
                throw new ArgumentException($"Value {town} not valid.{Environment.NewLine}Town {town} not found!");
            }

            var townId = this.townService.ByName<TownDto>(town).Id;
            this.userService.SetBornTown(userId, townId);
        }

        private void ChangePassword(int userId, string password)
        {
            var isValidPassword = password.Any(char.IsLower) && 
                                  password.Any(char.IsDigit);

            if (!isValidPassword)
            {
                throw new ArgumentException($"Value {password} not valid.{Environment.NewLine}Invalid Password");
            }

            this.userService.ChangePassword(userId, password);
        }
    }
}
