namespace TeamBuilder.App.Core
{
    using System;
    using System.Linq;
    using System.Reflection;

    using Interfaces;
    using Utilities;

    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IServiceProvider serviceProvider;

        public CommandDispatcher(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public string Dispatch(string input)
        {
            var inputArgs = input.Split(new[] {' ', '\t'}, StringSplitOptions.RemoveEmptyEntries);

            var commandName = inputArgs.Length > 0 ? inputArgs[0] : string.Empty;
            inputArgs = inputArgs.Skip(1).ToArray();

            var command = this.CreateCommand(commandName);

            if (command == null)
            {
                throw new NotSupportedException(string.Format(Constants.ErrorMessages.CommandNotSupported, commandName));
            }

            var result = command.Execute(inputArgs);
            return result;
        }

        private ICommand CreateCommand(string commandName)
        {
            commandName = commandName + "Command";

            var type = Assembly.GetCallingAssembly()
                .GetTypes()
                .FirstOrDefault(x => x.Name == commandName);

            if (type == null)
            {
                return null;
            }

            if (!typeof(ICommand).IsAssignableFrom(type))
            {
                return null;
            }

            var constructor = type.GetConstructors()
                .First();

            var constructorParameters = constructor.GetParameters()
                .Select(x => x.ParameterType)
                .ToArray();

            var service = constructorParameters.Select(this.serviceProvider.GetService)
                .ToArray();

            var command = (ICommand)constructor.Invoke(service);
            return command;
        }
    }
}