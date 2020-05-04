namespace MyApp.Core.Implementations
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Commands;

    public class CommandInterpreter : ICommandInterpreter
    {
        private const string SUFFIX = "Command";

        private readonly IServiceProvider _provider;

        public CommandInterpreter(IServiceProvider serviceProvider)
        {
            this._provider = serviceProvider;
        }

        public string Read(string[] inputArgs)
        {
            var commandName = inputArgs[0] + SUFFIX;
            var commandArgs = inputArgs.Skip(1).ToArray();

            var typeOfCommand = Assembly
                .GetCallingAssembly()
                .GetTypes()
                .FirstOrDefault(x => x.Name == commandName);

            if (typeOfCommand == null)
            {
                throw new InvalidOperationException($"Invalid command: {commandName}");
            }

            var constructor = typeOfCommand.GetConstructors().FirstOrDefault();

            var constructorParams = constructor.GetParameters()
                .Select(x => x.ParameterType)
                .ToArray();

            var services = constructorParams
                .Select(this._provider.GetService)
                .ToArray();

            var commandObject = (ICommand)Activator.CreateInstance(typeOfCommand, services);

            var result = commandObject.Execute(commandArgs);
            return result;
        }
    }
}
