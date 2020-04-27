namespace _03._Factory_Method
{
    using System.Collections.Generic;

    /// <summary>
    /// The 'Creator' abstract class
    /// </summary>
    public abstract class Document
    {
        private readonly List<Page> _pages;

        // Constructor calls abstract Factory method
        public Document()
        {
            this._pages = new List<Page>();
            this.CreatePages();
        }

        public List<Page> Pages => this._pages;

        // Factory Method
        public abstract void CreatePages();
    }
}
