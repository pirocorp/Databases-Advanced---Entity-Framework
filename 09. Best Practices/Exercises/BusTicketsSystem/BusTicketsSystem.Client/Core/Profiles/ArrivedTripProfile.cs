namespace BusTicketsSystem.Client.Core.Profiles
{
    using AutoMapper;

    using Models;
    using Services.Dtos;

    public class ArrivedTripProfile : Profile
    {
        public ArrivedTripProfile()
        {
            this.CreateMap<ArrivedTrip, ArrivedTrip>();

            this.CreateMap<ArrivedTrip, ArrivedTripExistsByIdDto>();
        }
    }
}