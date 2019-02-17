namespace TeamBuilder.App.Core.Commands
{
    using System;
    using System.Linq;
    using Dtos.ShowEventCommandDtos;
    using Interfaces;
    using Services.Interfaces;
    using Utilities;

    public class ShowEventCommand : ICommand
    {
        private readonly IHelperService helperService;
        private readonly IEventService eventService;

        public ShowEventCommand(IHelperService helperService, IEventService eventService)
        {
            this.helperService = helperService;
            this.eventService = eventService;
        }

        //ShowEvent <eventName>
        public string Execute(string[] inputArgs)
        {
            Check.CheckLength(1, inputArgs);

            var eventName = inputArgs[0];
            var eventExists = this.helperService.IsEventExisting(eventName);

            if (!eventExists)
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.EventNotFound, eventName));
            }

            //Without Dtos
            //var currentEvent = this.eventService.All
            //    .Where(e => e.Name == eventName)
            //    .OrderByDescending(e => e.StartDate)
            //    .Select(e => new
            //    {
            //        e.Name,
            //        e.StartDate,
            //        e.EndDate,
            //        e.Description,
            //        Teams = e.Teams.Select(t => t.Team.Name).ToArray()
            //    })
            //    .First();

            //With Dtos
            var currentEvent = this.eventService
                .ProjectTo<DateTime, ShowEventDto>(e => e.Name == eventName, e => e.StartDate)
                .Last();

            var result = $"{currentEvent.Name} {currentEvent.StartDate} {currentEvent.EndDate}" + Environment.NewLine +
                         $"{currentEvent.Description}" + Environment.NewLine +
                         $"Teams:" + Environment.NewLine +
                         $"{string.Join(Environment.NewLine, currentEvent.Teams.Select(x => $"-{x}"))}";

            return result.Trim();
        }
    }
}