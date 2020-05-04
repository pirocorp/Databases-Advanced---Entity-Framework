namespace MyApp.Core
{
    public interface ICommandInterpreter
    {
        string Read(string[] inputArgs);
    }
}
