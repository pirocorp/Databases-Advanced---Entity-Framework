namespace BusTicketsSystem.Client.Core.Profiles
{
    using AutoMapper;

    using Models;
    using Services.Dtos;

    public class TicketProfile : Profile
    {
        public TicketProfile()
        {
            this.CreateMap<Ticket, Ticket>();

            this.CreateMap<Ticket, TicketExistsByIdDto>();
        }
    }
}