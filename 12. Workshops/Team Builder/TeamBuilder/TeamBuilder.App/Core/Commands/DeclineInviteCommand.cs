namespace TeamBuilder.App.Core.Commands
{
    using System;
    using Interfaces;
    using Services.Interfaces;
    using Utilities;

    public class DeclineInviteCommand : ICommand
    {
        private readonly IHelperService helperService;
        private readonly ISessionService sessionService;
        private readonly ITeamService teamService;
        private readonly IInvitationService invitationService;

        public DeclineInviteCommand(IHelperService helperService, ISessionService sessionService, ITeamService teamService, IInvitationService invitationService)
        {
            this.helperService = helperService;
            this.sessionService = sessionService;
            this.teamService = teamService;
            this.invitationService = invitationService;
        }

        //DeclineInvite <teamName>
        public string Execute(string[] inputArgs)
        {
            Check.CheckLength(1, inputArgs);
            this.sessionService.Authorize();

            var teamName = inputArgs[0];

            var teamExists = this.helperService.IsTeamExisting(teamName);

            //Validate team exists
            if (!teamExists)
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.TeamNotFound, teamName));
            }

            var currentUser = this.sessionService.GetCurrentUser();
            var inviteExists = this.helperService.IsInviteExisting(teamName, currentUser);

            if (!inviteExists)
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.InviteNotFound, teamName));
            }

            var team = this.teamService.GetTeamByName(teamName);
            this.invitationService.Decline(currentUser.Id, team.Id);
            return $"Invite from {teamName} declined.";
        }
    }
}