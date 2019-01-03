namespace P04_SpeedRacing
{
    using System;

    public class Car
    {
        public Car(string model, decimal fuelAmount, decimal fuelConsumptionPerKm)
        {
            this.Model = model;
            this.FuelAmount = fuelAmount;
            this.FuelConsumptionPerKm = fuelConsumptionPerKm;
            this.DistanceTraveled = 0;
        }

        public string Model { get; set; }

        public decimal FuelAmount { get; set; }

        public decimal FuelConsumptionPerKm { get; set; }

        public decimal DistanceTraveled { get; set; }

        public void Drive(decimal distance)
        {
            var reachableDistance = this.FuelAmount / this.FuelConsumptionPerKm;

            if (reachableDistance >= distance)
            {
                this.FuelAmount -= this.FuelConsumptionPerKm * distance;
                this.DistanceTraveled += distance;
                return;
            }

            throw new ArgumentException("Insufficient fuel for the drive");
        }

        public override string ToString()
        {
            var result = $"{this.Model} {this.FuelAmount:F2} {this.DistanceTraveled}";
            return result;
        }
    }
}