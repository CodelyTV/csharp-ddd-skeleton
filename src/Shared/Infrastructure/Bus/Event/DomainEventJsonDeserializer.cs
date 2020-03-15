namespace CodelyTv.Shared.Infrastructure.Bus.Event
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using Domain.Bus.Event;
    using Newtonsoft.Json;

    public class DomainEventJsonDeserializer
    {
        private readonly DomainEventsInformation information;

        public DomainEventJsonDeserializer(DomainEventsInformation information)
        {
            this.information = information;
        }

        public DomainEvent Deserialize(string body)
        {
            var eventData = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, object>>>(body);

            var data = eventData["data"];
            var attributes = ToDictionary<string>(data["attributes"]);

            var domainEventType = information.ForName((string) data["type"]);

            DomainEvent instance = (DomainEvent) Activator.CreateInstance(domainEventType);

            DomainEvent domainEvent = (DomainEvent) domainEventType
                .GetTypeInfo()
                .GetDeclaredMethod(nameof(DomainEvent.FromPrimitives))
                .Invoke(instance, new object[]
                {
                    attributes["id"],
                    attributes,
                    data["id"].ToString(),
                    data["occurred_on"].ToString()
                });

            return domainEvent;
        }

        public static Dictionary<string, TValue> ToDictionary<TValue>(object obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            var dictionary = JsonConvert.DeserializeObject<Dictionary<string, TValue>>(json);
            return dictionary;
        }
    }
}