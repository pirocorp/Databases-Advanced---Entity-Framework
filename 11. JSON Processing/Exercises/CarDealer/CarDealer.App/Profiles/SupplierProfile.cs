namespace CarDealer.App.Profiles
{
    using AutoMapper;

    using Dtos;
    using Models;

    public class SupplierProfile : Profile
    {
        public SupplierProfile()
        {
            this.CreateMap<Supplier, LocalSupplier>()
                .ForMember(s => s.PartsCount, from => from.MapFrom(s => s.Parts.Count));
        }
    }
}