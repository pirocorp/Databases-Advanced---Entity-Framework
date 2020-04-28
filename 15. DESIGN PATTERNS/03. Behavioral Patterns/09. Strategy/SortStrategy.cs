namespace _09._Strategy
{
    using System.Collections.Generic;

    /// <summary>
    /// The 'Strategy' abstract class
    /// </summary>
    public abstract class SortStrategy
    {
        public abstract void Sort(List<string> list);
    }
}
