namespace BusTicketsSystem.Client.Core.Profiles
{
    using AutoMapper;

    using Models;
    using Services.Dtos;

    public class TownProfile : Profile
    {
        public TownProfile()
        {
            this.CreateMap<Town, Town>();

            this.CreateMap<Town, TownExistsByIdDto>();
        }
    }
}