namespace BusTicketsSystem.Client.Core.Commands
{
    using System;
    using System.Linq;
    using Dtos.TicketDtos;
    using Dtos.TripDtos;
    using Interfaces;
    using Models;
    using Models.Enums;
    using Services.Interfaces;

    public class ChangeTripStatusCommand : ICommand
    {
        private readonly ITripService tripService;
        private readonly IArrivedTripService arrivedTripService;
        private readonly ITicketService ticketService;

        public ChangeTripStatusCommand(ITripService tripService, IArrivedTripService arrivedTripService, ITicketService ticketService)
        {
            this.tripService = tripService;
            this.arrivedTripService = arrivedTripService;
            this.ticketService = ticketService;
        }

        //ChangeTripStatus {Trip Id} {New Status}
        public string Execute(string[] args)
        {
            var tripId = int.Parse(args[0]);

            var isParsed = Enum.TryParse<Status>(args[1], out var newStatus);

            var tripExists = this.tripService.Exists(tripId);

            if (!tripExists)
            {
                return "No such trip!";
            }

            if (!isParsed)
            {
                return "Invalid new status!";
            }

            var originalStatus = this.tripService.ById<TripStatusDto>(tripId).Status;
            var trip = this.tripService.ChangeStatus(tripId, newStatus);

            if (newStatus != Status.Arrived)
            {
                return $"Trip from {trip.OriginBusStation.Town.Name} to {trip.DestinationBusStation.Town.Name} on {trip.DepartureTime}" + Environment.NewLine +
                       $"Status changed from {originalStatus.ToString()} to {newStatus.ToString()}";
            }

            var passengersCount = this.ticketService.FindBy<PassengerCountDto>(t => t.TripId == tripId).Count();

            var arrivedTrips = this.arrivedTripService.Create(DateTime.Now, passengersCount,
                trip.OriginBusStationId, trip.DestinationBusStationId);

            arrivedTrips = this.arrivedTripService.ById<ArrivedTrip>(arrivedTrips.Id);

            return $"Trip from {trip.OriginBusStation.Town.Name} to {trip.DestinationBusStation.Town.Name} on {trip.DepartureTime}" + Environment.NewLine +
                   $"Status changed from {originalStatus.ToString()} to {newStatus.ToString()}" + Environment.NewLine +
                   $"On {arrivedTrips.ActualArrivalTime} - {arrivedTrips.PassengersCount} passengers arrived at {arrivedTrips.DestinationBusStation.Town.Name} from {arrivedTrips.OriginBusStation.Town.Name}";
        }
    }
}