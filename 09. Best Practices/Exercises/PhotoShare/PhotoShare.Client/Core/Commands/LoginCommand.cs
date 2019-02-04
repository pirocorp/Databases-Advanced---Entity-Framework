namespace PhotoShare.Client.Core.Commands
{
    using System;
    using Contracts;
    using Services.Contracts;

    public class LoginCommand : ICommand
    {
        private readonly IUserService userService;
        private readonly IUserSessionService userSessionService;

        public LoginCommand(IUserService userService, IUserSessionService userSessionService)
        {
            this.userService = userService;
            this.userSessionService = userSessionService;
        }

        public string Execute(string[] args)
        {
            if (this.userSessionService.IsLoggedIn())
            {
                throw new ArgumentException("You should logout first!");
            }

            var username = args[0];
            var password = args[1];

            var user = this.userService.Login(username, password);
            return $"User {user.Username} successfully logged in!";
        }
    }
}