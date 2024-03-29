﻿namespace _05._Mediator
{
    /// <summary>
    /// The 'Mediator' abstract class
    /// </summary>
    public abstract class AbstractChatRoom
    {
        public abstract void Register(Participant participant);
        public abstract void Send(string from, string to, string message);
    }
}
