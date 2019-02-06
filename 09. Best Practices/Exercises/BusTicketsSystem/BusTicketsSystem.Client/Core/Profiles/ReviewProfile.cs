namespace BusTicketsSystem.Client.Core.Profiles
{
    using AutoMapper;

    using Models;
    using Services.Dtos;

    public class ReviewProfile : Profile
    {
        public ReviewProfile()
        {
            this.CreateMap<Review, Review>();

            this.CreateMap<Review, ReviewExistsByIdDto>();
        }
    }
}