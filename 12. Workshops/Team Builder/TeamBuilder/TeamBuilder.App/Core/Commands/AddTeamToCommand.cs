namespace TeamBuilder.App.Core.Commands
{
    using System;
    using Interfaces;
    using Services.Interfaces;
    using Utilities;

    public class AddTeamToCommand : ICommand
    {
        private readonly ISessionService sessionService;
        private readonly IHelperService helperService;
        private readonly IEventService eventService;
        private readonly ITeamService teamService;
        private readonly ITeamEventService teamEventService;

        public AddTeamToCommand(ISessionService sessionService, IHelperService helperService, IEventService eventService, ITeamEventService teamEventService, ITeamService teamService)
        {
            this.sessionService = sessionService;
            this.helperService = helperService;
            this.eventService = eventService;
            this.teamEventService = teamEventService;
            this.teamService = teamService;
        }

        //AddTeamTo <eventName> <teamName>
        public string Execute(string[] inputArgs)
        {
            Check.CheckLength(2, inputArgs);
            this.sessionService.Authorize();

            var eventName = inputArgs[0];
            var teamName = inputArgs[1];

            var eventExists = this.helperService.IsEventExisting(eventName);

            //Validate event exists
            if (!eventExists)
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.EventNotFound, eventName));
            }

            var teamExists = this.helperService.IsTeamExisting(teamName);

            //Validate team exists
            if (!teamExists)
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.TeamNotFound, teamName));
            }

            var currentUser = this.sessionService.GetCurrentUser();
            var latestEvent = this.eventService.GetLatestEventByName(eventName);
            var team = this.teamService.GetTeamByName(teamName);

            var isAlreadyAddedToEvent = this.helperService.IsMemberOfEvent(latestEvent.Id, team.Id);

            //Validate team is not added to event
            if (isAlreadyAddedToEvent)
            {
                throw new InvalidOperationException(Constants.ErrorMessages.CannotAddSameTeamTwice);
            }

            var isCreatorOfEvent = this.helperService.IsUserCreatorOfEvent(eventName, currentUser);

            //Validate current user is creator of event
            if (!isCreatorOfEvent)
            {
                throw new InvalidOperationException(Constants.ErrorMessages.NotAllowed);
            }

            this.teamEventService.AddTeamToEvent(team.Id, latestEvent.Id);
            return $"Team {teamName} added for {eventName}!";
        }
    }
}