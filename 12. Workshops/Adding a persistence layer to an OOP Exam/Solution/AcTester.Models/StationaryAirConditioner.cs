namespace AcTester.Models
{
    using System.ComponentModel.DataAnnotations.Schema;
    using Attributes;
    using Helpers.Enumerations;

    [Table("StationaryAirConditioners")]
    public class StationaryAirConditioner : AirConditioner
    {
        [PowerUsage, Order(Order = 4)]
        public int PowerUsage
        {
            get; set;
        }

        [Order(Order = 3)]
        public EnergyEfficiencyRating RequiredEnergyEfficiencyRating { get; set; }
    }
}
