namespace Exercise.App.Core.Commands
{
    using System;
    using System.Threading;
    using Contracts;

    public class ExitCommand : ICommand
    {
        public string Execute(string[] args)
        {
            for (var i = 5; i > 0; i--)
            {
                Console.Write("\r");
                Console.Write($"Program will close after {i} seconds");
                Thread.Sleep(1000); 
            }

            Console.WriteLine();
            Console.Clear();
            Environment.Exit(0);
            return null;
        }
    }
}