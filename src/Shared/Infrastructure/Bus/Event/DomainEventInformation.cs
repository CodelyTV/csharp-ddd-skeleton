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
            IEnumerable<Type> eventTypes = GetDomainTypes();
            foreach (Type eventType in eventTypes)
            {
                this.IndexedDomainEvents.Add(this.GetEventName(eventType), eventType);
            }
        }

        public Type ForName(string name)
        {
            Type value;
            IndexedDomainEvents.TryGetValue(name, out value);
            return value;
        }

        private string GetEventName(Type eventType)
        {
            try
            {
                DomainEvent instance = (DomainEvent) Activator.CreateInstance(eventType);
                return eventType.GetMethod("EventName").Invoke(instance, null).ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return string.Empty;
        }

        private IEnumerable<Type> GetDomainTypes()
        {
            var type = typeof(DomainEvent);

            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p) && !p.IsAbstract);
            return types;
        }
    }
}