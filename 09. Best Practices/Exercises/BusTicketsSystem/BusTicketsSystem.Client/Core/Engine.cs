namespace BusTicketsSystem.Client.Core
{
    using System;

    using Microsoft.Extensions.DependencyInjection;

    using Interfaces;
    using Services.Interfaces;

    public class Engine : IEngine
    {
        private readonly IServiceProvider serviceProvider;
        private IReader reader;
        private IWriter writer;
        private ICommandInterpreter commandInterpreter;
        private IDatabaseInitializerService databaseInitializerService;

        public Engine(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            this.InitializeFields();
        }

        private void InitializeFields()
        {
            this.reader = this.serviceProvider.GetService<IReader>();
            this.writer = this.serviceProvider.GetService<IWriter>();
            this.commandInterpreter = this.serviceProvider.GetService<ICommandInterpreter>();
            this.databaseInitializerService = this.serviceProvider.GetService<IDatabaseInitializerService>();
        }

        public void Run()
        {
            this.databaseInitializerService.InitializeDatabase();

            while (true)
            {
                try
                {
                    this.writer.Write($"Enter Command: ");
                    var input = this.reader.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
                    var result = this.commandInterpreter.Read(input);
                    this.writer.WriteLine(result);
                }
                catch (Exception e)
                {
                    this.writer.WriteLine(e.Message);
                }
            }
        }
    }
}