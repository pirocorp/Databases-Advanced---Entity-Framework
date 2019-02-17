namespace TeamBuilder.App.Core.Commands
{
    using System;
    using System.Linq;

    using Interfaces;
    using Models.Enums;
    using Services.Interfaces;
    using Utilities;

    public class RegisterUserCommand : ICommand
    {
        private readonly IHelperService helperService;
        private readonly IUserService userService;
        private readonly ISessionService sessionService;

        public RegisterUserCommand(IHelperService helperService, IUserService userService, ISessionService sessionService)
        {
            this.helperService = helperService;
            this.userService = userService;
            this.sessionService = sessionService;
        }

        //RegisterUser <username> <password> <repeat password> <firstname> <lastname> <age> <gender>
        public string Execute(string[] inputArgs)
        {
            //Validate input Length
            Check.CheckLength(7, inputArgs);

            if (this.sessionService.IsAuthenticated())
            {
                throw new InvalidOperationException(Constants.ErrorMessages.LogoutFirst);
            }

            var username = inputArgs[0];

            //Validate given username
            if (username.Length < Constants.MinUsernameLength ||
                username.Length > Constants.MaxUsernameLength)
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.UsernameNotValid, username));
            }

            var password = inputArgs[1];

            //Validate password
            if (!password.Any(char.IsDigit) ||
                !password.Any(char.IsUpper))
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.PasswordNotValid, password));
            }

            //Validate password second phase
            if (password.Length < Constants.MinPasswordLength ||
                password.Length > Constants.MaxPasswordLength)
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.PasswordNotValid, password));
            }

            var repeatedPassword = inputArgs[2];

            //Validate password second phase
            if (password != repeatedPassword)
            {
                throw new ArgumentException(Constants.ErrorMessages.PasswordDoesNotMatch);
            }

            //Validate Names
            var firstName = inputArgs[3];
            Check.CheckName(firstName);

            var lastName = inputArgs[4];
            Check.CheckName(lastName);

            var isNumber = int.TryParse(inputArgs[5], out var age);

            //Validate Age
            if (!isNumber || age <= 0)
            {
                throw new ArgumentException(Constants.ErrorMessages.AgeNotValid);
            }

            Gender gender;
            var isGenderValid = Enum.TryParse(inputArgs[6], out gender);

            //validate gender
            if (!isGenderValid)
            {
                throw new ArgumentException(Constants.ErrorMessages.GenderNotValid);
            }

            if (this.helperService.IsUserExisting(username))
            {
                throw new InvalidOperationException(string.Format(Constants.ErrorMessages.UsernameIsTaken, username));
            }

            this.userService.RegisterUser(username, password, firstName, lastName, age, gender);
            return $"User {username} was registered successfully!";
        }
    }
}