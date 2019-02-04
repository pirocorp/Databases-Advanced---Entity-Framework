namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Linq;
    using Contracts;
    using Dtos;
    using Services.Contracts;

    public class AddFriendCommand : ICommand
    {
        private readonly IUserService userService;
        private readonly IUserSessionService userSessionService;

        public AddFriendCommand(IUserService userService, IUserSessionService userSessionService)
        {
            this.userService = userService;
            this.userSessionService = userSessionService;
        }

        // AddFriend <username1> <username2>
        public string Execute(string[] data)
        {
            var username = data[0];
            var friendUsername = data[1];

            if (!this.userSessionService.IsLoggedIn() || 
                this.userSessionService.User.Username != username)
            {
                throw new InvalidOperationException("Invalid credentials!");
            }

            var friendExists = this.userService.Exists(friendUsername);
            
            if (!friendExists)
            {
                throw new ArgumentException($"{friendUsername} not found!");
            }

            var user = this.userService.ByUsername<UserFriendsDto>(username);
            var friend = this.userService.ByUsername<UserFriendsDto>(friendUsername);

            var isFriendRequestFromUser = user.Friends.Any(x => x.Username == friend.Username);
            var isFriendRequestFromFriend = friend.Friends.Any(x => x.Username == user.Username);

            if (isFriendRequestFromUser && isFriendRequestFromFriend)
            {
                throw new InvalidOperationException($"{friend.Username} is already a friend to {user.Username}");
            }

            if (isFriendRequestFromUser || isFriendRequestFromFriend)
            {
                throw new InvalidOperationException($"Request is already sent.");
            }

            this.userService.AddFriend(user.Id, friend.Id);
            return $"Friend {friend.Username} added to {user.Username}";
        }
    }
}
