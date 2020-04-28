namespace _04._Iterator
{
    /// <summary>
    /// The 'ConcreteIterator' class
    /// </summary>
    public class Iterator : IAbstractIterator
    {
        private readonly Collection _collection;
        private int _current = 0;
        private int _step = 1;

        public Iterator(Collection collection)
        {
            this._collection = collection;
        }

        public Item First()
        {
            this._current = 0;
            return this._collection[this._current] as Item;
        }

        public Item Next()
        {
            this._current += this._step;
            if (!this.IsDone)
                return this._collection[this._current] as Item;
            else
                return null;
        }

        public int Step
        {
            get => this._step;
            set => this._step = value;
        }

        public Item CurrentItem => this._collection[this._current] as Item;

        public bool IsDone => this._current >= this._collection.Count;
    }
}
