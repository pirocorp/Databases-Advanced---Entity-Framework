namespace BusTicketsSystem.Client.Core.Commands
{
    using System.Linq;
    using System.Text;

    using Dtos.BusCompanyDtos;
    using Interfaces;
    using Services.Interfaces;

    //FindBusCompany [<string>]
    public class FindBusCompanyCommand : ICommand
    {
        private readonly IBusCompanyService busCompanyService;

        public FindBusCompanyCommand(IBusCompanyService busCompanyService)
        {
            this.busCompanyService = busCompanyService;
        }

        public string Execute(string[] args)
        {
            if (args.Length == 0)
            {
                var allStations = this.busCompanyService.FindBy<BusCompanyFindDto>(e => e.Name != null)
                    .OrderBy(x => x.CountryName)
                    .ThenBy(x => x.Name)
                    .ToArray();

                return this.GetData(allStations);
            }

            var name = args[0];

            var stations = this.busCompanyService.FindBy<BusCompanyFindDto>(e => e.Name.Contains(name))
                .OrderBy(x => x.CountryName)
                .ThenBy(x => x.Name)
                .ToArray();

            return this.GetData(stations);
        }

        private string GetData(BusCompanyFindDto[] stations)
        {
            var sb = new StringBuilder();

            foreach (var station in stations)
            {
                sb.AppendLine($"{station.Id}: {station.CountryName} {station.Name}");
            }

            return sb.ToString().Trim();
        }
    }
}