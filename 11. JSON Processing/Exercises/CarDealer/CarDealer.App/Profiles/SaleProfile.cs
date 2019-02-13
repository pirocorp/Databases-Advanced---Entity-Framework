namespace CarDealer.App.Profiles
{
    using System.Linq;

    using AutoMapper;

    using Dtos;
    using Models;

    public class SaleProfile : Profile
    {
        public SaleProfile()
        {
            this.CreateMap<Sale, SaleDto>()
                .ForMember(s => s.Car, from => from.MapFrom(s => s.Car))
                .ForMember(s => s.CustomerName, from => from.MapFrom(s => s.Customer.Name))
                .ForMember(s => s.Price, from => from.MapFrom(s => s.Car.Parts.Select(p => p.Part.Price).Sum()))
                .ForMember(s => s.PriceWithDiscount, from => from.MapFrom(s => s.Car.Parts.Select(p => p.Part.Price).Sum() - 
                                                                               (s.Car.Parts.Select(p => p.Part.Price).Sum() * (s.Discount / 100M))));
        }
    }
}