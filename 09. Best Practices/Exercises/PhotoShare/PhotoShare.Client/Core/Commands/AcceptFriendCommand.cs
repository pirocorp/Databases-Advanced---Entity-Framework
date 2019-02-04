namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Linq;
    using Contracts;
    using Dtos;
    using Services.Contracts;

    public class AcceptFriendCommand : ICommand
    {
        private readonly IUserService userService;

        public AcceptFriendCommand(IUserService userService)
        {
            this.userService = userService;
        }

        // AcceptFriend <username1> <username2>
        public string Execute(string[] data)
        {
            var username = data[0];
            var friendUsername = data[1];

            var userExists = this.userService.Exists(username);
            var friendExists = this.userService.Exists(friendUsername);

            if (!userExists)
            {
                throw new ArgumentException($"{username} not found!");
            }

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

            if (!isFriendRequestFromFriend)
            {
                throw new InvalidOperationException($"{friend.Username} has not added {user.Username} as a friend");
            }

            this.userService.AddFriend(user.Id, friend.Id);
            return $"{user.Username} accepted {friend.Username} as a friend";
        }
    }
}
