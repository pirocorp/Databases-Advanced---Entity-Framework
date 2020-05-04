namespace MyApp.Core.Implementations
{
    using System;
    using Microsoft.Extensions.DependencyInjection;

    public class Engine : IEngine
    {
        private readonly IServiceProvider _provider;

        public Engine(IServiceProvider provider)
        {
            this._provider = provider;
        }

        public void Run()
        {
            while (true)
            {
                try
                {
                    var inputArgs = Console.ReadLine()
                        .Split(" ", StringSplitOptions.RemoveEmptyEntries);

                    var commandInterpreter = this._provider.GetService<ICommandInterpreter>();
                    var result = commandInterpreter.Read(inputArgs);

                    if (result == "Exit")
                    {
                        //Console.WriteLine(result);
                        return;
                    }

                    Console.WriteLine(result);
                }
                catch (InvalidOperationException ioe)
                {
                    Console.WriteLine(ioe.Message);
                }
                catch (ArgumentException ae)
                {
                    Console.WriteLine(ae.Message);
                }
            }
        }
    }
}
