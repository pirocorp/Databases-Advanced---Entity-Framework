namespace TeamBuilder.App.Profiles
{
    using System.Linq;
    using AutoMapper;
    using Dtos.ShowTeamCommandDtos;
    using Models;

    public class TeamProfile : Profile
    {
        public TeamProfile()
        {
            this.CreateMap<Team, ShowTeamDto>()
                .ForMember(d => d.Users, 
                    ud => ud.MapFrom(t => t.Users.Select(u => u.User)));
        }
    }
}