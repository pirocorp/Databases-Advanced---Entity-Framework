namespace BusTicketsSystem.Client.Core.Commands
{
    using System.Linq;
    using System.Text;
    using Interfaces;
    using Models;
    using Services.Interfaces;

    public class BusStationTripsCommand : ICommand
    {
        private const string DATETIME_FORMAT = "dd-MM-yyyy HH:mm:ss";

        private readonly IBusStationService busStationService;

        public BusStationTripsCommand(IBusStationService busStationService)
        {
            this.busStationService = busStationService;
        }

        public string Execute(string[] args)
        {
            var busStationId = int.Parse(args[0]);

            var busStation = this.busStationService.ById<BusStation>(busStationId);

            var result = new StringBuilder();

            result.AppendLine($"{busStation.Name}, {busStation.Town.Name}");
            result.AppendLine($"Arrivals:");

            foreach (var arrival in busStation.Arrivals.OrderBy(a => a.ArrivalTime))
            {
                result.AppendLine(
                    $"From {arrival.OriginBusStation.Name} {arrival.OriginBusStation.Town.Name} | Arrive at: {arrival.ArrivalTime.ToString(DATETIME_FORMAT)} | Status: {arrival.Status.ToString()}");
            }

            result.AppendLine($"Departures:");

            foreach (var depart in busStation.Departure.OrderBy(d => d.DepartureTime))
            {
                result.AppendLine($"To {depart.DestinationBusStation.Name} {depart.DestinationBusStation.Town.Name} | Depart at: {depart.DepartureTime.ToString(DATETIME_FORMAT)} | Status {depart.Status.ToString()}");
            }

            return result.ToString().Trim();
        }
    }
}