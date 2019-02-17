namespace TeamBuilder.App
{
    using System;
    using System.IO;

    using AutoMapper;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using Core;
    using Core.Interfaces;
    using Data;
    using Profiles;
    using Services;
    using Services.Interfaces;

    public class Application
    {
        public static void Main()
        {
            var serviceProvider = ConfigureServices();
            var engine = new Engine(serviceProvider);
            engine.Run();
        }

        private static IServiceProvider ConfigureServices()
        {
            var serviceCollection = new ServiceCollection();

            //Database connection configuration
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            //Database configuration
            serviceCollection.AddDbContext<TeamBuilderContext>(opt =>
                {
                    opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
                });

            serviceCollection.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<EventProfile>();
                cfg.AddProfile<TeamProfile>();
                cfg.AddProfile<UserProfile>();
            });

            //Core Services
            serviceCollection.AddTransient<ICommandDispatcher, CommandDispatcher>();

            //Database Communication Services
            serviceCollection.AddTransient<IHelperService, HelperService>();
            serviceCollection.AddTransient<IUserService, UserService>();
            serviceCollection.AddTransient<IEventService, EventService>();
            serviceCollection.AddTransient<ITeamService, TeamService>();
            serviceCollection.AddTransient<IInvitationService, InvitationService>();
            serviceCollection.AddTransient<IUserTeamService, UserTeamService>();
            serviceCollection.AddTransient<ITeamEventService, TeamEventService>();

            //Session Service
            serviceCollection.AddSingleton<ISessionService, SessionService>();

            var serviceProvider = serviceCollection.BuildServiceProvider();
            return serviceProvider;
        }
    }
}