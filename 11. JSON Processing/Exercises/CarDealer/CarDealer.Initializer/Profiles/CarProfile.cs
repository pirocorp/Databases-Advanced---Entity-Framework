namespace CarDealer.Initializer.Profiles
{
    using AutoMapper;

    using Dtos;
    using Models;

    public class CarProfile : Profile
    {
        public CarProfile()
        {
            this.CreateMap<CarDto, Car>();
        }
    }
}