namespace TeamBuilder.App.Core.Commands
{
    using System;
    using Interfaces;
    using Services.Interfaces;
    using Utilities;

    public class LoginCommand : ICommand
    {
        private readonly ISessionService sessionService;
        private readonly IUserService userService;

        public LoginCommand(ISessionService sessionService, IUserService userService)
        {
            this.sessionService = sessionService;
            this.userService = userService;
        }

        //Login <username> <password>
        public string Execute(string[] inputArgs)
        {
            Check.CheckLength(2, inputArgs);

            var username = inputArgs[0];
            var password = inputArgs[1];

            if (this.sessionService.IsAuthenticated())
            {
                throw new InvalidOperationException(Constants.ErrorMessages.LogoutFirst);
            }

            var user = this.userService.GetUserByCredentials(username, password);

            if (user == null)
            {
                throw new ArgumentException(Constants.ErrorMessages.UsernameOrPasswordIsInvalid);
            }

            this.sessionService.Login(user);
            return $"User {user.Username} successfully logged in!";
        }
    }
}