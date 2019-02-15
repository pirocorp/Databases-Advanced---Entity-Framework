namespace Forum.Console
{
    public interface IImporter
    {
        string Message { get; }

        int Order { get; }

        void Import();
    }
}