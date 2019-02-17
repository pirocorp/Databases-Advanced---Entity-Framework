namespace TeamBuilder.App.Core.Commands
{
    using Interfaces;
    using Services.Interfaces;
    using Utilities;

    public class LogoutCommand : ICommand
    {
        private readonly ISessionService sessionService;

        public LogoutCommand(ISessionService sessionService)
        {
            this.sessionService = sessionService;
        }

        public string Execute(string[] inputArgs)
        {
            Check.CheckLength(0, inputArgs);
            this.sessionService.Authorize();
            var currentUser = this.sessionService.GetCurrentUser();

            this.sessionService.Logout();

            return $"User {currentUser.Username} successfully logged out!";
        }
    }
}