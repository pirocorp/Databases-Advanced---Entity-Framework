namespace BusTicketsSystem.Client.Core.Interfaces
{
    public interface IWriter
    {
        void Write(object input);

        void WriteLine(object input);
    }
}