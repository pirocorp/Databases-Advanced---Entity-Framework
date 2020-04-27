namespace _02._Builder
{
    /// <summary>
    /// The 'AnotherConcreteBuilder' class
    /// </summary>
    public class CarBuilder : VehicleBuilder
    {
        public CarBuilder()
        {
            this.vehicle = new Vehicle("Car");
        }

        public override void BuildFrame()
        {
            this.vehicle["frame"] = "Car Frame";
        }

        public override void BuildEngine()
        {
            this.vehicle["engine"] = "2500 cc";
        }

        public override void BuildWheels()
        {
            this.vehicle["wheels"] = "4";
        }

        public override void BuildDoors()
        {
            this.vehicle["doors"] = "4";
        }
    }
}
