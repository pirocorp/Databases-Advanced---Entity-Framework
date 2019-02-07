namespace BusTicketsSystem.Client.Core.Profiles
{
    using AutoMapper;

    using Dtos.TicketDtos;
    using Models;
    using Services.Dtos;

    public class TicketProfile : Profile
    {
        public TicketProfile()
        {
            this.CreateMap<Ticket, Ticket>();

            this.CreateMap<Ticket, TicketExistsByIdDto>();

            this.CreateMap<Ticket, PassengerCountDto>();
        }
    }
}