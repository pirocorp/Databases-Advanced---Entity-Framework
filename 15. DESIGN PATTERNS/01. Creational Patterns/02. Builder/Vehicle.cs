namespace _02._Builder
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The 'Product' class
    /// </summary>
    public class Vehicle
    {
        private readonly string _vehicleType;
        private readonly IDictionary<string, string> _parts;

        public Vehicle(string vehicleType)
        {
            this._vehicleType = vehicleType;
            this._parts = new Dictionary<string, string>();
        }

        public string this[string key]
        {
            get => this._parts[key];
            set => this._parts[key] = value;
        }

        public void Show()
        {
            Console.WriteLine("\n---------------------------");
            Console.WriteLine("Vehicle Type: {0}", this._vehicleType);
            Console.WriteLine(" Frame : {0}", this._parts["frame"]);
            Console.WriteLine(" Engine : {0}", this._parts["engine"]);
            Console.WriteLine(" #Wheels: {0}", this._parts["wheels"]);
            Console.WriteLine(" #Doors : {0}", this._parts["doors"]);
        }
    }
}
