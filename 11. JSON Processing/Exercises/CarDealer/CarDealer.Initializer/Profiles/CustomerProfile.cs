namespace CarDealer.Initializer.Profiles
{
    using AutoMapper;

    using Dtos;
    using Models;

    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            this.CreateMap<CustomerDto, Customer>();
        }
    }
}