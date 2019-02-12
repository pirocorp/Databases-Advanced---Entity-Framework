namespace CarDealer.App.Profiles
{
    using System.Linq;

    using AutoMapper;

    using Dtos;
    using Models;

    public class CustomerProfile : Profile
    {

        public CustomerProfile()
        {
            this.CreateMap<Customer, CustomerDto>()
                .ForMember(x => x.BoughtCars, from => from.MapFrom(c => c.Sales.Count))
                .ForMember(x => x.SpentMoney, from => from.MapFrom(c => c.Sales.Select(s => s.Car.Parts.Select(p => p.Part.Price).Sum()).Sum()));
        }
    }
}