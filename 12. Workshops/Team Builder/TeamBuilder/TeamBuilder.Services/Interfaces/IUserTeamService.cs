namespace TeamBuilder.Services.Interfaces
{
    using Models;

    public interface IUserTeamService : IGenericService<UserTeam>
    {
        UserTeam AddUserToTeam(int userId, int teamId);

        void RemoveUserFromTeam(int userId, int teamId);
    }
}