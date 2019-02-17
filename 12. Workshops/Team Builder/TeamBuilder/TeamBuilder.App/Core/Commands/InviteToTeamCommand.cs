namespace TeamBuilder.App.Core.Commands
{
    using System;
    using Interfaces;
    using Services.Interfaces;
    using Utilities;

    public class InviteToTeamCommand : ICommand
    {
        private readonly ISessionService sessionService;
        private readonly IUserTeamService userTeamService;
        private readonly IInvitationService invitationService;
        private readonly IHelperService helperService;
        private readonly IUserService userService;
        private readonly ITeamService teamService;

        public InviteToTeamCommand(ISessionService sessionService, IUserTeamService userTeamService, IInvitationService invitationService, IHelperService helperService, IUserService userService, ITeamService teamService)
        {
            this.sessionService = sessionService;
            this.userTeamService = userTeamService;
            this.invitationService = invitationService;
            this.helperService = helperService;
            this.userService = userService;
            this.teamService = teamService;
        }

        //InviteToTeam <teamName> <username>
        public string Execute(string[] inputArgs)
        {
            Check.CheckLength(2, inputArgs);

            this.sessionService.Authorize();

            var teamName = inputArgs[0];
            var username = inputArgs[1];

            var teamExists = this.helperService.IsTeamExisting(teamName);
            var invitedUserExists = this.helperService.IsUserExisting(username);

            //Validating user and team exists
            if (!teamExists || !invitedUserExists)
            {
                throw new ArgumentException(Constants.ErrorMessages.TeamOfUserNotExist);
            }

            var currentUser = this.sessionService.GetCurrentUser();
            var team = this.teamService.GetTeamByName(teamName);
            var invitedUser = this.userService.GetUserByUsername(username);

            var inviteExists = this.helperService.IsInviteExisting(teamName, invitedUser);

            //Validate invite
            if (inviteExists)
            {
                throw new InvalidOperationException(Constants.ErrorMessages.InviteIsAlreadySent);
            }

            var isCurrentUserMemberOfTeam = this.helperService.IsMemberOfTeam(teamName, currentUser.Username);
            var isCurrentUserCreatorOfTeam = this.helperService.IsUserCreatorOfTeam(teamName, currentUser);

            //Validate current user is a member or creator of team
            if (!isCurrentUserCreatorOfTeam && !isCurrentUserMemberOfTeam)
            {
                throw new InvalidOperationException(Constants.ErrorMessages.NotAllowed);
            }

            var isAlreadyMember = this.helperService.IsMemberOfTeam(teamName, username);

            //Validate invited user is not a already member
            if (isAlreadyMember)
            {
                throw new InvalidOperationException(Constants.ErrorMessages.NotAllowed);
            }

            //If the user is actually the creator of the team – add him/her directly!
            if (invitedUser.Id == team.CreatorId)
            {
                this.userTeamService.AddUserToTeam(invitedUser.Id, team.Id);
                return $"Team {teamName} adds {username}!";
            }

            this.invitationService.Invite(invitedUser.Id, team.Id);
            return $"Team {teamName} invited {username}!";
        }
    }
}