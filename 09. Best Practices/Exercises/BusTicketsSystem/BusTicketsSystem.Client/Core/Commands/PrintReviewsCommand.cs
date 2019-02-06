namespace BusTicketsSystem.Client.Core.Commands
{
    using System;
    using System.Text;
    using Interfaces;
    using Models;
    using Services.Interfaces;

    public class PrintReviewsCommand : ICommand
    {
        private readonly IReviewService reviewService;

        public PrintReviewsCommand(IReviewService reviewService)
        {
            this.reviewService = reviewService;
        }

        //PrintReview {Bus Station Id }
        public string Execute(string[] args)
        {
            var busStationId = int.Parse(args[0]);

            var reviews = this.reviewService.FindBy<Review>(r => r.BusStationId == busStationId);

            var result = new StringBuilder();

            foreach (var review in reviews)
            {
                result.AppendLine($"ID: {review.Id} Grade: {review.Grade} Date of publication: {review.DateTimeOfPublishing}");
                result.AppendLine($"{review.Customer.FirstName} {review.Customer.LastName}");
                result.AppendLine($"{review.Content}");
            }

            return result.ToString().Trim();
        }
    }
}