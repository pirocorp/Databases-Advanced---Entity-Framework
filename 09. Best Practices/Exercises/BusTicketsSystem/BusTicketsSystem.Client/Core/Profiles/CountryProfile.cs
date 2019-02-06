namespace BusTicketsSystem.Client.Core.Profiles
{
    using AutoMapper;

    using Models;
    using Services.Dtos;

    public class CountryProfile : Profile
    {
        public CountryProfile()
        {
            this.CreateMap<Country, Country>();

            this.CreateMap<Country, CountryExistsByIdDto>();

            this.CreateMap<Country, CountryExistsByNameDto>();
        }
    }
}