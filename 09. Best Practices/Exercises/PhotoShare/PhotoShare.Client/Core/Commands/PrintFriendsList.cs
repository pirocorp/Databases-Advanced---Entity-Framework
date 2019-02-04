namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Linq;
    using Contracts;
    using Dtos;
    using Services.Contracts;

    public class PrintFriendsListCommand : ICommand
    {
        private readonly IUserService userService;
        private readonly IUserSessionService userSessionService;

        public PrintFriendsListCommand(IUserService userService, IUserSessionService userSessionService)
        {
            this.userService = userService;
            this.userSessionService = userSessionService;
        }

        //ListFriends <username>
        public string Execute(string[] args)
        {
            var username = args[0];

            var userExists = this.userService.Exists(username);

            if (!userExists)
            {
                throw new ArgumentException($"User {username} not found!");
            }

            var userFriends = this.userService.ByUsername<UserFriendsDto>(username).Friends.Select(x => $"-{x.Username}").ToArray();

            if (userFriends.Length == 0)
            {
                return "No friends for this user. :(";
            }

            return $"Friends:{Environment.NewLine}{string.Join(Environment.NewLine, userFriends)}";
        }
    }
}
