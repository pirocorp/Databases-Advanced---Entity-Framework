namespace _04._Decorator
{
    using System;

    /// <summary>
    /// The 'ConcreteComponent' class
    /// </summary>
    public class Book : LibraryItem
    {
        private readonly string _author;
        private readonly string _title;

        public Book(string author, string title, int numCopies)
        {
            this._author = author;
            this._title = title;
            this.NumCopies = numCopies;
        }

        public override void Display()
        {
            Console.WriteLine("\nBook ------ ");
            Console.WriteLine(" Author: {0}", this._author);
            Console.WriteLine(" Title: {0}", this._title);
            Console.WriteLine(" # Copies: {0}", this.NumCopies);
        }
    }
}
