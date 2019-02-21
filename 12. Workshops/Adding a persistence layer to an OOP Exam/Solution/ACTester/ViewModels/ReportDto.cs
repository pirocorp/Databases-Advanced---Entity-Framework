namespace ACTester.ViewModels
{
    using System.Text;
    using AcTester.Helpers.Enumerations;
    using Interfaces;

    public class ReportDto : IReport
    {

        public string Manufacturer { get; set; }

        public string Model { get; set; }

        public Mark Mark { get; set; }

        public override string ToString()
        {
            var print = new StringBuilder();
            print.AppendLine("Report");
            print.AppendLine("====================");
            print.AppendLine(string.Format("Manufacturer: {0}", this.Manufacturer));
            print.AppendLine(string.Format("Model: {0}", this.Model));
            print.AppendLine(string.Format("Mark: {0}", this.Mark));
            print.Append("====================");
            return print.ToString();
        }
    }
}
