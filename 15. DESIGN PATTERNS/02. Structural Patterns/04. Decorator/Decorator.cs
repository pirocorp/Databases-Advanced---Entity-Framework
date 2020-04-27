namespace _04._Decorator
{
    /// <summary>
    /// The 'Decorator' abstract class
    /// </summary>
    public abstract class Decorator : LibraryItem
    {
        protected readonly LibraryItem libraryItem;

        protected Decorator(LibraryItem libraryItem)
        {
            this.libraryItem = libraryItem;
        }

        public override void Display()
        {
            this.libraryItem.Display();
        }
    }
}
