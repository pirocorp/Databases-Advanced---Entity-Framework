namespace BusTicketsSystem.Client.Core.Commands
{
    using System.Linq;
    using System.Text;

    using Dtos.BusStationDtos;
    using Interfaces;
    using Services.Interfaces;

    public class FindBusStationCommand : ICommand
    {
        private readonly IBusStationService busStationService;

        public FindBusStationCommand(IBusStationService busStationService)
        {
            this.busStationService = busStationService;
        }

        //FindBusStation [<string>]
        public string Execute(string[] args)
        {
            if (args.Length == 0)
            {
                var allStations = this.busStationService.FindBy<BusStationFindDto>(e => e.Name != null)
                    .OrderBy(x => x.CountryName)
                    .ThenBy(x => x.TownName)
                    .ThenBy(x => x.Id)
                    .ToArray();

                return this.GetData(allStations);
            }

            var name = args[0];

            var stations = this.busStationService.FindBy<BusStationFindDto>(e => e.Name.Contains(name))
                .OrderBy(x => x.CountryName)
                .ThenBy(x => x.TownName)
                .ThenBy(x => x.Id)
                .ToArray();

            return this.GetData(stations);
        }

        private string GetData(BusStationFindDto[] stations)
        {
            var sb = new StringBuilder();

            foreach (var station in stations)
            {
                sb.AppendLine($"{station.Id}: {station.CountryName} {station.TownName} {station.Name}");
            }

            return sb.ToString().Trim();
        }
    }
}