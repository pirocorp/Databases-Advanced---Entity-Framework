namespace Exercise.App.Core
{
    using System;

    using Microsoft.Extensions.DependencyInjection;

    using Contracts;
    using Services.Contracts;

    public class Engine : IEngine
    {
        private readonly IServiceProvider serviceProvider;

        public Engine(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public void Run()
        {
            var initializeDb = this.serviceProvider.GetService<IDbInitializerService>();
            initializeDb.InitializeDatabase();

            var commandInterpreter = this.serviceProvider
                .GetService<ICommandInterpreter>();

            while (true)
            {
                var input = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);

                var result = commandInterpreter.Read(input);

                Console.WriteLine(result);
            }
        }
    }
}