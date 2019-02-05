namespace BusTicketsSystem.Client.Core.Profiles
{
    using AutoMapper;
    using Dtos;
    using Dtos.BankAccountDtos;
    using Models;

    public class BankAccountProfile : Profile
    {
        public BankAccountProfile()
        {
            this.CreateMap<BankAccount, BankAccount>();

            this.CreateMap<BankAccount, BankAccountDetailsDto>()
                .ForMember(dest => dest.CustomerId, from => from.MapFrom(c => c.Customer.Id))
                .ForMember(dest => dest.CustomerFirstName, from => from.MapFrom(c => c.Customer.FirstName))
                .ForMember(dest => dest.CustomerLastName, from => from.MapFrom(c => c.Customer.LastName));
        }
    }
}