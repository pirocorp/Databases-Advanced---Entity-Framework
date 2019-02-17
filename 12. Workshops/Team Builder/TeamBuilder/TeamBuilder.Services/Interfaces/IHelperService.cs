namespace TeamBuilder.Services.Interfaces
{
    using Models;

    public interface IHelperService
    {
        bool IsTeamExisting(string teamName);

        bool IsUserExisting(string username);

        bool IsInviteExisting(string teamName, User user);

        bool IsUserCreatorOfTeam(string teamName, User user);

        bool IsUserCreatorOfEvent(string eventName, User user);

        bool IsMemberOfTeam(string teamName, string username);

        bool IsMemberOfEvent(int eventId, int teamId);

        bool IsEventExisting(string eventName);
    }
}