namespace TeamBuilder.App.Core.Commands
{
    using System;
    
    using Interfaces;
    using Services.Interfaces;
    using Utilities;

    public class CreateTeamCommand : ICommand
    {
        private readonly ISessionService sessionService;
        private readonly IHelperService helperService;
        private readonly ITeamService teamService;

        public CreateTeamCommand(ISessionService sessionService, IHelperService helperService, ITeamService teamService)
        {
            this.sessionService = sessionService;
            this.helperService = helperService;
            this.teamService = teamService;
        }

        //CreateTeam <name> <acronym>
        public string Execute(string[] inputArgs)
        {
            Check.CheckLength(2, inputArgs);

            this.sessionService.Authorize();

            var currentUser = this.sessionService.GetCurrentUser();

            var name = inputArgs[0];
            var teamExists = this.helperService.IsTeamExisting(name);

            //Validate name
            if (teamExists)
            {
                throw new ArgumentException(Constants.ErrorMessages.TeamExists);
            }

            var acronym = inputArgs[1];

            //Validate acronym
            if (acronym.Length != 3)
            {
                throw new ArgumentException(Constants.ErrorMessages.InvalidAcronym);
            }

            this.teamService.Create(name, acronym, currentUser.Id);
            return $"Team {name} successfully created!";
        }
    }
}