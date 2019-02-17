namespace TeamBuilder.App.Core.Commands
{
    using System;
    using System.Linq;
    using Dtos.ShowTeamCommandDtos;
    using Interfaces;
    using Services.Interfaces;
    using Utilities;

    public class ShowTeamCommand : ICommand
    {
        private readonly IHelperService helperService;
        private readonly ITeamService teamService;

        public ShowTeamCommand(IHelperService helperService, ITeamService teamService)
        {
            this.helperService = helperService;
            this.teamService = teamService;
        }

        //ShowTeam <teamName>
        public string Execute(string[] inputArgs)
        {
            Check.CheckLength(1, inputArgs);

            var teamName = inputArgs[0];

            var teamExists = this.helperService.IsTeamExisting(teamName);

            //Validate Team exists
            if (!teamExists)
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.TeamNotFound, teamName));
            }

            //Without Dtos
            //var team = this.teamService.All
            //    .Where(t => t.Name == teamName)
            //    .Select(t => new
            //    {
            //        t.Name,
            //        t.Acronym,
            //        Members = t.Users.Select(u => u.User.Username).ToArray()
            //    })
            //    .First();

            //With Dtos
            var currentTeam = this.teamService
                .ProjectTo<ShowTeamDto>(t => t.Name == teamName)
                .First();

            var result = $"{currentTeam.Name} {currentTeam.Acronym}" + Environment.NewLine +
                         $"Members:" + Environment.NewLine +
                         $"{string.Join(Environment.NewLine, currentTeam.Users.Select(x => $"--{x.Username}"))}";

            return result.Trim();
        }
    }
}