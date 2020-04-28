namespace _04._Iterator
{
    /// <summary>
    /// A collection item
    /// </summary>
    public class Item
    {
        private readonly string _name;
        
        public Item(string name)
        {
            this._name = name;
        }

        public string Name => this._name;
    }
}
