namespace Demo.DTOs.Profiles
{
    using System.Linq;

    using AutoMapper;
    using Models;

    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            this.CreateMap<Product, ProductDto>()
                .ForMember(dto => dto.InStorageCount, 
                    opt => opt.MapFrom(src => src.Storages.Sum(p => p.Quantity)));

        }
    }
}