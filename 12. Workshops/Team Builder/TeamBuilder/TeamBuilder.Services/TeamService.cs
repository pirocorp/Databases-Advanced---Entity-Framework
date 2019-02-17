namespace TeamBuilder.Services
{
    using System.Linq;

    using AutoMapper;

    using Data;
    using Interfaces;
    using Models;

    public class TeamService : GenericService<Team>, ITeamService
    {
        public TeamService(TeamBuilderContext context, IMapper mapper) 
            : base(context, mapper)
        {
        }

        public Team Create(string name, string acronym, int creatorId)
        {
            var team = new Team()
            {
                Name = name,
                Acronym = acronym,
                CreatorId = creatorId
            };

            this.Add(team);
            return team;
        }

        public Team GetTeamByName(string name)
        {
            var team = this.GetAll(t => t.Name == name)
                .SingleOrDefault();

            return team;
        }

        public void DeleteTeam(string teamName)
        {
            var team = this.GetTeamByName(teamName);

            this.Delete(team);
        }
    }
}