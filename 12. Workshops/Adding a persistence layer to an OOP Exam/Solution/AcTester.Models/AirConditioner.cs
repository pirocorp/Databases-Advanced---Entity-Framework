namespace AcTester.Models
{
    using System.ComponentModel.DataAnnotations;
    using Attributes;

    public abstract class AirConditioner
    {
        public int Id { get; set; }

        [Required, Manufacturer, Order(Order = 1)]
        public string Manufacturer { get; set; }

        [Required, Model, Order(Order = 2)]
        public string Model { get; set; }
    }
}
