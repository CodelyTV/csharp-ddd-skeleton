using System;
using System.Collections.Generic;
using CodelyTv.Shared.Domain.Bus.Event;

namespace CodelyTv.Shared.Domain.Courses.Domain
{
    public class CourseCreatedDomainEvent : DomainEvent
    {
        public string Name { get; }
        public string Duration { get; }

        public CourseCreatedDomainEvent(string id, string name, string duration, string eventId = null,
            string occurredOn = null) : base(id, eventId, occurredOn)
        {
            Name = name;
            Duration = duration;
        }

        public CourseCreatedDomainEvent()
        {
        }

        public override string EventName()
        {
            return "course.created";
        }

        public override Dictionary<string, string> ToPrimitives()
        {
            return new Dictionary<string, string>
            {
                {"name", Name},
                {"duration", Duration}
            };
        }

        public override DomainEvent FromPrimitives(string aggregateId, Dictionary<string, string> body, string eventId,
            string occurredOn)
        {
            return new CourseCreatedDomainEvent(aggregateId, body["name"], body["duration"], eventId, occurredOn);
        }

        public override bool Equals(object obj)
        {
            if (this == obj) return true;

            var item = obj as CourseCreatedDomainEvent;
            if (item == null) return false;

            return AggregateId.Equals(item.AggregateId) && Name.Equals(item.Name) && Duration.Equals(item.Duration);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(AggregateId, Name, Duration);
        }
    }
}
