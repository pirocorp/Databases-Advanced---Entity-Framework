namespace CarDealer.App.Profiles
{
    using System.Linq;
    using AutoMapper;
    using Dtos;
    using Dtos.CarsWithPartsDtos;
    using Models;

    public class CarProfile : Profile
    {
        public CarProfile()
        {
            this.CreateMap<Car, CarWithDistanceDto>();

            this.CreateMap<Car, CarFromMakeFerrariDto>();

            this.CreateMap<Car, CarWithTheirPartsDto>()
                .ForMember(x => x.Parts, from => from.MapFrom(c => c.Parts.Select(p => p.Part)));

            this.CreateMap<Car, CareSaleDto>();
        }
    }
}