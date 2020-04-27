namespace _03._Composite
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The 'Composite' class
    /// </summary>
    public class CompositeElement : DrawingElement
    {
        private readonly List<DrawingElement> _elements;

        public CompositeElement(string name) 
            : base(name)
        {
            this._elements = new List<DrawingElement>();
        }

        public override void Add(DrawingElement d)
        {
            this._elements.Add(d);
        }

        public override void Remove(DrawingElement d)
        {
            this._elements.Remove(d);
        }

        public override void Display(int indent)
        {
            Console.WriteLine(new string('-', indent) + "+ " + this._name);

            // Display each child element on this node
            foreach (DrawingElement d in this._elements)
            {
                d.Display(indent + 2);
            }
        }
    }
}
