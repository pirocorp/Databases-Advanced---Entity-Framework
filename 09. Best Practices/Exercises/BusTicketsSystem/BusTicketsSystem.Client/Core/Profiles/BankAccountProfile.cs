namespace BusTicketsSystem.Client.Core.Profiles
{
    using AutoMapper;

    using Dtos.BankAccountDtos;
    using Models;
    using Services.Dtos;

    public class BankAccountProfile : Profile
    {
        public BankAccountProfile()
        {
            this.CreateMap<BankAccount, BankAccount>().ReverseMap();

            this.CreateMap<BankAccount, BankAccountExistsByBankAccountDto>();

            this.CreateMap<BankAccount, BankAccountExistsByIdDto>();

            this.CreateMap<BankAccount, BankAccountDetailsDto>()
                .ForMember(dest => dest.CustomerId, from => from.MapFrom(c => c.Customer.Id))
                .ForMember(dest => dest.CustomerFirstName, from => from.MapFrom(c => c.Customer.FirstName))
                .ForMember(dest => dest.CustomerLastName, from => from.MapFrom(c => c.Customer.LastName));
        }
    }
}