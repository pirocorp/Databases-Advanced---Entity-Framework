using System;
namespace _02._Command
{
    /// <summary>
    /// The 'Command' abstract class
    /// </summary>
    public abstract class Command
    {
        public abstract void Execute();

        public abstract void UnExecute();
    }
}
