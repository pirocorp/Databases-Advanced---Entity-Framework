namespace ACTester.ViewModels
{
    using System.Text;

    public abstract class VehicleAirConditionerDto : AirConditionerDto
    {
        public int VolumeCovered { get; set; }

        public override string ToString()
        {
            var print = new StringBuilder(base.ToString());
            print.AppendLine(string.Format("Volume Covered: {0}", this.VolumeCovered));
            return print.ToString();
        }
    }
}
