namespace ACTester.ViewModels
{
    using System;
    using System.Text;
    using AcTester.Helpers.Utilities;

    public class PlaneAirConditionerDto : VehicleAirConditionerDto
    {
        public int ElectricityUsed { get; set; }

        public override bool Test()
        {
            var sqrtVolume = Math.Sqrt(this.VolumeCovered);
            if ((this.ElectricityUsed / sqrtVolume) < Constants.MinPlaneElectricity)
            {
                return true;
            }

            return false;
        }

        public override string ToString()
        {
            var print = new StringBuilder(base.ToString());
            print.AppendLine(string.Format("Electricity Used: {0}", this.ElectricityUsed));
            print.Append("====================");
            return print.ToString();
        }
    }
}
