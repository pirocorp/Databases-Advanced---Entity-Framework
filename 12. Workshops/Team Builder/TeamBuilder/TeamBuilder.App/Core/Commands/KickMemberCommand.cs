namespace TeamBuilder.App.Core.Commands
{
    using System;
    using Interfaces;
    using Services.Interfaces;
    using Utilities;

    public class KickMemberCommand : ICommand
    {
        private readonly ISessionService sessionService;
        private readonly IHelperService helperService;
        private readonly IUserTeamService userTeamService;
        private readonly ITeamService teamService;
        private readonly IUserService userService;

        public KickMemberCommand(ISessionService sessionService, IHelperService helperService, IUserTeamService userTeamService, ITeamService teamService, IUserService userService)
        {
            this.sessionService = sessionService;
            this.helperService = helperService;
            this.userTeamService = userTeamService;
            this.teamService = teamService;
            this.userService = userService;
        }

        //KickMember <teamName> <username>
        public string Execute(string[] inputArgs)
        {
            Check.CheckLength(2, inputArgs);
            this.sessionService.Authorize();

            var teamName = inputArgs[0];
            var username = inputArgs[1];

            var teamExists = this.helperService.IsTeamExisting(teamName);
            var userExists = this.helperService.IsUserExisting(username);

            //Validate team exists
            if (!teamExists)
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.TeamNotFound, teamName));
            }

            //Validate user exists
            if (!userExists)
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.UserNotFound, username));
            }

            var userInTeam = this.helperService.IsMemberOfTeam(teamName, username);

            //Validate user in team
            if (!userInTeam)
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.NotPartOfTeam, username, teamName));
            }

            var currentUser = this.sessionService.GetCurrentUser();
            var isCreator = this.helperService.IsUserCreatorOfTeam(teamName, currentUser);

            //Validate current user is creator of the team
            if (!isCreator)
            {
                throw new InvalidOperationException(Constants.ErrorMessages.NotAllowed);
            }

            //Validate user to be kicked is not creator of the team
            if (currentUser.Username == username)
            {
                throw new InvalidOperationException(string.Format(Constants.ErrorMessages.CommandNotAllowed, "Disband"));
            }

            var team = this.teamService.GetTeamByName(teamName);
            var user = this.userService.GetUserByUsername(username);

            this.userTeamService.RemoveUserFromTeam(user.Id, team.Id);
            return $"User {username} was kicked from {teamName}!";
        }
    }
}