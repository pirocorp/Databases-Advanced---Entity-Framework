namespace BusTicketsSystem.Client.Core.IO
{
    using System;
    using Interfaces;

    public class ConsoleWriter : IWriter
    {
        public void WriteLine(object input)
        {
            Console.WriteLine(input.ToString());
        }

        public void Write(object input)
        {
            Console.Write(input.ToString());
        }
    }
}