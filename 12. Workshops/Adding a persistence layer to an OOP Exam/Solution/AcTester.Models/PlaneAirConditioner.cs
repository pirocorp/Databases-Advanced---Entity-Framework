namespace AcTester.Models
{
    using System.ComponentModel.DataAnnotations.Schema;
    using Attributes;

    [Table("PlaneAirConditioners")]
    public class PlaneAirConditioner : VehicleAirConditioner
    {
        [Order(Order = 4)]
        public int ElectricityUsed { get; set; }
    }
}
