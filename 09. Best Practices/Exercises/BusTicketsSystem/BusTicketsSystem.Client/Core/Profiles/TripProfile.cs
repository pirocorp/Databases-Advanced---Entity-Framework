namespace BusTicketsSystem.Client.Core.Profiles
{
    using AutoMapper;

    using Models;
    using Services.Dtos;

    public class TripProfile : Profile
    {
        public TripProfile()
        {
            this.CreateMap<Trip, Trip>();

            this.CreateMap<Trip, TripExistsByIdDto>();
        }
    }
}