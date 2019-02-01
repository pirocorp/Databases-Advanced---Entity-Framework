namespace Exercise.App
{
    using System;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    using AutoMapper;

    using Core;
    using Core.Contracts;
    using Core.Services;
    using Data;
    using Services;
    using Services.Contracts;

    public class Startup
    {
        public static void Main()
        {
            var service = ConfigureService();
            IEngine engine = new Engine(service);
            engine.Run();
        }

        private static IServiceProvider ConfigureService()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddDbContext<ExerciseContext>(opt =>
                opt.UseSqlServer(Configuration.ConnectionString));

            serviceCollection.AddTransient<IDbInitializerService, DbInitializerService>();

            serviceCollection.AddTransient<ICommandInterpreter, CommandInterpreter>();

            serviceCollection.AddTransient<IEmployeeService, EmployeeService>();

            serviceCollection.AddTransient<IManagerService, ManagerService>();

            serviceCollection.AddAutoMapper(conf => conf.AddProfile(new ExerciseProfile()));

            var serviceProvider = serviceCollection.BuildServiceProvider();
            return serviceProvider;
        }
    }
}