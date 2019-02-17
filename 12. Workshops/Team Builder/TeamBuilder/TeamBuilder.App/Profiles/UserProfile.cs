namespace TeamBuilder.App.Profiles
{
    using AutoMapper;
    using Dtos.ShowTeamCommandDtos;
    using Models;

    public class UserProfile : Profile
    {

        public UserProfile()
        {
            this.CreateMap<User, ShowTeamUserDto>();
        }
    }
}