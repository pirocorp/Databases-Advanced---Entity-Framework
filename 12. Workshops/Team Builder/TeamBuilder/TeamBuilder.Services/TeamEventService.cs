namespace TeamBuilder.Services
{
    using AutoMapper;

    using Data;
    using Interfaces;
    using Models;

    public class TeamEventService : GenericService<TeamEvent>, ITeamEventService
    {
        public TeamEventService(TeamBuilderContext context, IMapper mapper) 
            : base(context, mapper)
        {
        }

        public TeamEvent AddTeamToEvent(int teamId, int eventId)
        {
            var teamEvent = new TeamEvent()
            {
                TeamId = teamId,
                EventId = eventId
            };

            this.Add(teamEvent);
            return teamEvent;
        }
    }
}