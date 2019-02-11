namespace ProductShop.App.Profiles
{
    using System.Linq;

    using AutoMapper;

    using Dtos.Export;
    using Dtos.Import;
    using Models;

    public class ProductShopProfile : Profile
    {
        public ProductShopProfile()
        {
            this.CreateMap<UserDto, User>();

            this.CreateMap<ProductDto, Product>();

            this.CreateMap<CategoryDto, Category>();

            this.CreateMap<Product, ProductInRangeDto>()
                .ForMember(x => x.BuyerFullName,
                    from => from.MapFrom(p => $"{p.Buyer.FirstName} {p.Buyer.LastName}".Trim()));

            this.CreateMap<Product, SoldProductDto>();

            this.CreateMap<User, UsersSoldProductsDto>()
                .ForMember(x => x.SoldProducts,
                    from => from.MapFrom(u => u.SoldProducts));

            this.CreateMap<Category, CategoryByProductCountDto>()
                .ForMember(x => x.ProductsCount,
                    from => from.MapFrom(c => c.Products.Count))
                .ForMember(x => x.AveragePrice,
                    from => from.MapFrom(c => c.Products.Average(p => p.Product.Price)))
                .ForMember(x => x.TotalRevenue,
                    from => from.MapFrom(c => c.Products.Sum(p => p.Product.Price)));

            this.CreateMap<User, UsersAndProductsDto>()
                .ForMember(x => x.SoldProducts,
                    from => from.MapFrom(u => u.SoldProducts));
        }
    }
}