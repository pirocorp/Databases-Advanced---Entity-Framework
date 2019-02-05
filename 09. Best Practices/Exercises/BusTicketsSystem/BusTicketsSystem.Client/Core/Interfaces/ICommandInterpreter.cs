namespace BusTicketsSystem.Client.Core.Interfaces
{
    public interface ICommandInterpreter
    {
        string Read(string[] input);
    }
}