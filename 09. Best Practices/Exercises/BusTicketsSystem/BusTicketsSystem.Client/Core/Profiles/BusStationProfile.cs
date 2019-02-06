namespace BusTicketsSystem.Client.Core.Profiles
{
    using AutoMapper;

    using Dtos.BusStationDtos;
    using Models;
    using Services.Dtos;

    public class BusStationProfile : Profile
    {
        public BusStationProfile()
        {
            this.CreateMap<BusStation, BusStation>();

            this.CreateMap<BusStation, BusStationExistsByIdDto>();

            this.CreateMap<BusStation, BusStationFindDto>()
                .ForMember(e => e.TownName, d => d.MapFrom(e => e.Town.Name))
                .ForMember(e => e.CountryName, d => d.MapFrom(e => e.Town.Country.Name));
        }
    }
}