namespace _04._Decorator
{
    using System;

    /// <summary>
    /// The 'ConcreteComponent' class
    /// </summary>
    public class Video : LibraryItem
    {
        private readonly string _director;
        private readonly string _title;
        private readonly int _playTime;

        public Video(string director, string title, int numCopies, int playTime)
        {
            this._director = director;
            this._title = title;
            this.NumCopies = numCopies;
            this._playTime = playTime;
        }

        public override void Display()
        {
            Console.WriteLine("\nVideo ----- ");
            Console.WriteLine(" Director: {0}", this._director);
            Console.WriteLine(" Title: {0}", this._title);
            Console.WriteLine(" # Copies: {0}", this.NumCopies);
            Console.WriteLine(" Playtime: {0}\n", this._playTime);
        }
    }
}
