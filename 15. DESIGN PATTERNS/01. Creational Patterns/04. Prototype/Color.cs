namespace _04._Prototype
{
    using System;

    /// <summary>
    /// The 'ConcretePrototype' class
    /// </summary>
    public class Color : ColorPrototype
    {
        private readonly int _red;
        private readonly int _green;
        private readonly int _blue;

        // Constructor
        public Color(int red, int green, int blue)
        {
            this._red = red;
            this._green = green;
            this._blue = blue;
        }

        // Create a shallow copy
        public override ColorPrototype Clone()
        {
            Console.WriteLine($"Cloning color RGB: {this._red:D3}, {this._green:D3}, {this._blue:D3}");

            return this.MemberwiseClone() as ColorPrototype;
        }
    }
}
