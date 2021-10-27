namespace ProductShop
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

            this.CreateMap<CategoryProductDto, CategoryProduct>();

            this.CreateMap<Product, ProductInRangeDto>()
                .ForMember(
                    x => x.Buyer, 
                    opt => opt.MapFrom(p => p.BuyerId == null 
                            ? null
                            : $"{p.Buyer.FirstName} {p.Buyer.LastName}"));

            this.CreateMap<Product, SoldProductDto>();

            this.CreateMap<User, UsersSoldProductsDto>()
                .ForMember(x => x.SoldProducts,
                    from => from.MapFrom(u => u.ProductsSold));

            this.CreateMap<Category, CategoryByProductCountDto>()
                .ForMember(x => x.ProductsCount,
                    from => from.MapFrom(c => c.CategoryProducts.Count))
                .ForMember(x => x.AveragePrice,
                    from => from.MapFrom(c => c.CategoryProducts.Average(p => p.Product.Price)))
                .ForMember(x => x.TotalRevenue,
                    from => from.MapFrom(c => c.CategoryProducts.Sum(p => p.Product.Price)));

            this.CreateMap<User, UserWithSoldProductsDto>()
                .ForMember(x => x.SoldProducts,
                    opt => opt.MapFrom(u => u));

            this.CreateMap<User, SoldProductsDto>()
                .ForMember(x => x.SoldProducts,
                    opt => opt.MapFrom(u => u.ProductsSold.OrderByDescending(p => p.Price)))
                .ForMember(x => x.Count,
                    opt => opt.MapFrom(u => u.ProductsSold.Count));
        }
    }
}
