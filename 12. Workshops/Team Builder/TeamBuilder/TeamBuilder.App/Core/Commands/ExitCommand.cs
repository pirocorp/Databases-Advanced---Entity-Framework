namespace TeamBuilder.App.Core.Commands
{
    using System;
    using Interfaces;
    using Utilities;

    public class ExitCommand : ICommand
    {
        public string Execute(string[] inputArgs)
        {
            Check.CheckLength(0, inputArgs);

            Environment.Exit(0);

            return "Bye!";
        }
    }
}