namespace _04._Iterator
{
    using System.Collections;

    /// <summary>
    /// The 'ConcreteAggregate' class
    /// </summary
    public class Collection : IAbstractCollection
    {
        private ArrayList _items;

        public Collection()
        {
            this._items = new ArrayList();
        }

        public Iterator CreateIterator()
        {
            return new Iterator(this);
        }

        public int Count => this._items.Count;

        public object this[int index]
        {
            get => this._items[index];
            set => this._items.Add(value);
        }
    }
}