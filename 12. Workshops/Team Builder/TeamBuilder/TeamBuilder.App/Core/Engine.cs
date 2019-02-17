namespace TeamBuilder.App.Core
{
    using System;

    using Microsoft.Extensions.DependencyInjection;

    using Interfaces;

    public class Engine
    {
        private readonly IServiceProvider serviceProvider;
        private ICommandDispatcher commandDispatcher;

        public Engine(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            this.InitializeFields();
        }

        private void InitializeFields()
        {
            this.commandDispatcher = this.serviceProvider.GetService<ICommandDispatcher>();
        }

        public void Run()
        {
            while (true)
            {
                try
                {
                    var input = Console.ReadLine();
                    var output = this.commandDispatcher.Dispatch(input);

                    Console.WriteLine(output);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.GetBaseException().Message);
                }
            }
        }
    }
}