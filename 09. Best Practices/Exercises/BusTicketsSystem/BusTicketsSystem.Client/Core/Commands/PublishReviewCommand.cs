namespace BusTicketsSystem.Client.Core.Commands
{
    using System.Linq;
    using Interfaces;
    using Models;
    using Services.Interfaces;


    public class PublishReviewCommand : ICommand
    {
        private readonly IReviewService reviewService;
        private readonly ICustomerService customerService;
        private readonly IBusStationService busStationService;

        public PublishReviewCommand(IReviewService reviewService, ICustomerService customerService, 
            IBusStationService busStationService)
        {
            this.reviewService = reviewService;
            this.customerService = customerService;
            this.busStationService = busStationService;
        }

        //PublishReview {Customer ID} {Grade} {Bus Station Id} {Content}
        public string Execute(string[] args)
        {
            var customerId = int.Parse(args[0]);
            var grade = double.Parse(args[1]);
            var busStationId = int.Parse(args[2]);
            var content = string.Join(" ", args.Skip(3));

            var customerExists = this.customerService.Exists(customerId);

            if (!customerExists)
            {
                return "Given customer doesn't exists!";
            }

            var stationExists = this.busStationService.Exists(busStationId);

            if (!stationExists)
            {
                return "Given station doesn't exists!";
            }

            if (grade < 0 || grade > 5)
            {
                return "Grade must be in range [0 - 5]";
            }

            var review = this.reviewService.Create(content, busStationId, customerId, grade);
            review = this.reviewService.ById<Review>(review.Id);

            return $"Customer {review.Customer.FirstName} {review.Customer.LastName} published review for bus station {review.BusStation.Name} {review.BusStation.Town.Name}";
        }
    }
}