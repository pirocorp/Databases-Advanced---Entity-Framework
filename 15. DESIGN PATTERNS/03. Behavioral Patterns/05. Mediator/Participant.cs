namespace _05._Mediator
{
    using System;

    /// <summary>
    /// The 'AbstractColleague' class
    /// </summary>
    public class Participant
    {
        private ChatRoom _chatRoom;
        private readonly string _name;

        public Participant(string name)
        {
            this._name = name;
        }

        public string Name => this._name;

        public ChatRoom ChatRoom
        {
            set => this._chatRoom = value;
            get => this._chatRoom;
        }

        public void Send(string to, string message)
        {
            this._chatRoom.Send(this._name, to, message);
        }

        public virtual void Receive(string from, string message)
        {
            Console.WriteLine($"{from} to {Name}: '{message}'");
        }
    }
}
