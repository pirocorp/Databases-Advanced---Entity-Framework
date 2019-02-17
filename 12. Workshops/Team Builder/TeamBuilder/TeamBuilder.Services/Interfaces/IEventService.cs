namespace TeamBuilder.Services.Interfaces
{
    using System;
    using Models;

    public interface IEventService : IGenericService<Event>
    {
        Event Create(string name, string description, DateTime startDate, DateTime endDate, int creatorId);

        Event GetLatestEventByName(string eventName);
    }
}