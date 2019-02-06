namespace BusTicketsSystem.Client
{
    using System;
    using System.IO;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using AutoMapper;
    using Core;
    using Core.Interfaces;
    using Core.IO;
    using Core.Profiles;
    using Data;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using Microsoft.EntityFrameworkCore.Internal;
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

            #region Database configuration

            //Database connection configuration
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            //DbContext --> Configuration
            serviceCollection.AddDbContext<BusTicketsContext>(options =>
            {
                options.UseLazyLoadingProxies() // <-- Allows lazy loading
                       .UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            ////Adds proxy service required for Lazy loading
            //serviceCollection.AddEntityFrameworkProxies();

            #endregion Database configuration

            #region AutoMapper configuration

            //AutoMapper --> Configuration
            serviceCollection.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<BankAccountProfile>();
                cfg.AddProfile<BusCompanyProfile>();
                cfg.AddProfile<BusStationProfile>();
                cfg.AddProfile<CountryProfile>();
                cfg.AddProfile<CustomerProfile>();
                cfg.AddProfile<ReviewProfile>();
                cfg.AddProfile<TicketProfile>();
                cfg.AddProfile<TownProfile>();
                cfg.AddProfile<TripProfile>();
            });

            #endregion AutoMapper configuration

            #region Services

            //IO Services
            serviceCollection.AddTransient<IReader, ConsoleReader>();
            serviceCollection.AddTransient<IWriter, ConsoleWriter>();

            //Core Service -> Makes commands executes them and return the result of commands
            serviceCollection.AddTransient<ICommandInterpreter, CommandInterpreter>();

            //Database Communication Services
            serviceCollection.AddTransient<IDatabaseInitializerService, DatabaseInitializerService>();
            serviceCollection.AddTransient<IBankAccountService, BankAccountService>();
            serviceCollection.AddTransient<IBusCompanyService, BusCompanyService>();
            serviceCollection.AddTransient<IBusStationService, BusStationService>();
            serviceCollection.AddTransient<ICountryService, CountryService>();
            serviceCollection.AddTransient<ICustomerService, CustomerService>();
            serviceCollection.AddTransient<IReviewService, ReviewService>();
            serviceCollection.AddTransient<ITicketService, TicketService>();
            serviceCollection.AddTransient<ITownService, TownService>();
            serviceCollection.AddTransient<ITripService, TripService>();

            #endregion

            var serviceProvider = serviceCollection.BuildServiceProvider();
            return serviceProvider;
        }
    }
}