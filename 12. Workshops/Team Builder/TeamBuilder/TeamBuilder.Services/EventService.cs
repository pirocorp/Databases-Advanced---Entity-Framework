namespace TeamBuilder.Services
{
    using System;
    using System.Linq;
    using AutoMapper;

    using Data;
    using Interfaces;
    using Models;

    public class EventService : GenericService<Event>, IEventService
    {
        public EventService(TeamBuilderContext context, IMapper mapper) 
            : base(context, mapper)
        {
        }

        public Event Create(string name, string description, 
            DateTime startDate, DateTime endDate, int creatorId)
        {
            var newEvent = new Event
            {
                Name = name,
                Description = description,
                StartDate = startDate,
                EndDate = endDate,
                CreatorId = creatorId
            };

            this.Add(newEvent);
            return newEvent;
        }

        public Event GetLatestEventByName(string eventName)
        {
            var latestEvent = this.GetAll(e => e.Name == eventName,
                e => e.StartDate,
                e => e).Last();

            return latestEvent;
        }
    }
}