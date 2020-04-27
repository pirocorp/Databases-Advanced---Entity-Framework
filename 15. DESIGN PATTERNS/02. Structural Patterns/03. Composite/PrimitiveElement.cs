namespace _03._Composite
{
    using System;

    /// <summary>
    /// The 'Leaf' class
    /// </summary>
    public class PrimitiveElement : DrawingElement
    {
        public PrimitiveElement(string name) 
            : base(name)
        {
        }

        public override void Add(DrawingElement d)
        {
            Console.WriteLine(
                "Cannot add to a PrimitiveElement");
        }

        public override void Remove(DrawingElement d)
        {
            Console.WriteLine(
                "Cannot remove from a PrimitiveElement");
        }

        public override void Display(int indent)
        {
            Console.WriteLine(
                new string('-', indent) + " " + this._name);
        }
    }
}
