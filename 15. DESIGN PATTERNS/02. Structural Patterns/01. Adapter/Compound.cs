namespace _01._Adapter
{
    using System;

    /// <summary>
    /// The 'Target' class
    /// </summary>
    public class Compound
    {
        protected readonly string _chemical;
        protected float _boilingPoint;
        protected float _meltingPoint;
        protected double _molecularWeight;
        protected string _molecularFormula;

        // Constructor
        public Compound(string chemical)
        {
            this._chemical = chemical;
        }

        public virtual void Display()
        {
            Console.WriteLine("\nCompound: {0} ------ ", this._chemical);
        }
    }
}
