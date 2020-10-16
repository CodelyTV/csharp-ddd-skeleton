using System;
using System.Collections.Generic;
using System.Linq;
using CodelyTv.Shared.Domain.Bus.Event;

namespace CodelyTv.Shared.Infrastructure.Bus.Event
{
    public class DomainEventsInformation
    {
        private readonly Dictionary<string, Type> IndexedDomainEvents = new Dictionary<string, Type>();

        public DomainEventsInformation()
        {
            GetDomainTypes().ForEach(eventType => IndexedDomainEvents.Add(GetEventName(eventType), eventType));
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
            var instance = (DomainEvent) Activator.CreateInstance(eventType);
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
