namespace _04._Iterator
{
    /// <summary>
    /// The 'Iterator' interface
    /// </summary>
    interface IAbstractIterator
    {
        Item First();

        Item Next();

        bool IsDone { get; }

        Item CurrentItem { get; }
    }
}
