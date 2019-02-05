namespace BusTicketsSystem.Client
{
    using System;
    using System.IO;
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using Core;
    using Core.Interfaces;
    using Core.IO;
    using Core.Profiles;
    using Data;
    using Services;
    using Services.Interfaces;

    public class Startup
    {
        public static void Main()
        {
            var service = ConfigureServices();
            var engine = new Engine(service);
            engine.Run();
        }

        private static IServiceProvider ConfigureServices()
        {
            var serviceCollection = new ServiceCollection();

            var configuration = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json")
             .Build();

            //DbContext --> Configuration
            serviceCollection.AddDbContext<BusTicketsContext>(options =>
                {
                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
                });

            //AutoMapper --> Configuration
            serviceCollection.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<BankAccountProfile>();
            });

            //IO Services
            serviceCollection.AddTransient<IReader, ConsoleReader>();
            serviceCollection.AddTransient<IWriter, ConsoleWriter>();

            //Core Services
            serviceCollection.AddTransient<ICommandInterpreter, CommandInterpreter>();

            //Services
            serviceCollection.AddTransient<IDatabaseInitializerService, DatabaseInitializerService>();
            serviceCollection.AddTransient<IBankAccountService, BankAccountService>();

            var serviceProvider = serviceCollection.BuildServiceProvider();
            return serviceProvider;
        }
    }
}
