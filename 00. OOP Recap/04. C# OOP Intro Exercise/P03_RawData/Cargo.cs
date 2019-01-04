namespace P03_RawData
{
    public class Cargo
    {
        private decimal weight;
        private string type;

        public Cargo(decimal weight, string type)
        {
            this.Weight = weight;
            this.Type = type;
        }

        public decimal Weight
        {
            get => this.weight;
            private set => this.weight = value;
        }

        public string Type
        {
            get => this.type;
            private set => this.type = value;
        }
    }
}