namespace MyApp.Core.Commands
{
    public interface ICommand
    {
        string Execute(string[] inputArgs);
    }
}
