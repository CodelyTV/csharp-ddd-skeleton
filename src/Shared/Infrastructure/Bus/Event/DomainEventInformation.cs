namespace CodelyTv.Shared.Infrastructure.Bus.Event
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Domain.Bus.Event;

    public class DomainEventInformation
    {
        private Dictionary<string, Type> IndexedDomainEvents = new Dictionary<string, Type>();

        public DomainEventInformation()
        {
            GetDomainTypes().ForEach(eventType => this.IndexedDomainEvents.Add(this.GetEventName(eventType), eventType));
        }

        public Type ForName(string name)
        {
            Type value;
            IndexedDomainEvents.TryGetValue(name, out value);
            return value;
        }

        public string ForClass(DomainEvent domainEvent)
        {
            return IndexedDomainEvents.FirstOrDefault(x => x.Value.Equals(domainEvent.GetType())).Key;
        }

        private string GetEventName(Type eventType)
        {
            DomainEvent instance = (DomainEvent) Activator.CreateInstance(eventType);
            return eventType.GetMethod("EventName").Invoke(instance, null).ToString();
        }

        private List<Type> GetDomainTypes()
        {
            var type = typeof(DomainEvent);

            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p) && !p.IsAbstract).ToList();
        }
    }
}