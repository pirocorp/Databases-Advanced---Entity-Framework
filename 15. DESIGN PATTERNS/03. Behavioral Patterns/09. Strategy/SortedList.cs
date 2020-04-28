namespace _09._Strategy
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The 'Context' class
    /// </summary
    public class SortedList
    {
        private readonly List<string> _list;
        private SortStrategy _sortStrategy;

        public SortedList()
        {
            this._list = new List<string>();
        }

        public void SetSortStrategy(SortStrategy sortStrategy)
        {
            this._sortStrategy = sortStrategy;
        }

        public void Add(string name)
        {
            this._list.Add(name);
        }

        public void Sort()
        {
            this._sortStrategy.Sort(this._list);

            // Iterate over list and display results
            foreach (string name in this._list)
            {
                Console.WriteLine(" " + name);
            }

            Console.WriteLine();
        }
    }
}
