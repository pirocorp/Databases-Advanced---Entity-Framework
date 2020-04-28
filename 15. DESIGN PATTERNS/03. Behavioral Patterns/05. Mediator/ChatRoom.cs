namespace _05._Mediator
{
    using System.Collections.Generic;

    /// <summary>
    /// The 'ConcreteMediator' class
    /// </summary>
    public class ChatRoom : AbstractChatRoom
    {
        private readonly Dictionary<string, Participant> _participants;

        public ChatRoom()
        {
            this._participants = new Dictionary<string, Participant>();
        }

        public override void Register(Participant participant)
        {
            if (!this._participants.ContainsValue(participant))
            {
                this._participants[participant.Name] = participant;
            }

            participant.ChatRoom = this;
        }

        public override void Send(string @from, string to, string message)
        {
            Participant participant = this._participants[to];

            if (participant != null)
            {
                participant.Receive(from, message);
            }
        }
    }
}
