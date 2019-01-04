namespace P03_RawData
{
    using System;
    using System.Linq;

    public class Car
    {
        private string model;
        private Engine engine;
        private Cargo cargo;
        private Tire[] tires;

        private Car(string model, Engine engine, Cargo cargo, Tire[] tires)
        {
            this.Model = model;
            this.Engine = engine;
            this.Cargo = cargo;
            this.Tires = tires;
        }

        public string Model
        {
            get => this.model;
            private set => this.model = value;
        }

        public Engine Engine
        {
            get => this.engine;
            private set => this.engine = value;
        }

        public Cargo Cargo
        {
            get => this.cargo;
            private set => this.cargo = value;
        }

        public Tire[] Tires
        {
            get => this.tires;
            private set => this.tires = value;
        }

        public static Car Parse(string inputString)
        {
            var inputTokens = inputString.Split(new[] {" "}, StringSplitOptions.RemoveEmptyEntries);
            var model = inputTokens[0];

            var engineSpeed = int.Parse(inputTokens[1]);
            var enginePower = int.Parse(inputTokens[2]);
            var engine = new Engine(engineSpeed, enginePower);

            var cargoWeight = decimal.Parse(inputTokens[3]);
            var cargoType = inputTokens[4];
            var cargo = new Cargo(cargoWeight, cargoType);

            var tires = CreateTires(inputTokens.Skip(5).ToArray());

            var car = new Car(model, engine, cargo, tires);
            return car;
        }

        private static Tire[] CreateTires(string[] tiresArgs)
        {
            var tires = new Tire[4];

            for (var i = 0; i < 4; i++)
            {
                var pressureIndex = i * 2;
                var ageIndex = (i * 2) + 1;
                var tirePressure = decimal.Parse(tiresArgs[pressureIndex]);
                var tireAge = int.Parse(tiresArgs[ageIndex]);
                var tire = new Tire(tirePressure, tireAge);
                tires[i] = tire;
            }

            return tires;
        }
    }
}