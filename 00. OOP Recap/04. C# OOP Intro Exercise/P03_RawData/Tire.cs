namespace P03_RawData
{
    public class Tire
    {
        private decimal pressure;
        private int age;

        public Tire(decimal pressure, int age)
        {
            this.Pressure = pressure;
            this.Age = age;
        }

        public decimal Pressure
        {
            get => this.pressure;
            private set => this.pressure = value;
        }

        public int Age
        {
            get => this.age;
            private set => this.age = value;
        }
    }
}