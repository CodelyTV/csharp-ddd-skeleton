using System;
using System.Collections.Generic;
using System.Reflection;
using CodelyTv.Shared.Domain.Bus.Event;
using Newtonsoft.Json;

namespace CodelyTv.Shared.Infrastructure.Bus.Event
{
    public class DomainEventJsonDeserializer
    {
        private readonly DomainEventsInformation information;

        public DomainEventJsonDeserializer(DomainEventsInformation information)
        {
            this.information = information ?? throw new ArgumentNullException(nameof(information));
        }

        public DomainEvent Deserialize(string body)
        {
            if (string.IsNullOrEmpty(body))
            {
                throw new ArgumentNullException(nameof(body));
            }

            var eventData = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, object>>>(body);
            if (eventData == null || !eventData.ContainsKey("data"))
            {
                throw new InvalidOperationException("Invalid JSON format");
            }

            var data = eventData["data"];
            if (data == null || !data.ContainsKey("attributes") || !data.ContainsKey("type") || !data.ContainsKey("id") || !data.ContainsKey("occurred_on"))
            {
                throw new InvalidOperationException("Invalid data format");
            }

            var attributes = JsonConvert.DeserializeObject<Dictionary<string, string>>(data["attributes"]?.ToString() ?? string.Empty);
            if (attributes == null)
            {
                throw new InvalidOperationException("Invalid attributes format");
            }

            var domainEventType = information.ForName(data["type"]?.ToString() ?? throw new InvalidOperationException("Type is missing"));
            if (domainEventType == null)
            {
                throw new InvalidOperationException("Unable to resolve domain event type");
            }

            var instance = Activator.CreateInstance(domainEventType) as DomainEvent;
            if (instance == null)
            {
                throw new InvalidOperationException($"Unable to create instance of type {domainEventType}");
            }

            var fromPrimitivesMethod = domainEventType.GetTypeInfo().GetDeclaredMethod(nameof(DomainEvent.FromPrimitives));
            if (fromPrimitivesMethod == null)
            {
                throw new InvalidOperationException("FromPrimitives method not found");
            }

            var id = data["id"]?.ToString() ?? throw new InvalidOperationException("ID is missing");
            var occurredOn = data["occurred_on"]?.ToString() ?? throw new InvalidOperationException("Occurred on is missing");

            var domainEvent = fromPrimitivesMethod.Invoke(instance, new object[]
            {
                attributes["id"],
                attributes,
                id,
                occurredOn
            }) as DomainEvent;

            if (domainEvent == null)
            {
                throw new InvalidOperationException("Unable to deserialize domain event");
            }

            return domainEvent;
        }
    }
}
