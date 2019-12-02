namespace CodelyTv.Mooc.Courses.Domain
{
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
    }
}