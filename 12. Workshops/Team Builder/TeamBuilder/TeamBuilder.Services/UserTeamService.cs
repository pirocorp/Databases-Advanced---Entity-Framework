namespace TeamBuilder.Services
{
    using System.Linq;
    using AutoMapper;

    using Data;
    using Interfaces;
    using Models;

    public class UserTeamService : GenericService<UserTeam>, IUserTeamService
    {
        public UserTeamService(TeamBuilderContext context, IMapper mapper) 
            : base(context, mapper)
        {
        }

        public UserTeam AddUserToTeam(int userId, int teamId)
        {
            var userTeam = new UserTeam()
            {
                UserId = userId,
                TeamId = teamId
            };

            this.Add(userTeam);
            return userTeam;
        }

        public void RemoveUserFromTeam(int userId, int teamId)
        {
            var userTeam = this.GetAll(ut => ut.UserId == userId &&
                                             ut.TeamId == teamId)
                .Single();

            this.Delete(userTeam);
        }
    }
}