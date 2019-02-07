namespace BusTicketsSystem.Client.Core.Commands
{
    using System;
    using System.Globalization;
    using Data;
    using Interfaces;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using Models;
    using Services.Interfaces;


    public class AddTripCommand : ICommand
    {
        private const string DATETIME_FORMAT = "dd-MM-yyyy HH:mm:ss";

        private readonly ITripService tripService;

        public AddTripCommand(ITripService tripService)
        {
            this.tripService = tripService;
        }

        //datetimeFormat dd-MM-yyyy
        //AddTrip <departureTime> <arrivalTime> <originBusStationId> <destinationBusStationId> <busCompanyId>
        public string Execute(string[] args)
        {
            var departureTime = DateTime.ParseExact($"{args[0]} {args[1]}", DATETIME_FORMAT, CultureInfo.InvariantCulture);
            var arrivalTime = DateTime.ParseExact($"{args[2]} {args[3]}", DATETIME_FORMAT, CultureInfo.InvariantCulture);
            var originBusStationId = int.Parse(args[4]);
            var destinationBusStationId = int.Parse(args[5]);
            var busCompanyId = int.Parse(args[6]);

            var trip = this.tripService.Create(departureTime, arrivalTime, originBusStationId, destinationBusStationId,
                busCompanyId);

            //Lazy loading does not work with newly created objects
            //Lazy loading can be done only for Original database objects
            trip = this.tripService.ById<Trip>(trip.Id); 

            return $"{trip.Id}: {trip.DepartureTime.ToString(DATETIME_FORMAT)} - {trip.ArrivalTime.ToString(DATETIME_FORMAT)}" +
                   Environment.NewLine +
                   $"{trip.OriginBusStation.Town.Name} {trip.OriginBusStation.Name} - {trip.DestinationBusStation.Town.Name} {trip.DestinationBusStation.Name}" +
                   Environment.NewLine +
                   $"{trip.BusCompany.Name} was saved to database";
        }
    }
}