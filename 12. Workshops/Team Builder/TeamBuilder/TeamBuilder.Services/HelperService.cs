namespace TeamBuilder.Services
{
    using System.Linq;

    using Data;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class HelperService : IHelperService
    {
        private readonly TeamBuilderContext context;

        public HelperService(TeamBuilderContext context)
        {
            this.context = context;
        }

        public bool IsTeamExisting(string teamName)
        {
            return this.context.Teams.Any(t => t.Name == teamName);
        }

        public bool IsUserExisting(string username)
        {
            return this.context.Users.Any(u => u.Username == username);
        }

        public bool IsInviteExisting(string teamName, User user)
        {
            return this.context.Invitations
                .Any(i => i.Team.Name == teamName &&
                          i.InvitedUserId == user.Id &&
                          i.IsActive);
        }

        public bool IsUserCreatorOfTeam(string teamName, User user)
        {
            return this.context.Teams
                .Any(t => t.Name == teamName &&
                          t.CreatorId == user.Id);
        }

        public bool IsUserCreatorOfEvent(string eventName, User user)
        {
            return this.context.Events
                .Any(e => e.Name == eventName &&
                          e.CreatorId == user.Id);
        }

        public bool IsMemberOfTeam(string teamName, string username)
        {
            return this.context.Teams
                .Include(t => t.Users)
                .ThenInclude(u => u.User)
                .Single(t => t.Name == teamName)
                .Users.Any(ut => ut.User.Username == username);
        }

        public bool IsMemberOfEvent(int eventId, int teamId)
        {
            return this.context.TeamEvents
                .Any(te => te.EventId == eventId &&
                           te.TeamId == teamId);
        }

        public bool IsEventExisting(string eventName)
        {
            return this.context.Events.Any(e => e.Name == eventName);
        }
    }
}