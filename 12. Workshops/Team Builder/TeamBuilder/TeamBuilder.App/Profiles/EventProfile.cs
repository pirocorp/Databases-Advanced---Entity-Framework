namespace TeamBuilder.App.Profiles
{
    using System.Linq;
    using AutoMapper;
    using Dtos.ShowEventCommandDtos;
    using Models;

    public class EventProfile : Profile
    {
        public EventProfile()
        {
            this.CreateMap<Event, ShowEventDto>()
                .ForMember(d => d.Teams, 
                    from => from.MapFrom(e => e.Teams.Select(t => t.Team.Name)));
        }
    }
}