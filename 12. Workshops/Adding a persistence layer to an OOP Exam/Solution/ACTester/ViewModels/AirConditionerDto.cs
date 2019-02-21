namespace ACTester.ViewModels
{
    using System.Text;
    using Interfaces;

    public abstract class AirConditionerDto : IAirConditioner
    {
        public string Manufacturer { get; set; }

        public string Model { get; set; }

        public abstract bool Test();

        public override string ToString()
        {
            var print = new StringBuilder();
            print.AppendLine("Air Conditioner");
            print.AppendLine("====================");
            print.AppendLine(string.Format("Manufacturer: {0}", this.Manufacturer));
            print.AppendLine(string.Format("Model: {0}", this.Model));
            return print.ToString();
        }
    }
}
