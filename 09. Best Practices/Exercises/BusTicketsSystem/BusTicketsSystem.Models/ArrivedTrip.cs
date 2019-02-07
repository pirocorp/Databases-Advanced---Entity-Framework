namespace BusTicketsSystem.Models
{
    using System;

    public class ArrivedTrip
    {
        public int Id { get; set; }

        public DateTime ActualArrivalTime { get; set; }

        public int PassengersCount { get; set; }

        public int OriginBusStationId { get; set; }
        public virtual BusStation OriginBusStation { get; set; }

        public int DestinationBusStationId { get; set; }
        public virtual BusStation DestinationBusStation { get; set; }
    }
}