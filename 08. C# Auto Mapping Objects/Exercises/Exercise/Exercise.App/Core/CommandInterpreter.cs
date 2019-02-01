namespace Exercise.App.Core
{
    using System;
    using System.Linq;
    using System.Reflection;

    using Contracts;

    public class CommandInterpreter : ICommandInterpreter
    {
        private readonly IServiceProvider serviceProvider;

        public CommandInterpreter(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public string Read(string[] input)
        {
            var commandName = input[0] + "Command";

            var args = input.Skip(1).ToArray();

            var type = Assembly.GetCallingAssembly()
                .GetTypes().FirstOrDefault(x => x.Name == commandName);

            if (type == null)
            {
                throw new ArgumentException("Invalid command");
            }

            var constructor = type.GetConstructors().First();

            var constructorParameters = constructor.GetParameters()
                .Select(p => p.ParameterType)
                .ToArray();

            var services = constructorParameters
                .Select(this.serviceProvider.GetService)
                .ToArray();

            var command = (ICommand) constructor.Invoke(services);
            var result = command.Execute(args);

            return result;
        }
    }
}