namespace TeamBuilder.App.Core.Commands
{
    using System;
    using Interfaces;
    using Services.Interfaces;
    using Utilities;


    public class DisbandCommand : ICommand
    {
        private readonly ISessionService sessionService;
        private readonly IHelperService helperService;
        private readonly ITeamService teamService;

        public DisbandCommand(ISessionService sessionService, IHelperService helperService, ITeamService teamService)
        {
            this.sessionService = sessionService;
            this.helperService = helperService;
            this.teamService = teamService;
        }

        //Disband <teamName>
        public string Execute(string[] inputArgs)
        {
            Check.CheckLength(1, inputArgs);
            this.sessionService.Authorize();

            var teamName = inputArgs[0];

            var teamExists = this.helperService.IsTeamExisting(teamName);

            //Validate Team exists
            if (!teamExists)
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.TeamNotFound, teamName));
            }

            var currentUser = this.sessionService.GetCurrentUser();

            var isCurrentUserCreatorOfTeam = this.helperService.IsUserCreatorOfTeam(teamName, currentUser);

            if (!isCurrentUserCreatorOfTeam)
            {
                throw new InvalidOperationException(Constants.ErrorMessages.NotAllowed);
            }

            this.teamService.DeleteTeam(teamName);
            return $"{teamName} has disbanded!";
        }
    }
}