namespace CodelyTv.Shared.Infrastructure.Bus.Event
{
    using System;
    using System.Collections.Generic;
    using Domain.Bus.Event;
    using Newtonsoft.Json;

    public class DomainEventJsonDeserializer
    {
        private readonly DomainEventInformation _information;

        public DomainEventJsonDeserializer(DomainEventInformation information)
        {
            _information = information;
        }

        public DomainEvent Deserialize(string body)
        {
            var eventData = JsonConvert.DeserializeObject<dynamic>(body);
            dynamic data = eventData.data;
            Dictionary<string, string> attributes = JsonConvert.DeserializeObject<Dictionary<string, string>>(data.attributes.ToString());
            Type domainEventType = _information.ForName(data.type.ToString());

            DomainEvent instance = (DomainEvent) Activator.CreateInstance(domainEventType);


            return (DomainEvent) domainEventType.GetMethod("FromPrimitives").Invoke(instance, new object[]
            {
                attributes.GetValueOrDefault("id"), attributes, data.id.ToString(), data.occurred_on.ToString()
            });
        }
    }
}