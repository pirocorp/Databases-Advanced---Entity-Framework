namespace _03._Composite
{
    /// <summary>
    /// The 'Component' Tree node
    /// </summary>
    public abstract class DrawingElement
    {
        protected readonly string _name;

        public DrawingElement(string name)
        {
            this._name = name;
        }

        public abstract void Add(DrawingElement d);

        public abstract void Remove(DrawingElement d);

        public abstract void Display(int indent);
    }
}
