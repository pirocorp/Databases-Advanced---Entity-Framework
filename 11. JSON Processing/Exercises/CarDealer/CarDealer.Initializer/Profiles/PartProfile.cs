namespace CarDealer.Initializer.Profiles
{
    using AutoMapper;

    using Dtos;
    using Models;

    public class PartProfile : Profile
    {
        public PartProfile()
        {
            this.CreateMap<PartDto, Part>();
        }
    }
}