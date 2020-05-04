namespace MyApp
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    using Automapper;
    using Core;
    using Core.Implementations;
    using Data;

    public static class StartUp
    {
        public static void Main()
        {
            var services = ConfigureServices();
            var engine = new Engine(services);
            engine.Run();
        }

        private static IServiceProvider ConfigureServices()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddDbContext<MyAppContext>(db =>
                db.UseSqlServer("Server=PIRO\\SQLEXPRESS2019;Database=EmployeeCatalog;Integrated Security=True;"));

            serviceCollection.AddTransient<ICommandInterpreter, CommandInterpreter>();
            serviceCollection.AddTransient<Mapper>();

            var serviceProvider = serviceCollection.BuildServiceProvider();

            return serviceProvider;
        }
    }
}
