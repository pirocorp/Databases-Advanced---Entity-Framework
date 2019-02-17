namespace TeamBuilder.App.Core.Commands
{
    using Interfaces;
    using Services.Interfaces;
    using Utilities;

    public class DeleteUserCommand : ICommand
    {
        private readonly ISessionService sessionService;
        private readonly IUserService userService;

        public DeleteUserCommand(ISessionService sessionService, 
            IUserService userService)
        {
            this.sessionService = sessionService;
            this.userService = userService;
        }

        public string Execute(string[] inputArgs)
        {
            Check.CheckLength(0, inputArgs);
            this.sessionService.Authorize();

            var currentUser = this.sessionService.GetCurrentUser();

            this.userService.DeleteUser(currentUser);
            this.sessionService.Logout();
            return $"User {currentUser.Username} was deleted successfully!";
        }
    }
}