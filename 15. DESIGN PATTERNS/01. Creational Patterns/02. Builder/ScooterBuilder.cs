namespace _02._Builder
{
    /// <summary>
    /// The 'YetAnotherConcreteBuilder' class
    /// </summary>
    public class ScooterBuilder : VehicleBuilder
    {
        public ScooterBuilder()
        {
            this.vehicle = new Vehicle("Scooter");
        }

        public override void BuildFrame()
        {
            this.vehicle["frame"] = "Scooter Frame";
        }

        public override void BuildEngine()
        {
            this.vehicle["engine"] = "50 cc";
        }

        public override void BuildWheels()
        {
            this.vehicle["wheels"] = "2";
        }

        public override void BuildDoors()
        {
            this.vehicle["doors"] = "0";
        }
    }
}
