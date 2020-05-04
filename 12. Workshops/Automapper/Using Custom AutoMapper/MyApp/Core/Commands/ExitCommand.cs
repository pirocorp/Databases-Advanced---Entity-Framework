namespace MyApp.Core.Commands
{
    public class ExitCommand : ICommand
    {
        public string Execute(string[] inputArgs)
        {
            return "Exit";
        }
    }
}
