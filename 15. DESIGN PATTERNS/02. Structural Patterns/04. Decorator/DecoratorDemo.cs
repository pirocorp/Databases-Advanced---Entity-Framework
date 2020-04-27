namespace _04._Decorator
{
    using System;

    public static class DecoratorDemo
    {
        public static void Main()
        {
            // Create book
            var book = new Book("Worley", "Inside ASP.NET", 10);
            book.Display();

            // Create video
            var video = new Video("Spielberg", "Jaws", 23, 92);
            video.Display();

            // Make video borrowable, then borrow and display
            Console.WriteLine("\nMaking video borrowable:");

            var borrowVideo = new Borrowable(video);
            borrowVideo.BorrowItem("Customer #1");
            borrowVideo.BorrowItem("Customer #2");

            borrowVideo.Display();
        }
    }
}