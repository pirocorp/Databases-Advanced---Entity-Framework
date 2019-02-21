namespace ACTester.ViewModels
{
    using System.Text;
    using AcTester.Helpers.Enumerations;

    public class StationaryAirConditionerDto : AirConditionerDto
    {

        public EnergyEfficiencyRating RequiredEnergyEfficiencyRating { get; set; }

        public int PowerUsage { get; set; }

        public override bool Test()
        {
            if (this.PowerUsage <= (int)this.RequiredEnergyEfficiencyRating || this.RequiredEnergyEfficiencyRating == EnergyEfficiencyRating.E)
            {
                return true;
            }

            return false;
        }

        public override string ToString()
        {
            var print = new StringBuilder(base.ToString());
            print.AppendLine(string.Format("Required energy efficiency rating: {0}", this.RequiredEnergyEfficiencyRating));
            print.AppendLine(string.Format("Power Usage(KW / h): {0}", this.PowerUsage));
            print.Append("====================");
            return print.ToString();
        }
    }
}
