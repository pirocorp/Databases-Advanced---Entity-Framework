namespace _04._Decorator
{
    /// <summary>
    /// The 'Component' abstract class
    /// </summary>
    public abstract class LibraryItem
    {
        public int NumCopies { get; set; }

        public abstract void Display();
    }
}
