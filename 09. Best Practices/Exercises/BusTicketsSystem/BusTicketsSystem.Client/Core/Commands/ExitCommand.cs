namespace BusTicketsSystem.Client.Core.Commands
{
    using System;

    using Interfaces;

    public class ExitCommand : ICommand
    {
        public string Execute(string[] args)
        {
            Environment.Exit(0);
            return "Bye!";
        }
    }
}