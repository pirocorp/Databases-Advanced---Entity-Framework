namespace CarDealer.Initializer.Profiles
{
    using AutoMapper;
    using Dtos;
    using Models;

    public class SupplierProfile : Profile
    {
        public SupplierProfile()
        {
            this.CreateMap<SupplierDto, Supplier>()
                .ForMember(x => x.IsImporter, 
                    from => from.MapFrom(x => bool.Parse(x.IsImporter)));
        }
    }
}