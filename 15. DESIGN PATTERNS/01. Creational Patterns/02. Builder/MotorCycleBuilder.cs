namespace _02._Builder
{
    /// <summary>
    /// The 'ConcreteBuilder' class
    /// </summary>
    public class MotorCycleBuilder : VehicleBuilder
    {
        public MotorCycleBuilder()
        {
            this.vehicle = new Vehicle("MotorCycle");
        }

        public override void BuildFrame()
        {
            this.vehicle["frame"] = "MotorCycle Frame";
        }

        public override void BuildEngine()
        {
            this.vehicle["engine"] = "500 cc";
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
