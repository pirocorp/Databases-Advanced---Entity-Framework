namespace TeamBuilder.App.Core.Commands
{
    using System;
    using Interfaces;
    using Services.Interfaces;
    using Utilities;

    public class AcceptInviteCommand : ICommand
    {
        private readonly IHelperService helperService;
        private readonly ISessionService sessionService;
        private readonly IUserTeamService userTeamService;
        private readonly ITeamService teamService;
        private readonly IInvitationService invitationService;

        public AcceptInviteCommand(IHelperService helperService, ISessionService sessionService, IUserTeamService userTeamService, ITeamService teamService, IInvitationService invitationService)
        {
            this.helperService = helperService;
            this.sessionService = sessionService;
            this.userTeamService = userTeamService;
            this.teamService = teamService;
            this.invitationService = invitationService;
        }

        //AcceptInvite <teamName>
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
            this.userTeamService.AddUserToTeam(currentUser.Id, team.Id);
            this.invitationService.Accept(currentUser.Id, team.Id);
            return $"User {currentUser.Username} joined team {teamName}!";
        }
    }
}