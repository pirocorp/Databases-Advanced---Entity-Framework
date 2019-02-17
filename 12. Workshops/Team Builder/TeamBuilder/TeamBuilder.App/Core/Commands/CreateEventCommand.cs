namespace TeamBuilder.App.Core.Commands
{
    using System;
    using System.Globalization;

    using Interfaces;
    using Services.Interfaces;
    using Utilities;

    public class CreateEventCommand : ICommand
    {
        private readonly ISessionService sessionService;
        private readonly IEventService eventService;

        public CreateEventCommand(ISessionService sessionService, IEventService eventService)
        {
            this.sessionService = sessionService;
            this.eventService = eventService;
        }

        //CreateEvent <name> <description> <startDate> <endDate>
        public string Execute(string[] inputArgs)
        {
            Check.CheckLength(6, inputArgs);

            this.sessionService.Authorize();

            var currentUser = this.sessionService.GetCurrentUser();

            var name = inputArgs[0];
            var description = inputArgs[1];

            var startDateIsCorrect = DateTime.TryParseExact($"{inputArgs[2]} {inputArgs[3]}", Constants.DateTimeFormat,
                CultureInfo.InvariantCulture, DateTimeStyles.None, out var startDate);

            var endDateIsCorrect = DateTime.TryParseExact($"{inputArgs[4]} {inputArgs[5]}", Constants.DateTimeFormat,
                CultureInfo.InvariantCulture, DateTimeStyles.None, out var endDate);

            //Validating Dates
            if (!startDateIsCorrect || !endDateIsCorrect)
            {
                throw new ArgumentException(Constants.ErrorMessages.InvalidDateFormat);
            }

            //Validating Dates Second Phase
            if (startDate >= endDate)
            {
                throw new ArgumentException(Constants.ErrorMessages.InvalidDates);
            }

            this.eventService.Create(name, description, startDate, endDate, currentUser.Id);
            return $"Event {name} was created successfully!";
        }
    }
}