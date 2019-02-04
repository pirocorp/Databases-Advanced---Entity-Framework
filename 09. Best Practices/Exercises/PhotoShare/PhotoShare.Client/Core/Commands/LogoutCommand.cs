namespace PhotoShare.Client.Core.Commands
{
    using System;

    using Contracts;
    using Services.Contracts;

    public class LogoutCommand : ICommand
    {
        private readonly IUserSessionService userSessionService;
        private readonly IUserService userService;

        public LogoutCommand(IUserService userService, IUserSessionService userSessionService)
        {
            this.userService = userService;
            this.userSessionService = userSessionService;
        }

        public string Execute(string[] args)
        {
            if (!this.userSessionService.IsLoggedIn())
            {
                throw new InvalidOperationException("You should log in first in order to logout.");
            }

            var loggedOutUsername = this.userService.Logout();

            return $"User {loggedOutUsername} successfully logged out!";
        }
    }
}