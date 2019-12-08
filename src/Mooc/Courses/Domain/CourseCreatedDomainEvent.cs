namespace CodelyTv.Mooc.Courses.Domain
{
    using System;
    using System.Collections.Generic;
    using CodelyTv.Shared.Domain.Bus.Event;

    public class CourseCreatedDomainEvent : DomainEvent
    {
        private string Name;
        private string Duration;

        public CourseCreatedDomainEvent(string id, string name, string duration, string eventId = null, string occurredOn = null) :
            base(id, eventId, occurredOn)
        {
            Name = name;
            Duration = duration;
        }

        public override string EventName()
        {
            return "course.created";
        }

        public override Dictionary<string, string> ToPrimitives()
        {
            return new Dictionary<string, string>()
            {
                {"name", this.Name},
                {"duration", this.Duration}
            };
        }

        public override DomainEvent FromPrimitives(string aggregateId, Dictionary<string, string> body, string eventId, string occurredOn)
        {
            return new CourseCreatedDomainEvent(aggregateId, body["name"], body["duration"], eventId, occurredOn);
        }

        public override bool Equals(object obj)
        {
            if (this == obj) return true;

            var item = obj as CourseCreatedDomainEvent;
            if (item == null) return false;

            return this.AggregateId.Equals(item.AggregateId) && this.Name.Equals(item.Name) && this.Duration.Equals(item.Duration);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.AggregateId, this.Name, this.Duration);
        }
    }
}