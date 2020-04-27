namespace _01._Adapter
{
    using System;

    /// <summary>
    /// The 'Adapter' class
    /// </summary>
    public class RichCompound : Compound
    {
        private ChemicalDatabank _bank;

        public RichCompound(string name) 
            : base(name)
        {
        }

        public override void Display()
        {
            // The Adaptee
            this._bank = new ChemicalDatabank();

            this._boilingPoint = this._bank.GetCriticalPoint(this._chemical, "B");
            this._meltingPoint = this._bank.GetCriticalPoint(this._chemical, "M");
            this._molecularWeight = this._bank.GetMolecularWeight(this._chemical);
            this._molecularFormula = this._bank.GetMolecularStructure(this._chemical);

            base.Display();
            Console.WriteLine(" Formula: {0}", this._molecularFormula);
            Console.WriteLine(" Weight : {0}", this._molecularWeight);
            Console.WriteLine(" Melting Pt: {0}", this._meltingPoint);
            Console.WriteLine(" Boiling Pt: {0}", this._boilingPoint);
        }
    }
}
