namespace BusTicketsSystem.Client.Core.Profiles
{
    using AutoMapper;

    using Dtos.BusCompanyDtos;
    using Models;
    using Services.Dtos;

    public class BusCompanyProfile : Profile
    {
        public BusCompanyProfile()
        {
            this.CreateMap<BusCompany, BusCompany>();

            this.CreateMap<BusCompany, BusCompanyExistsByIdDto>();

            this.CreateMap<BusCompany, BusCompanyFindDto>()
                .ForMember(e => e.CountryName, s => s.MapFrom(e => e.Country.Name));
        }
    }
}