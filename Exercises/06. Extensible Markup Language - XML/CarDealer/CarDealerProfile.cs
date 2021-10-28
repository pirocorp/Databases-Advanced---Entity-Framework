namespace CarDealer
{
    using System.Linq;
    using AutoMapper;
    using Dtos.Export;
    using Dtos.Import;
    using Models;
    using PartDto = Dtos.Export.PartDto;

    public class CarDealerProfile : Profile
    {
        public CarDealerProfile()
        {
            this.CreateMap<SupplierDto, Supplier>();

            this.CreateMap<Dtos.Import.PartDto, Part>();

            this.CreateMap<CarDto, Car>();

            this.CreateMap<CustomerDto, Customer>();

            this.CreateMap<SaleDto, Sale>();

            this.CreateMap<Car, CarWithTheirPartsDto>();

            this.CreateMap<Car, BmwCarDto>();

            this.CreateMap<Supplier, LocalSupplierDto>();

            this.CreateMap<Part, PartDto>()
                .ForMember(
                    d => d.Price,
                    opt => opt.MapFrom(s => s.Price));

            this.CreateMap<Car, CarWithPartsDto>()
                .ForMember(
                    d => d.PartCars,
                    opt => opt.MapFrom(s => s.PartCars
                        .OrderByDescending(pc => pc.Part.Price)
                        .Select(pc => pc.Part)));

            this.CreateMap<Customer, TotalSalesByCustomerDto>()
                .ForMember(
                    d => d.BoughtCars,
                    opt => opt.MapFrom(s => s.Sales.Count))
                .ForMember(
                    d => d.SpendMoney,
                    opt => opt.MapFrom(s => s.Sales.Sum(sale => sale.Car.PartCars.Sum(pc => pc.Part.Price))));

            this.CreateMap<Car, CarWithDiscountDto>();

            this.CreateMap<Sale, SaleWithDiscountDto>()
                .ForMember(d => d.CustomerName,
                    opt => opt.MapFrom(s => s.Customer.Name))
                .ForMember(d => d.Price,
                    opt => opt.MapFrom(s => s.Car.PartCars.Sum(pc => pc.Part.Price)))
                .ForMember(d => d.PriceWithDiscount, 
                    opt => opt.MapFrom(s => ((s.Car.PartCars.Sum(pc => pc.Part.Price) * ((100 - s.Discount) / 100))).ToString("0.#######")));
        }
    }
}
