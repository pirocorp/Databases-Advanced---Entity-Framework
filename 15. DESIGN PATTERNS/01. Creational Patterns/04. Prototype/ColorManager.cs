namespace _04._Prototype
{
    using System.Collections.Generic;

    /// <summary>
    /// Prototype manager
    /// </summary>
    public class ColorManager
    {
        private IDictionary<string, ColorPrototype> _colors;

        public ColorManager()
        {
            this._colors = new Dictionary<string, ColorPrototype>();
        }

        public ColorPrototype this[string key]
        {
            get => this._colors[key];
            set => this._colors[key] = value;
        }
    }
}
