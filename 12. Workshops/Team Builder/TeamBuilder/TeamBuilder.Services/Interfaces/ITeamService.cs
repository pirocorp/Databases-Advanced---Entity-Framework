namespace TeamBuilder.Services.Interfaces
{
    using Models;

    public interface ITeamService : IGenericService<Team>
    {
        Team Create(string name, string acronym, int creatorId);

        Team GetTeamByName(string name);

        void DeleteTeam(string teamName);
    }
}