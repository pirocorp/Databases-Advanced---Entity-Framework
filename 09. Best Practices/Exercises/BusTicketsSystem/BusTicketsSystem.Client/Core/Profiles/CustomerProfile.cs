namespace BusTicketsSystem.Client.Core.Profiles
{
    using AutoMapper;

    using Models;
    using Services.Dtos;

    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            this.CreateMap<Customer, Customer>();

            this.CreateMap<Customer, CustomerExistsByIdDto>();
        }
    }
}