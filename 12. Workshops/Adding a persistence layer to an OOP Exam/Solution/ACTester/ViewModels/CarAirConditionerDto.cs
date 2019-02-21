namespace ACTester.ViewModels
{
    using System;
    using System.Text;
    using AcTester.Helpers.Utilities;

    public class CarAirConditionerDto : VehicleAirConditionerDto
    {
        public override bool Test()
        {
            var sqrtVolume = Math.Sqrt(this.VolumeCovered);
            if (sqrtVolume < Constants.MinCarVolume)
            {
                return false;
            }

            return true;
        }

        public override string ToString()
        {
            var print = new StringBuilder(base.ToString());
            print.Append("====================");
            return print.ToString();
        }
    }
}
