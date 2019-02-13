namespace CarDealer.App.Profiles
{
    using AutoMapper;

    using Dtos.CarsWithPartsDtos;
    using Models;

    public class PartProfile : Profile
    {
        public PartProfile()
        {
            this.CreateMap<Part, PartDto>();
        }
    }
}