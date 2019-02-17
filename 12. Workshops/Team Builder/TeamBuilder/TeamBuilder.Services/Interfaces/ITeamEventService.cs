namespace TeamBuilder.Services.Interfaces
{
    using Models;

    public interface ITeamEventService : IGenericService<TeamEvent>
    {
        TeamEvent AddTeamToEvent(int teamId, int eventId);
    }
}